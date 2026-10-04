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
