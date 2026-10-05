using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MGestures.Common.Product;
using MGestures.Common.Updates;

namespace MGestures.App
{
    internal static class UpdateService
    {
        private static int _running;

        // Returns only after the updater has validated the task and is ready to wait for our exit.
        public static async Task PrepareAsync(VersionInfo version, IProgress<int> progress, CancellationToken cancellationToken)
        {
            if (Interlocked.CompareExchange(ref _running, 1, 0) != 0) throw new InvalidOperationException("已有更新正在进行。");
            string? stage = null;
            var handedOff = false;
            var runnerStarted = false;
            try
            {
                if (!version.CanAutoUpdate) throw new InvalidDataException("Release 缺少有效的 x64 更新包或 SHA-256，请使用手动下载。");
                var currentExe = Environment.ProcessPath ?? throw new IOException("无法定位当前程序。");
                if (!string.Equals(Path.GetFileName(currentExe), "MGestures.exe", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("请直接运行 MGestures.exe 后更新。");
                var target = Path.GetDirectoryName(currentExe)!;
                UpdateInstaller.ProbeWriteAccess(target);
                stage = Path.Combine(AppSettings.UserDataDirectory, "updates", Guid.NewGuid().ToString("N"));
                UpdatePackage.RejectReparsePoints(stage);
                Directory.CreateDirectory(stage);
                var archive = Path.Combine(stage, "package.zip");
                using var client = new HttpClient { Timeout = TimeSpan.FromMinutes(15) };
                client.DefaultRequestHeaders.UserAgent.ParseAdd("MGestures-Updater/1.0");
                await UpdatePackage.DownloadAsync(client, new Uri(version.DownloadUrl), archive, version.DownloadSize, version.Sha256, progress, cancellationToken);
                var payload = Path.Combine(stage, "payload");
                await Task.Run(() =>
                {
                    UpdatePackage.Extract(archive, payload, cancellationToken);
                    UpdatePackage.ValidatePayload(payload, version.NormalizedVersion);
                }, cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                var runner = Path.Combine(stage, "runner");
                Directory.CreateDirectory(runner);
                // Use the currently installed, trusted updater, not executable code from the downloaded ZIP.
                foreach (var name in new[] { "MGestures.Updater.exe", "MGestures.Updater.dll", "MGestures.Updater.deps.json", "MGestures.Updater.runtimeconfig.json" })
                {
                    var source = Path.Combine(target, "updater", name);
                    UpdatePackage.RejectReparsePoints(source);
                    File.Copy(source, Path.Combine(runner, name), false);
                }
                using var current = Process.GetCurrentProcess();
                var job = new UpdateJob
                {
                    ParentId = current.Id,
                    ParentStartTimeUtcTicks = current.StartTime.ToUniversalTime().Ticks,
                    TargetDirectory = target,
                    Version = version.NormalizedVersion,
                    PipeName = "MGestures-Update-" + Guid.NewGuid().ToString("N"),
                    Files = await Task.Run(() => UpdateInstaller.CreateManifest(payload), cancellationToken)
                };
                var jobPath = Path.Combine(stage, "job.json");
                File.WriteAllText(jobPath, JsonSerializer.Serialize(job));
                using var pipe = new NamedPipeServerStream(job.PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte,
                    PipeOptions.Asynchronous | PipeOptions.CurrentUserOnly);
                var start = new ProcessStartInfo(Path.Combine(runner, "MGestures.Updater.exe"))
                {
                    WorkingDirectory = runner, UseShellExecute = false, CreateNoWindow = true
                };
                start.ArgumentList.Add(jobPath);
                using var updater = Process.Start(start) ?? throw new IOException("无法启动更新器。");
                runnerStarted = true;
                using var timeout = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
                timeout.CancelAfter(TimeSpan.FromSeconds(30));
                try
                {
                    var connection = pipe.WaitForConnectionAsync(timeout.Token);
                    var exited = updater.WaitForExitAsync(timeout.Token);
                    if (await Task.WhenAny(connection, exited) == exited && !pipe.IsConnected)
                    {
                        timeout.Cancel();
                        try { await connection; } catch (OperationCanceledException) { }
                        var log = Path.Combine(stage, "error.log");
                        throw new IOException("更新器启动失败，当前软件不会退出。" +
                            (File.Exists(log) ? "\n诊断日志：" + log : "\n请确认已安装 .NET 8 Windows Desktop Runtime。"));
                    }
                    await connection;
                    using var reader = new StreamReader(pipe, leaveOpen: true);
                    using var writer = new StreamWriter(pipe, leaveOpen: true) { AutoFlush = true };
                    if (await reader.ReadLineAsync(timeout.Token) != "READY") throw new IOException("更新器未就绪。");
                    cancellationToken.ThrowIfCancellationRequested();
                    Program.SaveBeforeUpdate();
                    await writer.WriteLineAsync("APPLY");
                    await writer.FlushAsync();
                    handedOff = true;
                }
                catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
                {
                    throw new TimeoutException("更新器未能及时启动，当前软件不会退出。请确认已安装 .NET 8 Windows Desktop Runtime，或手动下载更新。");
                }
                finally { timeout.Cancel(); }
            }
            finally
            {
                if (!handedOff)
                {
                    Interlocked.Exchange(ref _running, 0);
                    // Only remove the unique task directory created by this attempt. Failed install backups are never removed here.
                    if (!runnerStarted && stage != null && Directory.Exists(stage))
                        try { UpdatePackage.RejectReparsePoints(stage); Directory.Delete(stage, true); } catch (IOException) { } catch (UnauthorizedAccessException) { }
                }
            }
        }
    }
}
