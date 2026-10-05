using MGestures.Core;

namespace MGestures.App
{
    internal sealed class UserSettings
    {
        public const int CurrentSchemaVersion = 1;

        public int SchemaVersion { get; set; } = CurrentSchemaVersion;
        public bool AutoStart { get; set; } = true;
        public bool AutoCheckForUpdate { get; set; } = true;
        public string SkippedUpdateVersion { get; set; } = "";
        // 仅接收此前每日检查版本保存的字段，避免严格配置校验将现有设置判为损坏。
        // 不再记录检查时间，也不将这两个废弃字段写入配置。
        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public System.DateTimeOffset? LastUpdateCheckUtc { get => null; set { } }
        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public string? LastUpdateCheckRepository { get => null; set { } }
        public GestureTriggerButton TriggerButton { get; set; } =
            GestureTriggerButton.Right | GestureTriggerButton.Middle | GestureTriggerButton.X;
        public int InitialValidMove { get; set; } = 4;
        public bool StayTimeout { get; set; }
        public int StayTimeoutMilliseconds { get; set; } = 500;
        public bool InitialStayTimeout { get; set; }
        public int InitialStayTimeoutMilliseconds { get; set; } = 200;
        public bool PreferWindowUnderCursor { get; set; } = true;
        public bool DisableInFullscreen { get; set; }
        public bool EnableWindowsKeyGesturing { get; set; }
        public bool ShowGesturePath { get; set; } = true;
        public bool ShowCommandName { get; set; } = true;
        public bool FadeGesturePath { get; set; } = true;
        public int MainPathColor { get; set; } = -14165512;
        public int AlternativePathColor { get; set; } = -10061415;
        public int MiddleButtonPathColor { get; set; } = -13771520;
        public int XButtonPathColor { get; set; } = -32704;
        public bool EnableHotCorners { get; set; } = true;
        public bool EnableEightDirectionGestures { get; set; } = true;
        public bool EnableRubEdges { get; set; } = true;
        public byte[] PauseResumeHotKey { get; set; } = [87, 0, 0, 0, 7, 0, 0, 0];
        public bool TrayIconVisible { get; set; } = true;
        public bool HasWarnedAbout360 { get; set; }
    }
}
