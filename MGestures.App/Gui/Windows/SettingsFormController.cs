using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MGestures.App.Gui.Windows.CommandViews;
using MGestures.App.Backup;
using MGestures.Common;
using MGestures.Common.Annotation;
using MGestures.Common.OsSpecific.Windows;
using MGestures.Common.Product;
using MGestures.Core;
using MGestures.Core.Annotations;
using MGestures.Core.Commands.Impl;
using MGestures.Core.Impl.Windows;
using MGestures.Core.Persistence;
using MGestures.Core.Persistence.Impl;
using MGestures.View.Impl.Windows;

namespace MGestures.App.Gui.Windows
{
    internal class SettingsFormController : IDisposable, INotifyPropertyChanged
    {
        private Form _form;

        public bool IsDisposed { get; private set; }

        private UserSettingsStore _config;
        private GestureParser _parser;
        private Win32MousePathTracker2 _pathTracker;
        private JsonGestureIntentStore _intentStore;
        private CanvasWindowGestureView _gestureView;
        private GlobalHotKeyManager _hotkeyMgr;

        public SettingsFormController(UserSettingsStore config, GestureParser parser,
            Win32MousePathTracker2 pathTracker, JsonGestureIntentStore intentStore,
            CanvasWindowGestureView gestureView, GlobalHotKeyManager hotkeyMgr)
        {
            _config = config;
            _parser = parser;
            _pathTracker = pathTracker;
            _intentStore = intentStore;
            _gestureView = gestureView;
            _hotkeyMgr = hotkeyMgr;

            #region 初始化支持的命令和命令视图
            //Add Command Types
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(DoNothingCommand)), typeof(DoNothingCommand));
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(HotKeyCommand)), typeof(HotKeyCommand));
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(WebSearchCommand)), typeof(WebSearchCommand));
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(WindowControlCommand)), typeof(WindowControlCommand));
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(TaskSwitcherCommand)), typeof(TaskSwitcherCommand));
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(OpenFileCommand)), typeof(OpenFileCommand));
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(SendTextCommand)), typeof(SendTextCommand));
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(GotoUrlCommand)), typeof(GotoUrlCommand));
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(CmdCommand)), typeof(CmdCommand));
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(ScriptCommand)), typeof(ScriptCommand));

            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(PauseMGesturesCommand)), typeof(PauseMGesturesCommand));
            SupportedCommands.Add(NamedAttribute.GetNameOf(typeof(ChangeAudioVolumeCommand)), typeof(ChangeAudioVolumeCommand));
            
            CommandViewFactory.Register<OpenFileCommand, OpenFileCommandView>();
            CommandViewFactory.Register<DoNothingCommand, GeneralNoParameterCommandView>();
            CommandViewFactory.Register<HotKeyCommand, HotKeyCommandView>();
            CommandViewFactory.Register<GotoUrlCommand, GotoUrlCommandView>();
            CommandViewFactory.Register<PauseMGesturesCommand, GeneralNoParameterCommandView>();
            CommandViewFactory.Register<WebSearchCommand, WebSearchCommandView>();
            CommandViewFactory.Register<WindowControlCommand, WindowControlCommandView>();
            CommandViewFactory.Register<CmdCommand, CmdCommandView>();
            CommandViewFactory.Register<SendTextCommand, SendTextCommandView>();
            CommandViewFactory.Register<TaskSwitcherCommand, TaskSwitcherCommandView>();
            CommandViewFactory.Register<ScriptCommand, ScriptCommandView>();
            CommandViewFactory.Register<ChangeAudioVolumeCommand, GeneralNoParameterCommandView>();
            #endregion

            #region Hotcorner 
            SupportedHotCornerCommands.Add(NamedAttribute.GetNameOf(typeof(DoNothingCommand)), typeof(DoNothingCommand));
            SupportedHotCornerCommands.Add(NamedAttribute.GetNameOf(typeof(HotKeyCommand)), typeof(HotKeyCommand));
            SupportedHotCornerCommands.Add(NamedAttribute.GetNameOf(typeof(CmdCommand)), typeof(CmdCommand));

            HotCornerCommandViewFactory.Register<DoNothingCommand, GeneralNoParameterCommandView>();
            HotCornerCommandViewFactory.Register<HotKeyCommand, HotKeyCommandView>();
            HotCornerCommandViewFactory.Register<CmdCommand, CmdCommandView>();
            #endregion

            _form = new SettingsForm(this);
        }


        #region tab1 Config Items
        
        public UserSettingsStore Config { get { return _config; } }

        public GlobalHotKeyManager.HotKey? PauseResumeHotkey
        {
            get
            {
                return _hotkeyMgr.GetRegisteredHotKeyById(ConfigKeys.PauseResumeHotKey);
            }

            set
            {

                if(value != null)
                {
                    if (PauseResumeHotkey != null && value.Value.Equals(PauseResumeHotkey.Value) ) return;
                    //use null action here cus we use _hotkeyMgr.HotKeyPreview event in Program class
                    _hotkeyMgr.RegisterHotKey(ConfigKeys.PauseResumeHotKey, value.Value, null);
                }else
                {
                    if (PauseResumeHotkey == null) return;
                    _hotkeyMgr.UnRegisterHotKey(ConfigKeys.PauseResumeHotKey);
                }

                _config.Settings.PauseResumeHotKey = value == null ? [] : value.Value.ToBytes();

                OnPropertyChanged("PauseResumeHotKey");
            }
        }

        /// <summary>
        /// 获取或设置是否开机自动运行
        /// </summary>
        public bool AutoStart
        {
            get
            {
                return _config.Settings.AutoStart;
            }
            set
            {
                if (value == AutoStart) return;

                try
                {
                    AutoStarter.SetAdminAutoStart(value, "MGesturesAutoStart");
                    //if (value) AutoStarter.Register(Constants.Identifier, Application.ExecutablePath);
                    //else AutoStarter.Unregister(Constants.Identifier);

                    _config.Settings.AutoStart = value;
                }
                catch (Exception)
                {
#if DEBUG                    
                    throw;
#endif
                }


                OnPropertyChanged("AutoStart");
            }
        }
        /// <summary>
        /// 获取或设置是否自动检查更新
        /// </summary>
        public bool AutoCheckForUpdate
        {
            get { return _config.Settings.AutoCheckForUpdate; }
            set
            {
                if (value == AutoCheckForUpdate) return;

                _config.Settings.AutoCheckForUpdate = value;

                OnPropertyChanged("AutoCheckForUpdate");
            }
        }
        /// <summary>
        /// 获取或设置哪一个鼠标按键作为手势键
        /// </summary>
        public GestureTriggerButton PathTrackerTriggerButton
        {
            get
            {
                return _pathTracker.TriggerButton;
            }

            set
            {
                if (value == PathTrackerTriggerButton) return;
                _pathTracker.TriggerButton = value;

                _config.Settings.TriggerButton = _pathTracker.TriggerButton;

                OnPropertyChanged("PathTrackerTriggerButton");
            }
        }

        public bool PathTrackerEnableWinKeyGesturing
        {
            get { return _pathTracker.EnableWindowsKeyGesturing; }
            set
            {
                Debug.WriteLine("Win: " + value);
                if (value == PathTrackerEnableWinKeyGesturing) return;
                _pathTracker.EnableWindowsKeyGesturing = value;

                _config.Settings.EnableWindowsKeyGesturing = value;
                OnPropertyChanged("PathTrackerEnableWinKeyGesturing");
            }
        }

        public bool GestureParserEnable8DirGesture
        {
            get { return _parser.Enable8DirGesture; }
            set
            {
                if (value == _parser.Enable8DirGesture) return;
                _parser.Enable8DirGesture = value;
                _config.Settings.EnableEightDirectionGestures = value;
                OnPropertyChanged("GestureParserEnable8DirGesture");
            }
        }

        /// <summary>
        /// 获取或设置出示有效移动距离
        /// </summary>
        public int PathTrackerInitialValidMove
        {
            get { return _pathTracker.InitialValidMove; }
            set
            {
                if (value == PathTrackerInitialValidMove) return;

                _pathTracker.InitialValidMove = value;
                _config.Settings.InitialValidMove = _pathTracker.InitialValidMove;

                OnPropertyChanged("PathTrackerInitialValidMove");
            }
        }

        public bool PathTrackerInitialStayTimeout
        {
            get { return _pathTracker.InitialStayTimeout; }
            set
            {
                if (value == PathTrackerInitialStayTimeout) return;

                _pathTracker.InitialStayTimeout = value;
                _config.Settings.InitialStayTimeout = value;

                OnPropertyChanged("PathTrackerInitialStayTimeout");
            }
        }

        public int PathTrackerInitalStayTimeoutMillis
        {
            get { return _pathTracker.InitialStayTimeoutMillis; }
            set
            {
                if (value == PathTrackerInitalStayTimeoutMillis) return;

                _pathTracker.InitialStayTimeoutMillis = value;
                _config.Settings.InitialStayTimeoutMilliseconds = value;

                OnPropertyChanged("PathTrackerInitalStayTimeoutMillis");
            }
        }

        public bool PathTrackerDisableInFullScreen
        {
            get { return _pathTracker.DisableInFullscreen; }
            set
            {
                if (value == PathTrackerDisableInFullScreen) return;

                _pathTracker.DisableInFullscreen = value;
                _config.Settings.DisableInFullscreen = value;

                OnPropertyChanged("PathTrackerDisableInFullScreen");
            }
        }

        /// <summary>
        /// 获取或设置鼠标是否允许停留超时
        /// </summary>
        public bool PathTrackerStayTimeout
        {
            get { return _pathTracker.StayTimeout; }
            set
            {
                if (value == PathTrackerStayTimeout) return;

                _pathTracker.StayTimeout = value;
                _config.Settings.StayTimeout = _pathTracker.StayTimeout;

                OnPropertyChanged("PathTrackerStayTimeout");
            }
        }
        /// <summary>
        /// 获取或设置停留超时的时间（毫秒）
        /// </summary>
        public int PathTrackerStayTimeoutMillis
        {
            get { return _pathTracker.StayTimeoutMillis; }
            set
            {
                if (value == PathTrackerStayTimeoutMillis) return;

                _pathTracker.StayTimeoutMillis = value;
                _config.Settings.StayTimeoutMilliseconds = _pathTracker.StayTimeoutMillis;

                OnPropertyChanged("PathTrackerStayTimeoutMillis");
            }
        }

        public bool PathTrackerPreferCursorWindow
        {
            get 
            {
                return _pathTracker.PreferWindowUnderCursorAsTarget;
            }
            set 
            {
                if (value == PathTrackerPreferCursorWindow) return;
                _pathTracker.PreferWindowUnderCursorAsTarget = value;
                _config.Settings.PreferWindowUnderCursor = value;

                OnPropertyChanged("PathTrackerPreferCursorWindow");
            }
        }
        /// <summary>
        /// 获取或设置是否显示手势路径
        /// </summary>
        public bool GestureViewShowPath
        {
            get { return _gestureView.ShowPath; }
            set
            {
                if (value == GestureViewShowPath) return;

                _gestureView.ShowPath = value;
                _config.Settings.ShowGesturePath = value;

                OnPropertyChanged("GestureViewShowPath");
            }
        }
        /// <summary>
        /// 获取或设置是否显示手示意图名称
        /// </summary>
        public bool GestureViewShowCommandName
        {
            get { return _gestureView.ShowCommandName; }
            set
            {
                if (value == GestureViewShowCommandName) return;

                _gestureView.ShowCommandName = value;
                _config.Settings.ShowCommandName = value;

                OnPropertyChanged("GestureViewShowCommandName");
            }
        }
        /// <summary>
        /// 获取或设置是否在手势执行后视图淡出
        /// </summary>
        public bool GestureViewFadeOut
        {
            get { return _gestureView.ViewFadeOut; }
            set
            {
                if (value == GestureViewFadeOut) return;

                _gestureView.ViewFadeOut = value;
                _config.Settings.FadeGesturePath = value;

                OnPropertyChanged("GestureViewFadeOut");
            }
        }
        /// <summary>
        /// 获取或设置手势被识别时轨迹显示的颜色
        /// </summary>
        public Color GestureViewMainPathColor
        {
            get { return _gestureView.PathMainColor; }
            set
            {
                if (value == GestureViewMainPathColor) return;

                _gestureView.PathMainColor = value;
                _config.Settings.MainPathColor = value.ToArgb();

                OnPropertyChanged("GestureViewMainPathColor");
            }
        }
        /// <summary>
        /// 获取或设置手势为被识别时轨迹的颜色
        /// </summary>
        public Color GestureViewAlternativePathColor
        {
            get { return _gestureView.PathAlternativeColor; }
            set
            {
                if (value == GestureViewAlternativePathColor) return;

                _gestureView.PathAlternativeColor = value;
                _config.Settings.AlternativePathColor = value.ToArgb();

                OnPropertyChanged("GestureViewAlternativePathColor");
            }
        }

        public Color GestureViewMiddleBtnMainColor
        {
            get { return _gestureView.PathMiddleBtnMainColor; }
            set
            {
                if (value == GestureViewMiddleBtnMainColor) return;

                _gestureView.PathMiddleBtnMainColor = value;
                _config.Settings.MiddleButtonPathColor = value.ToArgb();

                OnPropertyChanged("GestureViewMiddleBtnMainColor");
            }
        }

        public Color GestureVieXBtnMainColor
        {
            get { return _gestureView.PathXBtnMainColor; }
            set
            {
                if (value == GestureVieXBtnMainColor) return;

                _gestureView.PathXBtnMainColor = value;
                _config.Settings.XButtonPathColor = value.ToArgb();

                OnPropertyChanged("GestureVieXBtnMainColor");
            }
        }
        #endregion

        #region migrate methods

        internal void ExportTo(string filePath)
        {
            BackupService.ExportTo(filePath);
        }

        internal void RestoreDefaultGestures()
        {
            using var stream = File.OpenRead(AppSettings.DefaultGesturesFilePath);
            var defaults = new JsonGestureIntentStore(stream, closeStream: false);
            _intentStore.Import(defaults, replace: true);
            OnPropertyChanged("IntentStore");
        }

        internal void Import(BackupContents configAndGestures, bool importConfig, bool importGestures, bool mergeGestures)
        {
            var settingsSnapshot = _config.CreateSnapshot();
            var gesturesSnapshot = _intentStore.CreateSnapshot();

            try
            {
                if (importConfig)
                {
                    _config.Replace(configAndGestures.Settings);
                    ReApplySettings();
                }

                if (importGestures)
                {
                    _intentStore.Import(configAndGestures.GestureIntentStore, replace: !mergeGestures);
                    OnPropertyChanged("IntentStore");
                }

                if (importConfig) _config.Save();
                if (importGestures) _intentStore.Save();
            }
            catch (Exception importException)
            {
                _config.Replace(settingsSnapshot);
                _intentStore.Import(gesturesSnapshot, replace: true);
                var failures = new List<Exception> { importException };
                if (importConfig)
                {
                    try { ReApplySettings(); }
                    catch (Exception exception) { failures.Add(exception); }
                    try { _config.Save(); }
                    catch (Exception exception) { failures.Add(exception); }
                }
                if (importGestures)
                {
                    try { _intentStore.Save(); }
                    catch (Exception exception) { failures.Add(exception); }
                }
                if (failures.Count > 1)
                    throw new AggregateException("导入失败，部分恢复操作未成功，请检查磁盘或权限后重试。", failures);
                throw;
            }
        }

        private void ReApplySettings()
        {
            var settings = _config.Settings;
            AutoStarter.SetAdminAutoStart(settings.AutoStart, "MGesturesAutoStart");

            PathTrackerTriggerButton = settings.TriggerButton;
            PathTrackerInitialValidMove = settings.InitialValidMove;
            PathTrackerInitialStayTimeout = settings.InitialStayTimeout;
            PathTrackerInitalStayTimeoutMillis = settings.InitialStayTimeoutMilliseconds;
            PathTrackerPreferCursorWindow = settings.PreferWindowUnderCursor;
            PathTrackerDisableInFullScreen = settings.DisableInFullscreen;
            PathTrackerStayTimeout = settings.StayTimeout;
            PathTrackerStayTimeoutMillis = settings.StayTimeoutMilliseconds;
            PathTrackerEnableWinKeyGesturing = settings.EnableWindowsKeyGesturing;

            GestureViewShowPath = settings.ShowGesturePath;
            GestureViewShowCommandName = settings.ShowCommandName;
            GestureViewFadeOut = settings.FadeGesturePath;
            GestureViewMainPathColor = Color.FromArgb(settings.MainPathColor);
            GestureViewAlternativePathColor = Color.FromArgb(settings.AlternativePathColor);
            GestureViewMiddleBtnMainColor = Color.FromArgb(settings.MiddleButtonPathColor);
            GestureVieXBtnMainColor = Color.FromArgb(settings.XButtonPathColor);

            GestureParserEnableHotCorners = settings.EnableHotCorners;
            GestureParserEnable8DirGesture = settings.EnableEightDirectionGestures;
            GestureParserEnableRubEdges = settings.EnableRubEdges;

            PauseResumeHotkey = settings.PauseResumeHotKey.Length == 0
                ? null
                : GlobalHotKeyManager.HotKey.FromBytes(settings.PauseResumeHotKey);

        }

        #endregion

        #region tab2 props
        /// <summary>
        /// 获取支持的命令类型字典
        /// </summary>
        public Dictionary<string, Type> SupportedCommands { get; private set; } = new Dictionary<string, Type>();
        /// <summary>
        /// 获取命令视图工厂
        /// </summary>
        public ICommandViewFactory<CommandViewUserControl> CommandViewFactory { get; private set; } = new CommandViewFactory<CommandViewUserControl>() { EnableCaching = false };
        /// <summary>
        /// 获取内存中的手示意图存储结构
        /// </summary>
        public IGestureIntentStore IntentStore
        {
            get { return _intentStore; }
        }
        /// <summary>
        /// 获取手势解析器
        /// </summary>
        public GestureParser GestureParser { get { return _parser; } }

        #endregion

        #region hotcorners & edges
        public Dictionary<string, Type> SupportedHotCornerCommands { get; private set; } = new Dictionary<string, Type>();
        public ICommandViewFactory<CommandViewUserControl> HotCornerCommandViewFactory { get; private set; } = new CommandViewFactory<CommandViewUserControl>() { EnableCaching = false };

        public bool GestureParserEnableHotCorners
        {
            get { return _parser.EnableHotCorners; }
            set
            {
                if (value == _parser.EnableHotCorners) return;
                _parser.EnableHotCorners = value;
                Config.Settings.EnableHotCorners = value;
                OnPropertyChanged("GestureParserEnableHotCorners");
            }
        }

        public bool GestureParserEnableRubEdges
        {
            get { return _parser.EnableRubEdge; }
            set
            {
                if (value == _parser.EnableRubEdge) return;
                _parser.EnableRubEdge = value;
                Config.Settings.EnableRubEdges = value;
                OnPropertyChanged("GestureParserEnableRubEdges");
            }
        }

        #endregion
        /// <summary>
        /// 以Modal方式呈现主设置窗口
        /// </summary>
        public void ShowDialog()
        {
            _form.ShowDialog();

            using (var proc = Process.GetCurrentProcess())
            {
                Native.SetProcessWorkingSetSize(proc.Handle, -1, -1);
            }
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

        }

        internal void SaveUserData()
        {
            _config.Save();
            _intentStore.Save();
        }

        public void BringToFront()
        {
            _form.WindowState = FormWindowState.Normal;
            _form.Activate();
            _form.BringToFront();

        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (CommandViewFactory != null)
            {
                CommandViewFactory.Dispose();
            }

            _config = null;
            _parser = null;
            _gestureView = null;
            _intentStore = null;
            _pathTracker = null;

            SupportedCommands.Clear();
            SupportedCommands = null;
            CommandViewFactory = null;

            _form.Dispose();
            _form = null;

            //GC.Collect();
        }


    }
}
