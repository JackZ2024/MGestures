using System.Diagnostics;
using System.IO.Pipes;
using System.Text.Json;
using MGestures.Common.Updates;

namespace MGestures.Updater;

internal static class Program
{
    [STAThread]
    private static void Main(string[] args)
    {
        string? stage = null;
        var authorized = false;
        try
        {
            if (args.Length != 1) throw new InvalidDataException("更新器只能由 MGestures 启动。");
            var jobPath = Path.GetFullPath(args[0]);
            stage = Path.GetDirectoryName(jobPath)!;
            var root = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MGestures", "updates");
            if (!Guid.TryParseExact(Path.GetFileName(stage), "N", out _) ||
                !string.Equals(Path.GetDirectoryName(stage), root, StringComparison.OrdinalIgnoreCase) ||
                Path.GetFileName(jobPath) != "job.json" ||
                !string.Equals(Path.GetFullPath(AppContext.BaseDirectory).TrimEnd(Path.DirectorySeparatorChar), Path.Combine(stage, "runner"), StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("更新任务位置无效。");
            UpdatePackage.RejectReparsePoints(stage);
            var job = JsonSerializer.Deserialize<UpdateJob>(File.ReadAllText(jobPath)) ?? throw new InvalidDataException("更新任务无效。");
            using var parent = Process.GetProcessById(job.ParentId);
            var target = Path.GetFullPath(job.TargetDirectory).TrimEnd(Path.DirectorySeparatorChar);
            if (parent.StartTime.ToUniversalTime().Ticks != job.ParentStartTimeUtcTicks ||
                !string.Equals(parent.MainModule?.FileName, Path.Combine(target, "MGestures.exe"), StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("更新任务与主程序不匹配。");
            using var gate = new Mutex(false, UpdateJob.UpdateMutexName);
            var held = false;
            try
            {
                try { held = gate.WaitOne(0); } catch (AbandonedMutexException) { held = true; }
                if (!held) throw new InvalidOperationException("已有更新正在进行。");
                var payload = Path.Combine(stage, "payload");
                UpdatePackage.ValidatePayload(payload, job.Version);
                UpdateInstaller.ValidateFiles(payload, job.Files);
                UpdateInstaller.ProbeWriteAccess(target);
                using (var pipe = new NamedPipeClientStream(".", job.PipeName, PipeDirection.InOut, PipeOptions.None))
                {
                    pipe.Connect(15000);
                    using var reader = new StreamReader(pipe, leaveOpen: true);
                    using var writer = new StreamWriter(pipe, leaveOpen: true) { AutoFlush = true };
                    writer.WriteLine("READY");
                    // A timeout/failed handshake must never replace any files.
                    var command = reader.ReadLineAsync().WaitAsync(TimeSpan.FromSeconds(20)).GetAwaiter().GetResult();
                    if (command != "APPLY") return;
                    authorized = true;
                }
                if (!parent.WaitForExit(60000)) throw new TimeoutException("主程序未能退出，未替换任何文件。");
                UpdateInstaller.Apply(payload, target, Path.Combine(stage, "recovery"), job.Files, () =>
                {
                    gate.ReleaseMutex();
                    held = false;
                    using var started = Process.Start(new ProcessStartInfo(Path.Combine(target, "MGestures.exe")) { WorkingDirectory = target, UseShellExecute = true });
                    if (started == null) throw new IOException("新版本无法启动。");
                });
            }
            finally { if (held) gate.ReleaseMutex(); }
        }
        catch (Exception error)
        {
            if (stage != null && Directory.Exists(stage))
                try { File.WriteAllText(Path.Combine(stage, "error.log"), error.ToString()); } catch { }
            // Before the handoff, the parent reports the failure; do not pop another dialog after a canceled download.
            if (authorized)
                MessageBox.Show("更新失败：" + error.Message + "\n\n" + (stage == null ? "" : "日志和备份目录：" + stage),
                    "MGestures 更新", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
