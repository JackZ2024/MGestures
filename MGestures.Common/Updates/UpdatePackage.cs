using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

namespace MGestures.Common.Updates
{
    public static class UpdatePackage
    {
        public const long MaxDownloadBytes = 512L * 1024 * 1024;
        public const long MaxExpandedBytes = 2L * 1024 * 1024 * 1024;

        public static async Task DownloadAsync(HttpClient client, Uri uri, string destination, long expectedSize,
            string expectedSha256, IProgress<int>? progress, CancellationToken cancellationToken)
        {
            if (expectedSize <= 0 || expectedSize > MaxDownloadBytes ||
                !System.Text.RegularExpressions.Regex.IsMatch(expectedSha256, "^[0-9a-fA-F]{64}$") ||
                uri.Scheme != "https" || uri.Host != "github.com" || !uri.IsDefaultPort || uri.UserInfo.Length != 0)
                throw new InvalidDataException("更新附件地址、大小或 SHA-256 无效。");
            using var response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            response.EnsureSuccessStatusCode();
            var finalUri = response.RequestMessage?.RequestUri ?? uri;
            if (finalUri.Scheme != "https" || !finalUri.IsDefaultPort || finalUri.UserInfo.Length != 0 ||
                !(finalUri.Host == "github.com" || finalUri.Host.EndsWith(".githubusercontent.com", StringComparison.OrdinalIgnoreCase)))
                throw new InvalidDataException("更新附件重定向至不受信任的地址。");
            if (response.Content.Headers.ContentLength is long length && length != expectedSize)
                throw new InvalidDataException("更新附件大小与 Release 信息不一致。");
            using var input = await response.Content.ReadAsStreamAsync(cancellationToken);
            using var output = new FileStream(destination, FileMode.CreateNew, FileAccess.Write, FileShare.None, 81920, true);
            using var hash = IncrementalHash.CreateHash(HashAlgorithmName.SHA256);
            var buffer = new byte[81920];
            long total = 0;
            int read;
            while ((read = await input.ReadAsync(buffer.AsMemory(), cancellationToken)) > 0)
            {
                total += read;
                if (total > expectedSize) throw new InvalidDataException("下载内容超过声明的大小。");
                hash.AppendData(buffer, 0, read);
                await output.WriteAsync(buffer.AsMemory(0, read), cancellationToken);
                progress?.Report((int)(total * 100 / expectedSize));
            }
            if (total != expectedSize || !string.Equals(Convert.ToHexString(hash.GetHashAndReset()), expectedSha256, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("更新包校验失败，请重新下载。");
            await output.FlushAsync(cancellationToken);
        }

        public static string SafePath(string root, string relative)
        {
            var parts = relative.Replace('\\', '/').Split('/');
            if (string.IsNullOrWhiteSpace(relative) || Path.IsPathRooted(relative) ||
                parts.Any(part => part.Length == 0 || part == "." || part == ".." ||
                    part.EndsWith(' ') || part.EndsWith('.') || part.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 ||
                    System.Text.RegularExpressions.Regex.IsMatch(part, @"^(CON|PRN|AUX|NUL|COM[0-9]|LPT[0-9])(\.|$)", System.Text.RegularExpressions.RegexOptions.IgnoreCase)))
                throw new InvalidDataException("更新包包含不安全的文件路径。");
            var fullRoot = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;
            var full = Path.GetFullPath(Path.Combine(fullRoot, Path.Combine(parts)));
            if (!full.StartsWith(fullRoot, StringComparison.OrdinalIgnoreCase)) throw new InvalidDataException("更新路径越界。");
            RejectReparsePoints(full);
            return full;
        }

        public static void RejectReparsePoints(string path)
        {
            for (var current = Path.GetFullPath(path); current != null; current = Path.GetDirectoryName(current))
                if ((File.Exists(current) || Directory.Exists(current)) &&
                    (File.GetAttributes(current) & FileAttributes.ReparsePoint) != 0)
                    throw new InvalidDataException("更新路径不能包含符号链接或目录联接。");
        }

        public static void Extract(string archivePath, string destination, CancellationToken cancellationToken)
        {
            if (Directory.Exists(destination)) throw new IOException("暂存目录已存在。");
            using var archive = ZipFile.OpenRead(archivePath);
            if (archive.Entries.Count == 0 || archive.Entries.Count > 10000) throw new InvalidDataException("更新包文件数量异常。");
            var paths = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            long total = 0;
            // Validate every entry before creating any output.
            foreach (var entry in archive.Entries)
            {
                var relative = entry.FullName.TrimEnd('/', '\\');
                SafePath(destination, relative);
                if (!paths.Add(relative.Replace('\\', '/')) ||
                    ((entry.ExternalAttributes >> 16) & 0xF000) == 0xA000 ||
                    (entry.ExternalAttributes & (int)FileAttributes.ReparsePoint) != 0)
                    throw new InvalidDataException("更新包存在重复路径或符号链接。");
                total = checked(total + entry.Length);
                if (total > MaxExpandedBytes) throw new InvalidDataException("解压大小超出限制。");
                if (relative.Equals("settings.json", StringComparison.OrdinalIgnoreCase) ||
                    relative.Equals("gestures.json", StringComparison.OrdinalIgnoreCase) ||
                    relative.Equals("config.plist", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidDataException("更新包不得包含用户配置文件。");
            }
            Directory.CreateDirectory(destination);
            foreach (var entry in archive.Entries)
            {
                cancellationToken.ThrowIfCancellationRequested();
                var directory = entry.FullName.EndsWith('/') || entry.FullName.EndsWith('\\');
                var path = SafePath(destination, entry.FullName.TrimEnd('/', '\\'));
                if (directory) Directory.CreateDirectory(path);
                else
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                    using var source = entry.Open();
                    using var target = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None);
                    var buffer = new byte[81920];
                    long written = 0;
                    int count;
                    while ((count = source.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                        written += count;
                        if (written > entry.Length) throw new InvalidDataException("解压内容大小异常。");
                        target.Write(buffer, 0, count);
                    }
                    if (written != entry.Length) throw new InvalidDataException("更新包文件不完整。");
                }
            }
        }

        public static void ValidatePayload(string root, string version)
        {
            foreach (var name in new[] { "MGestures.exe", "MGestures.dll", "MGestures.deps.json", "MGestures.runtimeconfig.json",
                "MGestures.Common.dll", "MGestures.Core.dll", "MGestures.View.dll", "defaults/gestures.json",
                "updater/MGestures.Updater.exe", "updater/MGestures.Updater.dll", "updater/MGestures.Updater.deps.json", "updater/MGestures.Updater.runtimeconfig.json" })
                if (!File.Exists(SafePath(root, name))) throw new InvalidDataException("更新包缺少文件：" + name);
            if (!System.Version.TryParse(version, out var expected) ||
                AssemblyName.GetAssemblyName(SafePath(root, "MGestures.dll")).Version != expected)
                throw new InvalidDataException("包内软件版本与 Release 标签不一致。");
        }
    }
}
