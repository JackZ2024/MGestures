using System;

namespace MGestures.Common.Product
{
    public class VersionInfo
    {
        public string Version { get; set; } = "";
        public string WhatsNew { get; set; } = "";
        public string ReleaseUrl { get; set; } = "";
        public string DownloadUrl { get; set; } = "";
        public long DownloadSize { get; set; }
        public string Sha256 { get; set; } = "";
        public bool CanAutoUpdate => DownloadSize > 0 && DownloadSize <= Updates.UpdatePackage.MaxDownloadBytes &&
            Sha256.Length == 64 && System.Text.RegularExpressions.Regex.IsMatch(Sha256, "^[0-9a-fA-F]{64}$") &&
            Uri.TryCreate(DownloadUrl, UriKind.Absolute, out var uri) && uri.Scheme == "https" && uri.Host == "github.com";

        public bool ShouldPrompt(string currentVersion, string? skippedVersion, bool manual)
        {
            return IsNewerThan(currentVersion) && (manual ||
                !TryParseVersion(skippedVersion ?? "", out var skipped) ||
                !TryParseVersion(Version, out var remote) || remote != skipped);
        }

        public string NormalizedVersion => TryParseVersion(Version, out var value)
            ? value!.ToString(4) : throw new InvalidOperationException("版本号无效。");

        public bool IsNewerThan(string currentVersion)
        {
            if (!TryParseVersion(Version, out var remote) || !TryParseVersion(currentVersion, out var current))
                throw new InvalidOperationException("无法识别软件版本号。");
            return remote > current;
        }

        public static bool TryParseVersion(string value, out System.Version? version)
        {
            value = value.Trim().TrimStart('v', 'V').Split('+')[0];
            version = null;
            if (!System.Version.TryParse(value, out var parsed)) return false;
            version = new System.Version(parsed.Major, parsed.Minor, Math.Max(0, parsed.Build), Math.Max(0, parsed.Revision));
            return true;
        }
    }
}
