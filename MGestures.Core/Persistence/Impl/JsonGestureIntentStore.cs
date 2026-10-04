using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using MGestures.Common.IO;
using MGestures.Core.Commands;

//using Newtonsoft.Json;

namespace MGestures.Core.Persistence.Impl
{

    public class JsonGestureIntentStore : IGestureIntentStore
    {
        public const int CurrentSchemaVersion = 1;

        public int SchemaVersion { get; private set; } = CurrentSchemaVersion;
        public Dictionary<string, ExeApp> Apps { get; set; } = null!;
        public GlobalApp GlobalApp { get; set; } = null!;
        public AbstractCommand[] HotCornerCommands { get; set; } = null!; //4 corners + 4 edges

        private readonly string? jsonPath;
        private readonly JsonSerializer ser = new JsonSerializer();

        public bool RecoveredFromBackup { get; private set; }

        public JsonGestureIntentStore(string jsonPath)
        {
            this.jsonPath = jsonPath;
            SetupSerializer();
            DeserializeWithBackupRecovery();
        }

        public JsonGestureIntentStore(Stream stream, bool closeStream)
        {
            SetupSerializer();
            Deserialize(stream, closeStream);
        }

        private void Deserialize(Stream stream, bool closeStream)
        {
            if(stream == null || !stream.CanRead) throw new ArgumentException("stream");
            try
            {
                using (var txtReader = new StreamReader(
                    stream,
                    Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: true,
                    bufferSize: 1024,
                    leaveOpen: !closeStream))
                using (var jsonReader = new JsonTextReader(txtReader))
                {
                    var result = ser.Deserialize<SerializeWrapper>(jsonReader)
                        ?? throw new InvalidDataException("手势文件内容为空");
                    Validate(result);

                    SchemaVersion = result.SchemaVersion;
                    GlobalApp = result.Global;
                    Apps = new Dictionary<string, ExeApp>(StringComparer.OrdinalIgnoreCase);

                    foreach(var a in result.Apps.Values)
                    {
                        if (string.IsNullOrWhiteSpace(a.ExecutablePath))
                            throw new InvalidDataException("手势文件包含空的应用程序路径");
                        Apps.Add(a.ExecutablePath, a);
                    }
                    HotCornerCommands = result.HotCornerCommands;
                }
            }
            finally
            {
                if (closeStream) stream.Dispose();
            }
        }

        private void Serialize()
        {
            if (string.IsNullOrWhiteSpace(jsonPath))
                throw new InvalidOperationException("未指定手势文件保存路径");

            var wrapper = new SerializeWrapper
            {
                Apps = Apps,
                SchemaVersion = CurrentSchemaVersion,
                Global = GlobalApp,
                HotCornerCommands = HotCornerCommands
            };
            Validate(wrapper);

            using var text = new StringWriter(CultureInfo.InvariantCulture);
            using (var writer = new JsonTextWriter(text))
            {
                ser.Serialize(writer, wrapper);
            }
            AtomicFile.WriteAllText(jsonPath, text.ToString());
        }

        private void DeserializeWithBackupRecovery()
        {
            var backupPath = AtomicFile.GetBackupPath(jsonPath!);
            if (!File.Exists(jsonPath) && File.Exists(backupPath))
            {
                RestoreBackup(backupPath);
                return;
            }
            try
            {
                DeserializeFile(jsonPath!);
            }
            catch (Exception exception) when (IsInvalidData(exception))
            {
                AtomicFile.QuarantineCorruptFile(jsonPath!);
                if (!File.Exists(backupPath)) throw new InvalidDataException("手势文件已损坏且没有可用备份", exception);

                RestoreBackup(backupPath);
            }
        }

        private void RestoreBackup(string backupPath)
        {
            try
            {
                DeserializeFile(backupPath);
            }
            catch (Exception exception) when (IsInvalidData(exception))
            {
                AtomicFile.QuarantineCorruptFile(backupPath);
                throw new InvalidDataException("手势备份已损坏", exception);
            }
            RecoveredFromBackup = true;
            Save();
        }

        private void DeserializeFile(string path)
        {
            using var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            Deserialize(file, false);
        }

        private void SetupSerializer()
        {
            ser.Formatting = Formatting.None;
            ser.TypeNameHandling = TypeNameHandling.Auto;
            ser.SerializationBinder = new KnownTypesBinder();
            ser.CheckAdditionalContent = true;
            ser.MaxDepth = 64;
            ser.Converters.Add(new GestureIntentConverter());
        }


        public bool TryGetExeApp(string key, out ExeApp found)
        {
            return Apps.TryGetValue(key, out found);
        }

        public ExeApp GetExeApp(string key)
        {
            return Apps[key];
        }


        public void Remove(string key)
        {
            Apps.Remove(key);
        }

        public void Remove(ExeApp app)
        {
            Remove(app.ExecutablePath);
        }

        public void Add(ExeApp app)
        {
            Apps.Add(app.ExecutablePath, app);
        }

        public void Save()
        {
            Serialize();

        }

        public JsonGestureIntentStore CreateSnapshot()
        {
            var wrapper = new SerializeWrapper
            {
                Apps = Apps,
                SchemaVersion = CurrentSchemaVersion,
                Global = GlobalApp,
                HotCornerCommands = HotCornerCommands
            };
            Validate(wrapper);

            using var text = new StringWriter(CultureInfo.InvariantCulture);
            using (var writer = new JsonTextWriter(text))
            {
                ser.Serialize(writer, wrapper);
            }

            using var stream = new MemoryStream(Encoding.UTF8.GetBytes(text.ToString()));
            return new JsonGestureIntentStore(stream, closeStream: false);
        }

        public void Import(JsonGestureIntentStore from, bool replace=false)
        {
            if (from == null) return;

            if (replace)
            {
                GlobalApp.GestureIntents.Clear();
                GlobalApp.IsGesturingEnabled = from.GlobalApp.IsGesturingEnabled;
                Apps.Clear();
                HotCornerCommands = from.HotCornerCommands;
            }else
            {
                for(var i=0; i<from.HotCornerCommands.Length; i++)
                {
                    if(from.HotCornerCommands[i] != null)
                    {
                        HotCornerCommands[i] = from.HotCornerCommands[i];
                    }
                }
            }
            
            GlobalApp.ImportGestures(from.GlobalApp);
            
            foreach (var kv in from.Apps)
            {
                ExeApp appInSelf;
                //如果应用程序已经在列表中，则合并手势
                if (TryGetExeApp(kv.Key, out appInSelf))
                {
                    appInSelf.ImportGestures(kv.Value);
                    appInSelf.IsGesturingEnabled = appInSelf.IsGesturingEnabled && kv.Value.IsGesturingEnabled;
                }
                else//否则将app添加到列表中
                {
                    Add(kv.Value);
                }
            }
        }

        public IEnumerator<ExeApp> GetEnumerator()
        {
            return Apps.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal class SerializeWrapper
        {
            public int SchemaVersion { get; set; }
            public Dictionary<string, ExeApp> Apps { get; set; } = null!;
            public GlobalApp Global { get; set; } = null!;
            public AbstractCommand[] HotCornerCommands { get; set; } = new AbstractCommand[8];
        }

        private static void Validate(SerializeWrapper wrapper)
        {
            if (wrapper.SchemaVersion != CurrentSchemaVersion)
                throw new InvalidDataException($"不支持的手势文件版本: {wrapper.SchemaVersion}");
            if (wrapper.Apps == null) throw new InvalidDataException("手势文件缺少 Apps");
            if (wrapper.Global == null) throw new InvalidDataException("手势文件缺少 Global");
            if (wrapper.HotCornerCommands == null || wrapper.HotCornerCommands.Length != 8)
                throw new InvalidDataException("触发角和摩擦边命令必须恰好包含 8 项");

            ValidateApp(wrapper.Global, "Global");
            foreach (var pair in wrapper.Apps)
            {
                if (pair.Value == null)
                    throw new InvalidDataException($"应用程序手势为空: {pair.Key}");
                ValidateApp(pair.Value, pair.Key);
                if (string.IsNullOrWhiteSpace(pair.Value.ExecutablePath))
                    throw new InvalidDataException("应用程序路径不能为空");
            }
        }

        private static void ValidateApp(AbstractApp app, string name)
        {
            if (app.GestureIntents == null)
                throw new InvalidDataException($"手势列表为空: {name}");

            foreach (var intent in app.GestureIntents.Values)
            {
                if (intent?.Gesture == null || intent.Command == null || intent.Gesture.Dirs == null)
                    throw new InvalidDataException($"手势内容不完整: {name}");
                if (intent.Gesture.Dirs.Any(direction => !Enum.IsDefined(direction)))
                    throw new InvalidDataException($"手势方向无效: {name}");
            }
        }

        private static bool IsInvalidData(Exception exception)
        {
            return exception is JsonException or InvalidDataException or ArgumentException;
        }

        internal class GestureIntentConverter : JsonConverter
        {
            public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
            {
                var dict = value as GestureIntentDict
                    ?? throw new JsonSerializationException("手势列表类型无效");
                serializer.Serialize(writer, dict.Values.ToList());
            }

            public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
            {
                var dict = new GestureIntentDict();
                var list = serializer.Deserialize<List<GestureIntent>>(reader)
                    ?? throw new JsonSerializationException("手势列表不能为空");
                foreach (var i in list)
                {
                    if (i?.Gesture?.Dirs == null || i.Command == null)
                        throw new JsonSerializationException("手势内容不完整");
                    dict.Add(i);
                }

                return dict;
            }

            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(GestureIntentDict);
            }
        }

        private sealed class KnownTypesBinder : ISerializationBinder
        {
            private static readonly HashSet<string> AllowedTypes = new(StringComparer.Ordinal)
            {
                "MGestures.App.Gui.Model.OrderableExeApp",
                "MGestures.App.Gui.Model.OrderableIntent",
                "MGestures.Core.Commands.Impl.ChangeAudioVolumeCommand",
                "MGestures.Core.Commands.Impl.CmdCommand",
                "MGestures.Core.Commands.Impl.DoNothingCommand",
                "MGestures.Core.Commands.Impl.GotoUrlCommand",
                "MGestures.Core.Commands.Impl.HotKeyCommand",
                "MGestures.Core.Commands.Impl.OpenFileCommand",
                "MGestures.Core.Commands.Impl.PauseMGesturesCommand",
                "MGestures.Core.Commands.Impl.ScriptCommand",
                "MGestures.Core.Commands.Impl.SendTextCommand",
                "MGestures.Core.Commands.Impl.TaskSwitcherCommand",
                "MGestures.Core.Commands.Impl.WebSearchCommand",
                "MGestures.Core.Commands.Impl.WindowControlCommand"
            };

            public Type BindToType(string? assemblyName, string typeName)
            {
                EnsureAllowed(assemblyName, typeName);
                return Type.GetType($"{typeName}, {assemblyName}", throwOnError: true)!;
            }

            public void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
            {
                assemblyName = serializedType.Assembly.GetName().Name;
                typeName = serializedType.FullName;
                EnsureAllowed(assemblyName, typeName);
            }

            private static void EnsureAllowed(string? assemblyName, string? typeName)
            {
                if (string.IsNullOrWhiteSpace(assemblyName) || string.IsNullOrWhiteSpace(typeName))
                    throw new JsonSerializationException("手势命令类型信息不完整");

                var shortAssemblyName = new AssemblyName(assemblyName).Name;
                var expectedAssembly = typeName.StartsWith("MGestures.App.", StringComparison.Ordinal)
                    ? "MGestures"
                    : "MGestures.Core";

                if (!AllowedTypes.Contains(typeName) ||
                    !string.Equals(shortAssemblyName, expectedAssembly, StringComparison.Ordinal))
                    throw new JsonSerializationException($"不允许的手势命令类型: {typeName}");
            }
        }
    }
}
