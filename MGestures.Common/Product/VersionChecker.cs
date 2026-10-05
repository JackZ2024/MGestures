using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace MGestures.Common.Product
{
    /// <summary>Checks the latest published, non-prerelease GitHub Release without credentials.</summary>
    public sealed class VersionChecker : IDisposable
    {
        private readonly string? _repository;
        private readonly HttpClient _client;
        private CancellationTokenSource? _request;
        private bool _disposed;

        public VersionChecker(string? repository, int timeOutSeconds = 15, HttpMessageHandler? handler = null)
        {
            _repository = repository;
            _client = handler == null ? new HttpClient() : new HttpClient(handler);
            _client.Timeout = TimeSpan.FromSeconds(timeOutSeconds);
            _client.DefaultRequestHeaders.UserAgent.ParseAdd("MGestures/2.1");
            _client.DefaultRequestHeaders.Accept.ParseAdd("application/vnd.github+json");
            _client.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2022-11-28");
        }

        public event Action<VersionInfo>? Finished;
        public event Action? Canceled;
        public event Action<Exception>? ErrorHappened;
        public bool IsBusy => !_disposed && _request != null;

        public void CheckAsync()
        {
            if (_disposed || IsBusy) return;
            if (!Uri.TryCreate(_repository, UriKind.Absolute, out var repository) ||
                repository.Scheme != Uri.UriSchemeHttps || repository.Host != "github.com" ||
                !repository.IsDefaultPort || repository.Query.Length != 0 || repository.Fragment.Length != 0 ||
                repository.UserInfo.Length != 0 ||
                !Regex.IsMatch(repository.AbsolutePath, @"^/[A-Za-z0-9_-]+/[A-Za-z0-9_.-]+/?$"))
            {
                ErrorHappened?.Invoke(new InvalidOperationException(
                    "请先填写 GitHubRepositoryUrl，格式：https://github.com/用户名/仓库名。"));
                return;
            }
            var path = repository.AbsolutePath.TrimEnd('/');
            _request = new CancellationTokenSource();
            _ = CheckCoreAsync(new Uri("https://api.github.com/repos" + path + "/releases/latest"), _request);
        }

        private async Task CheckCoreAsync(Uri uri, CancellationTokenSource request)
        {
            VersionInfo? result = null;
            Exception? error = null;
            var canceled = false;
            try
            {
                using var response = await _client.GetAsync(uri, request.Token);
                if (response.StatusCode == HttpStatusCode.NotFound)
                    throw new InvalidOperationException("仓库不存在、不是公开仓库，或尚未发布正式 Release。");
                if (response.StatusCode is HttpStatusCode.Forbidden or HttpStatusCode.TooManyRequests)
                    throw new InvalidOperationException("GitHub 暂时拒绝访问或请求次数已达上限，请稍后再试。");
                response.EnsureSuccessStatusCode();
                var release = JObject.Parse(await response.Content.ReadAsStringAsync(request.Token));
                result = new VersionInfo
                {
                    Version = (string?)release["tag_name"] ?? "",
                    WhatsNew = (string?)release["body"] ?? "暂无更新说明。",
                    ReleaseUrl = (string?)release["html_url"] ?? ""
                };
                if ((bool?)release["draft"] == true || (bool?)release["prerelease"] == true ||
                    !VersionInfo.TryParseVersion(result.Version, out _) ||
                    !Uri.TryCreate(result.ReleaseUrl, UriKind.Absolute, out var page) ||
                    page.Scheme != "https" || page.Host != "github.com" || !page.IsDefaultPort ||
                    page.UserInfo.Length != 0 ||
                    !page.AbsolutePath.StartsWith(uri.AbsolutePath.Replace("/repos/", "/").Replace("/latest", "/tag/"), StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("Release 信息无效，请使用 v2.1.1 这样的正式版本标签。");
                // Only select a specifically named binary asset from this repository/release.
                var name = $"MGestures-{result.Version}-win-x64.zip";
                var matches = new System.Collections.Generic.List<JToken>();
                if (release["assets"] is JArray assets)
                    foreach (var asset in assets)
                        if (string.Equals((string?)asset["name"], name, StringComparison.OrdinalIgnoreCase)) matches.Add(asset);
                if (matches.Count == 1)
                {
                    var asset = matches[0];
                    var download = (string?)asset["browser_download_url"] ?? "";
                    var expected = "https://github.com" + uri.AbsolutePath.Replace("/repos/", "/").Replace("/releases/latest", "/releases/download/") + Uri.EscapeDataString(result.Version) + "/";
                    if (download.StartsWith(expected, StringComparison.OrdinalIgnoreCase) && (string?)asset["state"] == "uploaded")
                    {
                        result.DownloadUrl = download;
                        result.DownloadSize = (long?)asset["size"] ?? 0;
                        var digest = (string?)asset["digest"] ?? "";
                        result.Sha256 = digest.StartsWith("sha256:", StringComparison.OrdinalIgnoreCase) ? digest.Substring(7) : "";
                    }
                }
            }
            catch (OperationCanceledException)
            {
                canceled = request.IsCancellationRequested;
                if (!canceled) error = new TimeoutException("连接 GitHub 超时，请检查网络后重试。");
            }
            catch (Exception exception) { error = exception; }
            finally
            {
                _request = null;
                request.Dispose();
            }
            if (_disposed) return;
            if (canceled) Canceled?.Invoke();
            else if (error != null) ErrorHappened?.Invoke(error);
            else Finished?.Invoke(result!);
        }

        public void Cancel() { if (!_disposed) _request?.Cancel(); }
        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;
            _request?.Cancel();
            _client.Dispose();
        }
    }
}
