using System;
using System.IO;
using System.Text;

namespace MGestures.Common.IO
{
    public static class AtomicFile
    {
        public static string GetBackupPath(string path)
        {
            return path + ".bak";
        }

        public static void WriteAllText(string path, string contents)
        {
            var directory = Path.GetDirectoryName(path)
                ?? throw new ArgumentException("文件路径必须包含目录", nameof(path));
            Directory.CreateDirectory(directory);

            var temporaryPath = Path.Combine(
                directory,
                $".{Path.GetFileName(path)}.{Guid.NewGuid():N}.tmp");

            try
            {
                using (var stream = new FileStream(
                    temporaryPath,
                    FileMode.CreateNew,
                    FileAccess.Write,
                    FileShare.None,
                    4096,
                    FileOptions.WriteThrough))
                using (var writer = new StreamWriter(stream, new UTF8Encoding(false)))
                {
                    writer.Write(contents);
                    writer.Flush();
                    stream.Flush(flushToDisk: true);
                }

                ReplaceWithFile(path, temporaryPath);
            }
            finally
            {
                if (File.Exists(temporaryPath)) File.Delete(temporaryPath);
            }
        }

        public static void Copy(string sourcePath, string destinationPath)
        {
            WriteAllText(destinationPath, File.ReadAllText(sourcePath, Encoding.UTF8));
        }

        public static string QuarantineCorruptFile(string path)
        {
            var quarantinePath = $"{path}.corrupt-{DateTime.Now:yyyyMMdd-HHmmssfff}";
            File.Move(path, quarantinePath);
            return quarantinePath;
        }

        public static void ReplaceWithFile(string destinationPath, string temporaryPath)
        {
            if (File.Exists(destinationPath))
            {
                File.Replace(
                    temporaryPath,
                    destinationPath,
                    GetBackupPath(destinationPath),
                    ignoreMetadataErrors: true);
            }
            else
            {
                File.Move(temporaryPath, destinationPath);
            }
        }
    }
}
