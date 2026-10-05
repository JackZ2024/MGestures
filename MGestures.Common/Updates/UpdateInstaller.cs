using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;

namespace MGestures.Common.Updates
{
    public sealed class UpdateJob
    {
        public const string UpdateMutexName = "com.Jack.MGestures.Update";
        public int ParentId { get; set; }
        public long ParentStartTimeUtcTicks { get; set; }
        public string TargetDirectory { get; set; } = "";
        public string Version { get; set; } = "";
        public string PipeName { get; set; } = "";
        public List<UpdateFile> Files { get; set; } = new();
    }

    public sealed class UpdateFile
    {
        public string Path { get; set; } = "";
        public string Sha256 { get; set; } = "";
        public bool Existed { get; set; }
    }

    /// <summary>Replaces only declared files; retains backups and a recovery journal outside the install directory.</summary>
    public static class UpdateInstaller
    {
        public static List<UpdateFile> CreateManifest(string payload)
        {
            UpdatePackage.RejectReparsePoints(payload);
            return Directory.EnumerateFiles(payload, "*", SearchOption.AllDirectories).Select(path =>
            {
                UpdatePackage.RejectReparsePoints(path);
                using var stream = File.OpenRead(path);
                return new UpdateFile { Path = System.IO.Path.GetRelativePath(payload, path), Sha256 = Convert.ToHexString(SHA256.HashData(stream)) };
            }).OrderBy(file => file.Path, StringComparer.OrdinalIgnoreCase).ToList();
        }

        public static void ValidateFiles(string payload, IReadOnlyList<UpdateFile> files)
        {
            if (files.Count == 0 || files.Count > 10000) throw new InvalidDataException("更新清单无效。");
            var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (var file in files)
            {
                if (!paths.Add(file.Path.Replace('\\', '/'))) throw new InvalidDataException("更新清单存在重复路径。");
                using var stream = File.OpenRead(UpdatePackage.SafePath(payload, file.Path));
                if (!string.Equals(Convert.ToHexString(SHA256.HashData(stream)), file.Sha256, StringComparison.OrdinalIgnoreCase))
                    throw new InvalidDataException("暂存文件校验失败：" + file.Path);
            }
            if (Directory.EnumerateFiles(payload, "*", SearchOption.AllDirectories).Count() != files.Count)
                throw new InvalidDataException("暂存文件与更新清单不一致。");
        }

        public static void ProbeWriteAccess(string target)
        {
            UpdatePackage.RejectReparsePoints(target);
            var probe = System.IO.Path.Combine(target, ".mg-update-probe-" + Guid.NewGuid().ToString("N"));
            using (new FileStream(probe, FileMode.CreateNew, FileAccess.Write, FileShare.None, 1, FileOptions.DeleteOnClose)) { }
        }

        public static void Apply(string payload, string target, string recoveryDirectory, List<UpdateFile> files, Action? startNewVersion = null)
        {
            ValidateFiles(payload, files);
            UpdatePackage.RejectReparsePoints(target);
            var fullTarget = System.IO.Path.GetFullPath(target).TrimEnd(System.IO.Path.DirectorySeparatorChar);
            var fullRecovery = System.IO.Path.GetFullPath(recoveryDirectory);
            if (fullTarget == System.IO.Path.GetPathRoot(fullTarget)?.TrimEnd(System.IO.Path.DirectorySeparatorChar) ||
                fullRecovery.StartsWith(fullTarget + System.IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase) ||
                fullTarget.StartsWith(System.IO.Path.GetFullPath(payload).TrimEnd(System.IO.Path.DirectorySeparatorChar) + System.IO.Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("更新目标或恢复目录无效。");
            if (Directory.Exists(recoveryDirectory)) throw new IOException("恢复目录已存在，禁止覆盖。");
            Directory.CreateDirectory(recoveryDirectory);
            var backup = System.IO.Path.Combine(recoveryDirectory, "files");
            var journal = System.IO.Path.Combine(recoveryDirectory, "journal.json");
            var changed = new List<UpdateFile>();
            // Complete all backups before the first replacement.
            foreach (var file in files)
            {
                var destination = UpdatePackage.SafePath(target, file.Path);
                file.Existed = File.Exists(destination);
                if (Directory.Exists(destination)) throw new IOException("目标路径是目录：" + file.Path);
                if (file.Existed)
                {
                    var saved = UpdatePackage.SafePath(backup, file.Path);
                    Directory.CreateDirectory(System.IO.Path.GetDirectoryName(saved)!);
                    File.Copy(destination, saved, false);
                }
            }
            try
            {
                foreach (var file in files)
                {
                    changed.Add(file);
                    WriteJournal(journal, target, changed, "applying");
                    ReplaceFile(UpdatePackage.SafePath(payload, file.Path), UpdatePackage.SafePath(target, file.Path));
                }
                WriteJournal(journal, target, changed, "completed");
                // No file I/O after restart: a journal failure must not trigger rollback against a running new process.
                startNewVersion?.Invoke();
            }
            catch (Exception original)
            {
                var failures = new List<Exception> { original };
                foreach (var file in changed.AsEnumerable().Reverse())
                {
                    try
                    {
                        var destination = UpdatePackage.SafePath(target, file.Path);
                        if (file.Existed) ReplaceFile(UpdatePackage.SafePath(backup, file.Path), destination);
                        else if (File.Exists(destination)) File.Delete(destination);
                    }
                    catch (Exception error) { failures.Add(error); }
                }
                WriteJournal(journal, target, changed, failures.Count == 1 ? "rolled-back" : "rollback-incomplete");
                if (failures.Count > 1) throw new AggregateException("更新失败且部分文件未能恢复，备份保存在：" + recoveryDirectory, failures);
                throw new IOException("更新失败，已恢复原文件。备份保存在：" + recoveryDirectory, original);
            }
        }

        private static void ReplaceFile(string source, string destination)
        {
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(destination)!);
            UpdatePackage.RejectReparsePoints(destination);
            var temporary = destination + ".mg-update-" + Guid.NewGuid().ToString("N");
            try
            {
                using (var input = File.OpenRead(source))
                using (var output = new FileStream(temporary, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, FileOptions.WriteThrough))
                {
                    input.CopyTo(output);
                    output.Flush(true);
                }
                File.Move(temporary, destination, true);
            }
            finally { if (File.Exists(temporary)) File.Delete(temporary); }
        }

        private static void WriteJournal(string path, string target, List<UpdateFile> files, string state)
        {
            var bytes = JsonSerializer.SerializeToUtf8Bytes(new { Target = target, State = state, Files = files }, new JsonSerializerOptions { WriteIndented = true });
            using var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.Read, 4096, FileOptions.WriteThrough);
            stream.Write(bytes);
            stream.Flush(true);
        }
    }
}
