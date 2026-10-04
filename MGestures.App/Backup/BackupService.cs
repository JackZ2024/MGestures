using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using MGestures.Common.IO;
using MGestures.Core.Persistence.Impl;

namespace MGestures.App.Backup
{
    internal static class BackupService
    {
        public static BackupContents Import(string backupPath)
        {
            if (!File.Exists(backupPath))
                throw new BackupException("备份文件不存在: " + backupPath);
            if (!string.Equals(Path.GetExtension(backupPath), ".mgb", StringComparison.OrdinalIgnoreCase))
                throw new BackupException("只支持当前版本的 .mgb 备份文件");

            try
            {
                using var archive = ZipFile.OpenRead(backupPath);
                var settingsName = Path.GetFileName(AppSettings.ConfigFilePath);
                var gesturesName = Path.GetFileName(AppSettings.GesturesFilePath);
                var files = archive.Entries.ToArray();

                var settingsEntries = files
                    .Where(entry => string.Equals(entry.FullName, settingsName, StringComparison.OrdinalIgnoreCase))
                    .ToArray();
                var gestureEntries = files
                    .Where(entry => string.Equals(entry.FullName, gesturesName, StringComparison.OrdinalIgnoreCase))
                    .ToArray();

                if (files.Length != 2 || settingsEntries.Length != 1 || gestureEntries.Length != 1)
                    throw new BackupException("备份必须恰好包含 settings.json 和 gestures.json");

                UserSettings settings;
                using (var stream = settingsEntries[0].Open())
                {
                    settings = UserSettingsStore.Deserialize(stream);
                }

                JsonGestureIntentStore gestures;
                using (var stream = gestureEntries[0].Open())
                {
                    gestures = new JsonGestureIntentStore(stream, closeStream: false);
                }

                return new BackupContents(settings, gestures);
            }
            catch (BackupException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new BackupException("备份文件内容无效", exception);
            }
        }

        public static void ExportTo(string destinationPath)
        {
            ExportTo(destinationPath, AppSettings.ConfigFilePath, AppSettings.GesturesFilePath);
        }

        internal static void ExportTo(string destinationPath, string settingsPath, string gesturesPath)
        {
            var directory = Path.GetDirectoryName(destinationPath)
                ?? throw new BackupException("备份路径必须包含目录");
            Directory.CreateDirectory(directory);
            var temporaryPath = Path.Combine(
                directory,
                $".{Path.GetFileName(destinationPath)}.{Guid.NewGuid():N}.tmp.mgb");

            try
            {
                using (var archive = ZipFile.Open(temporaryPath, ZipArchiveMode.Create))
                {
                    archive.CreateEntryFromFile(settingsPath, "settings.json");
                    archive.CreateEntryFromFile(gesturesPath, "gestures.json");
                }

                // 在替换用户已有备份之前，先用完整的导入流程验证新备份。
                Import(temporaryPath);
                AtomicFile.ReplaceWithFile(destinationPath, temporaryPath);
            }
            finally
            {
                if (File.Exists(temporaryPath)) File.Delete(temporaryPath);
            }
        }
    }
}
