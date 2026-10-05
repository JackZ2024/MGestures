using System.Reflection;
using System.Text;
using System.Text.Json.Nodes;
using MGestures.Common.IO;
using MGestures.Core.Persistence.Impl;

// Run with Visual Studio MSBuild (the app references Windows COM components).
var root = Path.Combine(Path.GetTempPath(), "MGestures-config-test-" + Guid.NewGuid().ToString("N"));
Directory.CreateDirectory(root);
var assembly = Assembly.Load("MGestures");
var settingsStoreType = assembly.GetType("MGestures.App.UserSettingsStore", true)!;
var backupService = assembly.GetType("MGestures.App.Backup.BackupService", true)!;
var settingsPath = Path.Combine(root, "settings.json");
var gesturesPath = Path.Combine(root, "gestures.json");
var backupPath = Path.Combine(root, "test.mgb");
var defaultGestures = File.ReadAllText(Path.Combine(AppContext.BaseDirectory, "defaults", "gestures.json"));
var passed = 0;
try
{
    var store = LoadSettings(out var flags);
    Check((bool)flags[1]! && !(bool)flags[2]!, "first launch creates settings");
    var settings = settingsStoreType.GetProperty("Settings")!.GetValue(store)!;
    settings.GetType().GetProperty("AutoStart")!.SetValue(settings, false);
    SaveSettings(store);
    SaveSettings(store);
    Check(File.Exists(settingsPath + ".bak"), "successive atomic saves keep backup");
    File.WriteAllText(settingsPath, "{broken");
    store = LoadSettings(out flags);
    Check((bool)flags[2]! && !AutoStart(store), "corrupt settings restore previous user value");
    File.Delete(settingsPath);
    store = LoadSettings(out flags);
    Check((bool)flags[2]!, "missing settings restore backup");

    using (var locked = new FileStream(settingsPath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
    {
        Expect<IOException>(() => LoadSettings(out _), "locked settings report IO error");
    }
    Check(File.Exists(settingsPath), "IO failure preserves primary");
    var validSettings = File.ReadAllText(settingsPath);
    foreach (var invalid in new[] { "{}", "{\"schemaVersion\":\"invalid\"}", "{\"schemaVersion\":99}", "{\"schemaVersion\":1,\"stayTimeoutMilliseconds\":-1}" })
    {
        using var stream = new MemoryStream(Encoding.UTF8.GetBytes(invalid));
        Expect<InvalidDataException>(() => Call(settingsStoreType.GetMethod("Deserialize")!, null, [stream]), "reject invalid settings schema/value");
    }

    AtomicFile.WriteAllText(gesturesPath, defaultGestures);
    var gestures = new JsonGestureIntentStore(gesturesPath);
    gestures.Save();
    File.WriteAllText(gesturesPath, "{broken");
    gestures = new JsonGestureIntentStore(gesturesPath);
    Check(gestures.RecoveredFromBackup, "corrupt gestures restore backup");
    File.Delete(gesturesPath);
    gestures = new JsonGestureIntentStore(gesturesPath);
    Check(gestures.RecoveredFromBackup, "missing gestures restore backup");
    Check(Directory.GetFiles(root, "*.corrupt-*").Length >= 2, "corrupt files are retained");
    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(defaultGestures)))
    {
        _ = new JsonGestureIntentStore(stream, false);
        Check(stream.CanRead, "leaveOpen preserves caller stream");
    }
    var malformed = JsonNode.Parse(defaultGestures)!;
    malformed["HotCornerCommands"] = new JsonArray();
    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(malformed.ToJsonString())))
        Expect<InvalidDataException>(() => new JsonGestureIntentStore(stream, false), "reject incorrect corner count");
    var malicious = defaultGestures.Replace("MGestures.Core.Commands.Impl.HotKeyCommand, MGestures.Core", "System.Version, System.Private.CoreLib", StringComparison.Ordinal);
    using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(malicious)))
        Expect<Newtonsoft.Json.JsonSerializationException>(() => new JsonGestureIntentStore(stream, false), "reject unknown polymorphic type");

    var export = backupService.GetMethod("ExportTo", BindingFlags.Static | BindingFlags.NonPublic)!;
    var import = backupService.GetMethod("Import")!;
    Call(export, null, [backupPath, settingsPath, gesturesPath]);
    Check(Call(import, null, [backupPath]) != null, "ZIP backup round trip");
    var savedBackup = File.ReadAllBytes(backupPath);
    File.WriteAllText(settingsPath, "{broken");
    try { Call(export, null, [backupPath, settingsPath, gesturesPath]); throw new Exception("invalid export was accepted"); }
    catch (Exception exception) when (exception.GetType().Name == "BackupException") { }
    Check(File.ReadAllBytes(backupPath).SequenceEqual(savedBackup), "failed export preserves existing backup");
    File.WriteAllText(settingsPath, validSettings);
    Console.WriteLine($"PASS: {passed} configuration checks");

    foreach (var address in new string?[] { null, "", "not-a-url", "/relative", "file:///C:/test" })
    {
        using var checker = new MGestures.Common.Product.VersionChecker(address);
        Exception? updateError = null;
        checker.ErrorHappened += exception => updateError = exception;
        checker.CheckAsync();
        Check(updateError is InvalidOperationException && !checker.IsBusy, "invalid update address reports error without request");
        checker.CheckAsync();
        checker.Dispose();
        checker.Dispose();
        checker.CheckAsync();
        Check(!checker.IsBusy, "disposed update checker is idempotent");
    }
    Console.WriteLine("PASS: missing/invalid update address, retry, and disposal checks");

    var versionInfo = new MGestures.Common.Product.VersionInfo { Version = "v2.1.1" };
    Check(versionInfo.IsNewerThan("2.1.0+abcdef"), "newer numeric version");
    versionInfo.Version = "v2.1.0";
    Check(!versionInfo.IsNewerThan("2.1.0.0+abcdef"), "same version ignores build metadata");
    versionInfo.Version = "2.0.9";
    Check(!versionInfo.IsNewerThan("2.1.0"), "older release does not trigger update");
    versionInfo.Version = "v2.10.0";
    Check(versionInfo.IsNewerThan("2.9.0"), "numeric not lexical comparison");
    Check(!MGestures.Common.Product.VersionInfo.TryParseVersion("v2.2.0-beta", out _), "reject prerelease tag");
    versionInfo.Version = "v2.1.1";
    Check(!versionInfo.ShouldPrompt("2.1.0", "2.1.1.0", false), "auto check suppresses normalized skipped version");
    Check(versionInfo.ShouldPrompt("2.1.0", "2.1.1", true), "manual check ignores skipped version");
    versionInfo.Version = "v2.1.2";
    Check(versionInfo.ShouldPrompt("2.1.0", "2.1.1.0", false), "later version prompts again");
    var skipProperty = settings.GetType().GetProperty("SkippedUpdateVersion")!;
    skipProperty.SetValue(settings, "2.1.1.0");
    // Save/load with a fresh store so this assertion does not depend on earlier recovery tests.
    var currentStore = LoadSettings(out _);
    var currentSettings = settingsStoreType.GetProperty("Settings")!.GetValue(currentStore)!;
    skipProperty.SetValue(currentSettings, "2.1.1.0");
    SaveSettings(currentStore);
    var reloadedStore = LoadSettings(out _);
    Check((string)skipProperty.GetValue(settingsStoreType.GetProperty("Settings")!.GetValue(reloadedStore)!)! == "2.1.1.0", "skip choice survives restart");

    foreach (var scenario in new[] { "success", "asset", "404", "403", "429", "malformed", "invalid-tag", "unsafe-url", "prerelease" })
    {
        using var checker = new MGestures.Common.Product.VersionChecker("https://github.com/example/MGestures", handler: new ReleaseTestHandler(scenario));
        var completion = new TaskCompletionSource<object>(TaskCreationOptions.RunContinuationsAsynchronously);
        checker.Finished += info => completion.TrySetResult(info);
        checker.ErrorHappened += error => completion.TrySetResult(error);
        checker.CheckAsync();
        var result = await completion.Task.WaitAsync(TimeSpan.FromSeconds(5));
        Check(!checker.IsBusy, "request clears busy flag");
        if (scenario is "success" or "asset")
        {
            Check(result is MGestures.Common.Product.VersionInfo info && info.Version == "v2.1.1" && info.WhatsNew == "Release notes" && info.ReleaseUrl.EndsWith("/tag/v2.1.1"), "GitHub release mapping");
            Check(((MGestures.Common.Product.VersionInfo)result).CanAutoUpdate == (scenario == "asset"), "auto update requires recognized asset and digest");
        }
        else
            Check(result is Exception, "GitHub failure reports error: " + scenario);
    }
    using (var checker = new MGestures.Common.Product.VersionChecker("https://github.com/example/MGestures", handler: new ReleaseTestHandler("cancel")))
    {
        var completion = new TaskCompletionSource<bool>(TaskCreationOptions.RunContinuationsAsynchronously);
        checker.Canceled += () => completion.TrySetResult(true);
        checker.CheckAsync();
        Check(checker.IsBusy, "pending request is busy");
        checker.CheckAsync(); // repeated clicks must not start another request
        checker.Cancel();
        Check(await completion.Task.WaitAsync(TimeSpan.FromSeconds(5)) && !checker.IsBusy, "cancel clears pending request");
    }
    Console.WriteLine("PASS: GitHub release mapping, errors, cancellation, and numeric version comparison");

    await CheckUpdatePackages();
    var payloadArgument = Array.IndexOf(args, "--payload");
    if (payloadArgument >= 0)
    {
        var releasePath = args[payloadArgument + 1];
        var expectedVersion = assembly.GetName().Version!.ToString(4);
        MGestures.Common.Updates.UpdatePackage.ValidatePayload(releasePath, expectedVersion);
        Check(true, "published release contains required files and matching version");
        Expect<InvalidDataException>(() => MGestures.Common.Updates.UpdatePackage.ValidatePayload(releasePath, "99.0.0.0"), "reject package/version mismatch");
        Console.WriteLine("PASS: published payload completeness and assembly version");
    }

    // Explicit opt-in: briefly installs real Windows hooks without suppressing input.
    if (args.Contains("--hooks"))
    {
        var hookType = typeof(JsonGestureIntentStore).Assembly.GetType(
            "MGestures.Core.Impl.Windows.MouseKeyboardHook", true)!;
        var threadField = hookType.GetField("_hookThread", BindingFlags.Instance | BindingFlags.NonPublic)!;
        var idField = hookType.GetField("_hookThreadNativeId", BindingFlags.Instance | BindingFlags.NonPublic)!;
        var install = hookType.GetMethod("Install")!;
        var uninstall = hookType.GetMethod("Uninstall")!;
        for (var mode = 0; mode < 3; mode++)
        {
            using var hook = (IDisposable)Activator.CreateInstance(hookType)!;
            if (mode == 1)
            {
                Func<MGestures.Common.OsSpecific.Windows.Native.MSG, bool> keepRunning = _ => true;
                hookType.GetEvent("GotMessage")!.AddEventHandler(hook, keepRunning);
            }
            Call(install, hook, null);
            Check(SpinWait.SpinUntil(() => (uint)idField.GetValue(hook)! != 0, 3000), "hook startup");
            var thread = (Thread)threadField.GetValue(hook)!;
            if (mode == 2)
                Task.WaitAll(Task.Run(() => Call(uninstall, hook, null)), Task.Run(() => Call(uninstall, hook, null)));
            else
                Call(uninstall, hook, null);
            Check(!thread.IsAlive, "close terminates hook thread");
            Call(uninstall, hook, null);
        }
        Console.WriteLine("PASS: hook shutdown without subscriber, with continuing subscriber, and with concurrent uninstall");
    }
}
finally
{
    if (Path.GetFullPath(root).StartsWith(Path.GetFullPath(Path.GetTempPath()), StringComparison.OrdinalIgnoreCase))
        Directory.Delete(root, true);
}

object LoadSettings(out object?[] flags)
{
    flags = [settingsPath, false, false, false];
    return Call(settingsStoreType.GetMethod("Load")!, null, flags)!;
}
bool AutoStart(object store)
{
    var settings = settingsStoreType.GetProperty("Settings")!.GetValue(store)!;
    return (bool)settings.GetType().GetProperty("AutoStart")!.GetValue(settings)!;
}
void SaveSettings(object store) => Call(settingsStoreType.GetMethod("Save")!, store, null);
void Check(bool condition, string name)
{
    if (!condition) throw new Exception("FAIL: " + name);
    passed++;
}
void Expect<T>(Action action, string name) where T : Exception
{
    try { action(); }
    catch (T) { passed++; return; }
    throw new Exception("FAIL: " + name);
}
static object? Call(MethodInfo method, object? target, object?[]? args)
{
    try { return method.Invoke(target, args); }
    catch (TargetInvocationException exception) when (exception.InnerException != null)
    {
        System.Runtime.ExceptionServices.ExceptionDispatchInfo.Capture(exception.InnerException).Throw();
        throw;
    }
}

async Task CheckUpdatePackages()
{
    var updateRoot = Path.Combine(root, "update-tests");
    Directory.CreateDirectory(updateRoot);
    var data = Encoding.UTF8.GetBytes("fake package bytes");
    var hash = Convert.ToHexString(System.Security.Cryptography.SHA256.HashData(data));
    using var client = new System.Net.Http.HttpClient(new BytesTestHandler(data));
    var archivePath = Path.Combine(updateRoot, "download.zip");
    await MGestures.Common.Updates.UpdatePackage.DownloadAsync(client, new Uri("https://github.com/example/MGestures/releases/download/v2.1.1/package.zip"), archivePath, data.Length, hash, null, CancellationToken.None);
    Check(File.ReadAllBytes(archivePath).SequenceEqual(data), "verified download writes expected bytes");
    try
    {
        await MGestures.Common.Updates.UpdatePackage.DownloadAsync(client, new Uri("https://github.com/example/MGestures/package.zip"), Path.Combine(updateRoot, "bad-hash.zip"), data.Length, new string('0', 64), null, CancellationToken.None);
        throw new Exception("bad hash accepted");
    }
    catch (InvalidDataException) { Check(true, "reject invalid SHA-256"); }
    try
    {
        await MGestures.Common.Updates.UpdatePackage.DownloadAsync(client, new Uri("https://github.com/example/MGestures/package.zip"), Path.Combine(updateRoot, "bad-size.zip"), data.Length + 1, hash, null, CancellationToken.None);
        throw new Exception("bad size accepted");
    }
    catch (InvalidDataException) { Check(true, "reject mismatched asset size"); }
    using (var canceled = new CancellationTokenSource())
    {
        canceled.Cancel();
        try
        {
            await MGestures.Common.Updates.UpdatePackage.DownloadAsync(client, new Uri("https://github.com/example/MGestures/package.zip"), Path.Combine(updateRoot, "canceled.zip"), data.Length, hash, null, canceled.Token);
            throw new Exception("canceled download was accepted");
        }
        catch (OperationCanceledException) { Check(true, "download supports cancellation"); }
        Check(!File.Exists(Path.Combine(updateRoot, "canceled.zip")), "canceled request creates no downloaded file");
    }
    foreach (var path in new[] { "../escape", "/absolute", "C:/absolute", "dir/../escape", "dir/file:stream", "dir/NUL.txt", "dir/file.", "dir/file ", "dir//file" })
        Expect<InvalidDataException>(() => MGestures.Common.Updates.UpdatePackage.SafePath(updateRoot, path), "reject unsafe path " + path);
    foreach (var entry in new[] { "../escape.txt", "settings.json", "gestures.json" })
    {
        var zipPath = Path.Combine(updateRoot, Guid.NewGuid().ToString("N") + ".zip");
        using (var zip = System.IO.Compression.ZipFile.Open(zipPath, System.IO.Compression.ZipArchiveMode.Create))
        using (var writer = new StreamWriter(zip.CreateEntry(entry).Open())) writer.Write("test");
        var extraction = Path.Combine(updateRoot, Guid.NewGuid().ToString("N"));
        Expect<InvalidDataException>(() => MGestures.Common.Updates.UpdatePackage.Extract(zipPath, extraction, CancellationToken.None), "reject invalid archive before extraction");
        Check(!Directory.Exists(extraction), "bad archive creates no payload");
    }
    var validZip = Path.Combine(updateRoot, "valid.zip");
    using (var zip = System.IO.Compression.ZipFile.Open(validZip, System.IO.Compression.ZipArchiveMode.Create))
    {
        using (var writer = new StreamWriter(zip.CreateEntry("app.txt").Open())) writer.Write("new app");
        using (var writer = new StreamWriter(zip.CreateEntry("sub/new.txt").Open())) writer.Write("new file");
    }
    var payload = Path.Combine(updateRoot, "payload");
    MGestures.Common.Updates.UpdatePackage.Extract(validZip, payload, CancellationToken.None);
    Check(File.ReadAllText(Path.Combine(payload, "sub/new.txt")) == "new file", "safe archive extraction");
    Expect<InvalidDataException>(() => MGestures.Common.Updates.UpdatePackage.ValidatePayload(payload, "2.1.1.0"), "reject incomplete release payload");
    var target = Path.Combine(updateRoot, "target");
    Directory.CreateDirectory(target);
    File.WriteAllText(Path.Combine(target, "app.txt"), "old app");
    File.WriteAllText(Path.Combine(target, "settings.json"), "keep config");
    var manifest = MGestures.Common.Updates.UpdateInstaller.CreateManifest(payload);
    MGestures.Common.Updates.UpdateInstaller.Apply(payload, target, Path.Combine(updateRoot, "recovery-ok"), manifest);
    Check(File.ReadAllText(Path.Combine(target, "app.txt")) == "new app" && File.ReadAllText(Path.Combine(target, "settings.json")) == "keep config", "replace declared files while preserving unrelated data");
    Check(File.ReadAllText(Path.Combine(updateRoot, "recovery-ok", "files", "app.txt")) == "old app", "keep original files for recovery");
    File.WriteAllText(Path.Combine(target, "app.txt"), "old app");
    File.Delete(Path.Combine(target, "sub/new.txt"));
    Expect<IOException>(() => MGestures.Common.Updates.UpdateInstaller.Apply(payload, target, Path.Combine(updateRoot, "recovery-fail"), manifest,
        () => throw new IOException("simulated restart failure")), "rollback on failed restart");
    Check(File.ReadAllText(Path.Combine(target, "app.txt")) == "old app" && !File.Exists(Path.Combine(target, "sub/new.txt")), "rollback restores old file and removes newly installed file");
    Check(File.ReadAllText(Path.Combine(updateRoot, "recovery-fail", "journal.json")).Contains("rolled-back"), "recovery journal records rollback");
    using (var locked = new FileStream(Path.Combine(target, "app.txt"), FileMode.Open, FileAccess.ReadWrite, FileShare.Read))
    {
        Expect<AggregateException>(() => MGestures.Common.Updates.UpdateInstaller.Apply(payload, target, Path.Combine(updateRoot, "recovery-locked"), manifest), "report locked file replacement/restore failure");
    }
    Check(File.ReadAllText(Path.Combine(target, "app.txt")) == "old app", "locked target retains original data");
    Check(File.Exists(Path.Combine(updateRoot, "recovery-locked", "files", "app.txt")), "retain backup when recovery cannot complete");
    File.WriteAllText(Path.Combine(payload, "app.txt"), "tampered");
    Expect<InvalidDataException>(() => MGestures.Common.Updates.UpdateInstaller.Apply(payload, target, Path.Combine(updateRoot, "recovery-tampered"), manifest), "reject staged file tampering");
    Check(File.ReadAllText(Path.Combine(target, "app.txt")) == "old app", "tampered payload leaves target unchanged");
    Console.WriteLine("PASS: skip persistence, download integrity, safe ZIP extraction, replacement and rollback");
}

sealed class BytesTestHandler(byte[] data) : System.Net.Http.HttpMessageHandler
{
    protected override Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(new System.Net.Http.HttpResponseMessage(System.Net.HttpStatusCode.OK)
        { RequestMessage = request, Content = new System.Net.Http.ByteArrayContent(data) });
    }
}

sealed class ReleaseTestHandler(string scenario) : System.Net.Http.HttpMessageHandler
{
    protected override async Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri?.AbsoluteUri != "https://api.github.com/repos/example/MGestures/releases/latest" ||
            !request.Headers.UserAgent.Any() || !request.Headers.Accept.Any())
            throw new Exception("incorrect GitHub endpoint or headers");
        if (scenario == "cancel") await Task.Delay(Timeout.Infinite, cancellationToken);
        var status = int.TryParse(scenario, out var code) ? (System.Net.HttpStatusCode)code : System.Net.HttpStatusCode.OK;
        var tag = scenario == "invalid-tag" ? "latest" : "v2.1.1";
        var url = scenario == "unsafe-url" ? "https://malicious.example/download" : "https://github.com/example/MGestures/releases/tag/v2.1.1";
        var body = scenario == "malformed" ? "{broken" : System.Text.Json.JsonSerializer.Serialize(new
        {
            tag_name = tag, body = "Release notes", html_url = url, draft = false, prerelease = scenario == "prerelease",
            assets = scenario == "asset" ? new[] { new { name = "MGestures-v2.1.1-win-x64.zip", state = "uploaded", size = 1234,
                digest = "sha256:" + new string('a', 64), browser_download_url = "https://github.com/example/MGestures/releases/download/v2.1.1/MGestures-v2.1.1-win-x64.zip" } } : []
        });
        return new System.Net.Http.HttpResponseMessage(status) { Content = new System.Net.Http.StringContent(body) };
    }
}
