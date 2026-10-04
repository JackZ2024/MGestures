using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using MGestures.Common.IO;
using MGestures.Core;

namespace MGestures.App
{
    internal sealed class UserSettingsStore
    {
        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Disallow
        };

        private readonly string _path;

        private UserSettingsStore(string path, UserSettings settings)
        {
            _path = path;
            Settings = settings;
        }

        public UserSettings Settings { get; private set; }

        public static UserSettingsStore Load(
            string path,
            out bool createdNew,
            out bool recoveredBackup,
            out bool resetToDefaults)
        {
            createdNew = false;
            recoveredBackup = false;
            resetToDefaults = false;

            var backupPath = AtomicFile.GetBackupPath(path);
            var primaryExists = File.Exists(path);
            if (!primaryExists && !File.Exists(backupPath))
            {
                var created = new UserSettingsStore(path, new UserSettings());
                created.Save();
                createdNew = true;
                return created;
            }

            if (primaryExists)
            {
                try
                {
                    return new UserSettingsStore(path, ReadAndValidate(path));
                }
                catch (Exception exception) when (IsInvalidData(exception))
                {
                    AtomicFile.QuarantineCorruptFile(path);
                }
            }

            if (File.Exists(backupPath))
            {
                try
                {
                    var recovered = new UserSettingsStore(path, ReadAndValidate(backupPath));
                    recovered.Save();
                    recoveredBackup = true;
                    return recovered;
                }
                catch (Exception backupException) when (IsInvalidData(backupException))
                {
                    AtomicFile.QuarantineCorruptFile(backupPath);
                }
            }

            var reset = new UserSettingsStore(path, new UserSettings());
            reset.Save();
            resetToDefaults = true;
            return reset;
        }

        public static UserSettings Deserialize(Stream stream)
        {
            using var document = JsonDocument.Parse(stream);
            if (document.RootElement.ValueKind != JsonValueKind.Object ||
                !document.RootElement.TryGetProperty("schemaVersion", out var version) ||
                version.ValueKind != JsonValueKind.Number ||
                !version.TryGetInt32(out var schemaVersion) ||
                schemaVersion != UserSettings.CurrentSchemaVersion)
                throw new InvalidDataException("设置文件缺少有效的 schemaVersion");
            var settings = document.RootElement.Deserialize<UserSettings>(JsonOptions)
                ?? throw new InvalidDataException("设置文件内容为空");
            Validate(settings);
            return settings;
        }

        public void Replace(UserSettings settings)
        {
            Validate(settings);
            Settings = settings;
        }

        public UserSettings CreateSnapshot()
        {
            var json = JsonSerializer.Serialize(Settings, JsonOptions);
            return JsonSerializer.Deserialize<UserSettings>(json, JsonOptions)
                ?? throw new InvalidDataException("无法创建设置快照");
        }

        public void Save()
        {
            Validate(Settings);
            AtomicFile.WriteAllText(_path, JsonSerializer.Serialize(Settings, JsonOptions));
        }

        private static UserSettings ReadAndValidate(string path)
        {
            using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            return Deserialize(stream);
        }

        private static void Validate(UserSettings settings)
        {
            if (settings.SchemaVersion != UserSettings.CurrentSchemaVersion)
                throw new InvalidDataException($"不支持的设置版本: {settings.SchemaVersion}");

            const GestureTriggerButton validButtons =
                GestureTriggerButton.Right | GestureTriggerButton.Middle | GestureTriggerButton.X;
            if (settings.TriggerButton == GestureTriggerButton.None ||
                (settings.TriggerButton & ~validButtons) != 0)
                throw new InvalidDataException("手势触发键无效");

            if (settings.InitialValidMove < 0 || settings.InitialValidMove > 100)
                throw new InvalidDataException("初始移动距离必须在 0 到 100 之间");
            if (settings.StayTimeoutMilliseconds < 50 || settings.StayTimeoutMilliseconds > 10000)
                throw new InvalidDataException("停留超时必须在 50 到 10000 毫秒之间");
            if (settings.InitialStayTimeoutMilliseconds < 20 || settings.InitialStayTimeoutMilliseconds > 2000)
                throw new InvalidDataException("初始停留超时必须在 20 到 2000 毫秒之间");
            if (settings.PauseResumeHotKey == null ||
                (settings.PauseResumeHotKey.Length != 0 && settings.PauseResumeHotKey.Length != sizeof(ulong)))
                throw new InvalidDataException("暂停快捷键配置必须为空或 8 字节");
        }

        private static bool IsInvalidData(Exception exception)
        {
            return exception is JsonException or InvalidDataException;
        }
    }
}
