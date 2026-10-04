using MGestures.App.Gui.Windows;
using MGestures.App.Properties;
using MGestures.Common;
using MGestures.Common.IO;
using MGestures.Common.OsSpecific.Windows;
using MGestures.Common.Product;
using MGestures.Core;
using MGestures.Core.Impl.Windows;
using MGestures.Core.Persistence.Impl;
using MGestures.Core.Persistence.Impl.Windows;
using MGestures.View.Impl.Windows;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace MGestures.App
{
    static class Program
    {
        static Mutex mutext;
        static GestureParser gestureParser;

        static UserSettingsStore config;
        static CanvasWindowGestureView gestureView;

        static readonly IList<IDisposable> componentsToDispose = new List<IDisposable>();
        static SettingsFormController settingsFormController;

        static bool isFirstRun;
        static JsonGestureIntentStore intentStore;
        static Win32GestrueIntentFinder intentFinder;

        static NotifyIcon trayIcon;
        static GlobalHotKeyManager hotkeyMgr;
        static SynchronizationContext uiContext;
        static bool resumeAfterSessionUnlock;
        static bool resumeAfterPowerResume;
        
        //for adding hotkey
        static ToolStripMenuItem menuItem_pause;

        [STAThread]
        static void Main(string[] args)
        {
            Trace.Listeners.Add(new DetailedConsoleListener());

            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);

            if (IsDuplicateInstance())
            {
                //让主实例打开`设置`
                PostIpcCmd("ShowSettings");
                return;
            }

            AppWideInit();

            try
            {
                //加载配置文件，如果文件不存在或损坏，则加载默认配置文件
                LoadUserData();
                
                SyncAutoStartState();
                CheckAndDoFirstRunStuff();

                ConfigureComponents();
                SystemEvents.SessionSwitch += SystemEvents_SessionSwitch;
                SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
                StartParserThread();

                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
                GC.WaitForPendingFinalizers();

                //显示托盘图标
                ShowTrayIcon();

                //监听IPC消息
                StartIpcPipe();


                Application.Run();

            }
#if !DEBUG
            catch (Exception e)
            {
                ShowFatalError(e);
            }
#endif
            finally { Dispose(); }

            

        }

        //TODO: refactor out
        static void StartIpcPipe()
        {
            //todo: impl a set of IPC APIs to perform some cmds. eg.Pause/Resume, Quit...
            //todo: Temporay IPC mechanism.
            var synCtx = new WindowsFormsSynchronizationContext();//note: won't work with `SynchronizationContext.Current`
            var pipeThread = new Thread(() =>
            {
                while (true)
                {
                    using (var server = new System.IO.Pipes.NamedPipeServerStream("MGestures_IPC_API"))
                    {
                        server.WaitForConnection();

                        Debug.WriteLine("Clien Connected");
                        using (var reader = new StreamReader(server))
                        {
                            var cmd = reader.ReadLine();
                            Debug.WriteLine("Pipe CMD=" + cmd);

                            if (cmd == "ShowSettings")
                            {
                                synCtx.Post((s) =>
                                {
                                    Debug.WriteLine("Thread=" + Thread.CurrentThread.ManagedThreadId);
                                    ShowSettings();
                                }, null);
                            }
                        }
                    }
                }
            }, maxStackSize: 1) { IsBackground = true };
            pipeThread.Start();
        }

        static void PostIpcCmd(string cmd)
        {
            using (var pipeClient = new System.IO.Pipes.NamedPipeClientStream("MGestures_IPC_API"))
            {
                pipeClient.Connect();
                using (var writer = new StreamWriter(pipeClient) { AutoFlush = true })
                {
                    writer.WriteLine(cmd);
                }
            }
        }

        static void StartParserThread()
        {
            new Thread(() =>
            {
#if DEBUG
                gestureParser.Start();
#else
                try
                {
                    gestureParser.Start();
                }
                catch (Exception e)
                {
                    ShowFatalError(e);
                }
#endif
            }, maxStackSize: 1) {Name = "Parser线程", Priority = ThreadPriority.Highest, IsBackground = false}.Start();
        }

        static bool IsDuplicateInstance()
        {
            bool createdNew;
            mutext = new Mutex(true, Constants.Identifier, out createdNew);
            if (!createdNew)
            {
                mutext.Close();
                return true;
            }
            return false;
        }

        static void ShowFatalError(Exception e)
        {
            var frm = new ErrorForm() {Text = Application.ProductName};
            frm.ErrorText = e.ToString();
            frm.ShowDialog();
            Environment.Exit(1);
        }

        static void CheckAndDoFirstRunStuff()
        {
            if (isFirstRun)
            {
                //ShowQuickStartGuide(isFirstRun: true);
                Warning360Safe();
            }
        }

        static void AppWideInit()
        {
            Application.EnableVisualStyles();
            Native.SetProcessDPIAware();
            uiContext = new WindowsFormsSynchronizationContext();

            Thread.CurrentThread.IsBackground = false;
            Thread.CurrentThread.Name = "入口线程";

            using (var proc = Process.GetCurrentProcess())
            {
                //高优先级
                proc.PriorityClass = ProcessPriorityClass.High;
            }

            hotkeyMgr = new GlobalHotKeyManager();
        }
        
        static void LoadUserData()
        {
            Directory.CreateDirectory(AppSettings.UserDataDirectory);

            config = UserSettingsStore.Load(
                AppSettings.ConfigFilePath,
                out isFirstRun,
                out var recoveredSettingsBackup,
                out var resetSettings);

            var recoveryMessages = new List<string>();
            if (recoveredSettingsBackup)
                recoveryMessages.Add("设置文件已损坏，已从备份恢复。");
            else if (resetSettings)
                recoveryMessages.Add("设置文件及其备份均无法读取，已恢复默认设置。损坏文件已保留在用户数据目录。");

            if (!File.Exists(AppSettings.GesturesFilePath) &&
                !File.Exists(AtomicFile.GetBackupPath(AppSettings.GesturesFilePath)))
            {
                AtomicFile.Copy(AppSettings.DefaultGesturesFilePath, AppSettings.GesturesFilePath);
            }

            try
            {
                intentStore = new JsonGestureIntentStore(AppSettings.GesturesFilePath);
                if (intentStore.RecoveredFromBackup)
                    recoveryMessages.Add("手势文件已损坏，已从备份恢复。");
            }
            catch (InvalidDataException exception)
            {
                Trace.TraceWarning("手势文件无法恢复，已重置为默认值：" + exception.Message);
                AtomicFile.Copy(AppSettings.DefaultGesturesFilePath, AppSettings.GesturesFilePath);
                intentStore = new JsonGestureIntentStore(AppSettings.GesturesFilePath);
                recoveryMessages.Add("手势文件及其备份均无法读取，已恢复默认手势。损坏文件已保留在用户数据目录。");
            }

            if (recoveryMessages.Count > 0)
            {
                var message = string.Join(Environment.NewLine, recoveryMessages);
                Trace.TraceWarning(message);
                MessageBox.Show(message, "MGestures 配置恢复", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        static void ConfigureComponents()
        {
#region Create Components
            intentFinder = new Win32GestrueIntentFinder(intentStore);
            var pathTracker = new Win32MousePathTracker2();
            gestureParser = new GestureParser(pathTracker, intentFinder);

            gestureView = new CanvasWindowGestureView(gestureParser);

            componentsToDispose.Add(gestureParser);
            componentsToDispose.Add(gestureView);
            componentsToDispose.Add(pathTracker);
            componentsToDispose.Add(hotkeyMgr);
#endregion

#region pathTracker
            pathTracker.DisableInFullscreen = config.Settings.DisableInFullscreen;
            pathTracker.PreferWindowUnderCursorAsTarget = config.Settings.PreferWindowUnderCursor;
            pathTracker.TriggerButton = config.Settings.TriggerButton;
            pathTracker.InitialValidMove = config.Settings.InitialValidMove;
            pathTracker.StayTimeout = config.Settings.StayTimeout;
            pathTracker.StayTimeoutMillis = config.Settings.StayTimeoutMilliseconds;
            pathTracker.InitialStayTimeout = config.Settings.InitialStayTimeout;
            pathTracker.InitialStayTimeoutMillis = config.Settings.InitialStayTimeoutMilliseconds;
            pathTracker.RequestPauseResume += paused => RunOnUiThread(TogglePause);
            pathTracker.EnableWindowsKeyGesturing = config.Settings.EnableWindowsKeyGesturing;
            pathTracker.RequestShowHideTray += () => RunOnUiThread(ToggleTrayIconVisibility);
            
#endregion

#region gestureView
            gestureView.ShowPath = config.Settings.ShowGesturePath;
            gestureView.ShowCommandName = config.Settings.ShowCommandName;
            gestureView.ViewFadeOut = config.Settings.FadeGesturePath;
            gestureView.PathMainColor = Color.FromArgb(config.Settings.MainPathColor);
            gestureView.PathAlternativeColor = Color.FromArgb(config.Settings.AlternativePathColor);
            gestureView.PathMiddleBtnMainColor = Color.FromArgb(config.Settings.MiddleButtonPathColor);
            gestureView.PathXBtnMainColor = Color.FromArgb(config.Settings.XButtonPathColor);
            #endregion

            #region GestureParser
            gestureParser.EnableHotCorners = config.Settings.EnableHotCorners;
            gestureParser.Enable8DirGesture = config.Settings.EnableEightDirectionGestures;
            gestureParser.EnableRubEdge = config.Settings.EnableRubEdges;
            
#endregion
            //HOt key
            hotkeyMgr.HotKeyPreview += HotkeyMgr_HotKeyPreview;
            hotkeyMgr.HotKeyRegistered += HotkeyMgr_Updated;
            hotkeyMgr.HotKeyUnRegistered += HotkeyMgr_Updated;
            var pauseHotKey = config.Settings.PauseResumeHotKey;
            
            if (pauseHotKey != null && pauseHotKey.Length > 0)
            {
                var hotkey = GlobalHotKeyManager.HotKey.FromBytes(pauseHotKey);

                try
                {
                    hotkeyMgr.RegisterHotKey(ConfigKeys.PauseResumeHotKey, hotkey, null);
                }catch(InvalidOperationException e)
                {
                    Debug.WriteLine(e);

                    //ignore for now ?
                }
                
            }
        }

        private static void HotkeyMgr_Updated(string arg1, GlobalHotKeyManager.HotKey arg2)
        {
            UpdateTray();
        }

        static bool HotkeyMgr_HotKeyPreview(GlobalHotKeyManager mgr, string id, GlobalHotKeyManager.HotKey hk)
        {
            if(id == ConfigKeys.PauseResumeHotKey)
            {
                Debug.WriteLine("HotKey Pressed: " + hk);
                TogglePause();
                //menuItem_pause.Text = string.Format("{0} ({1})", gestureParser.IsPaused ? "继续" : "暂停" ,hk.ToString());

                return true; //Handled
            }

            return false;
        }

        static void TogglePause()
        {
            gestureParser.TogglePause();
        }

        static void ShowTrayIcon()
        {
            trayIcon = CreateNotifyIcon();
            EventHandler handleBalloon = (sender, args) =>
            {
                var timer = new Timer { Interval = 1000 };
                timer.Tick += (sender_1, args_1) =>
                {
                    timer.Stop();
                    trayIcon.Visible = config.Settings.TrayIconVisible;
                };
                timer.Start();
            };

            trayIcon.BalloonTipClosed += handleBalloon;
            trayIcon.BalloonTipClicked += handleBalloon;
            trayIcon.DoubleClick += (sender, args) => ShowSettings();
            
            if (isFirstRun)
            {
                //trayIcon.ShowBalloonTip(1000 * 10, "MGstures在这里", "双击图标打开设置，右击查看菜单", ToolTipIcon.Info);
            }
            else
            {
                if (!config.Settings.TrayIconVisible) //隐藏
                {
                    ToggleTrayIconVisibility();
                    //trayIcon.ShowBalloonTip(10* 1000, "MGestures图标将隐藏", "按 Shift+左键+中键 恢复\n再次运行MGestures可打开设置界面", ToolTipIcon.Info);
                }
            }
            //是否检查更新
            if (config.Settings.AutoCheckForUpdate)
            {
                var checkForUpdateTimer = new Timer { Interval = Constants.AutoCheckForUpdateInterval };

                checkForUpdateTimer.Tick += (sender, args) =>
                {
                    checkForUpdateTimer.Stop();
                    ScheduledUpdateCheck(sender, trayIcon);

                };
                checkForUpdateTimer.Start();
            }

            UpdateTray();
        }

        static void SystemEvents_SessionSwitch(object sender, SessionSwitchEventArgs e)
        {
            RunOnUiThread(() => HandleSessionSwitch(e));
        }

        static void HandleSessionSwitch(SessionSwitchEventArgs e)
        {
            switch (e.Reason)
            {
                case SessionSwitchReason.SessionLock:
                    Debug.WriteLine("SessionLock: pausing gesture tracking");
                    gestureView.ForceHide();
                    resumeAfterSessionUnlock = !gestureParser.IsPaused;
                    if (resumeAfterSessionUnlock) gestureParser.Pause();
                    break;

                case SessionSwitchReason.SessionUnlock:
                    Debug.WriteLine("SessionUnlock: resuming gesture tracking");
                    if (resumeAfterSessionUnlock) gestureParser.Resume();
                    resumeAfterSessionUnlock = false;
                    break;
            }
        }

        static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            RunOnUiThread(() => HandlePowerModeChanged(e));
        }

        static void HandlePowerModeChanged(PowerModeChangedEventArgs e)
        {
            if (e.Mode == PowerModes.Suspend)
            {
                Debug.WriteLine("PowerSuspend: pausing gesture tracking");
                gestureView.ForceHide();
                resumeAfterPowerResume = !gestureParser.IsPaused;
                if (resumeAfterPowerResume) gestureParser.Pause();
            }
            else if (e.Mode == PowerModes.Resume)
            {
                Debug.WriteLine("PowerResume: resuming gesture tracking");
                if (resumeAfterPowerResume) gestureParser.Resume();
                resumeAfterPowerResume = false;
            }
        }

        static void RunOnUiThread(Action action)
        {
            if (uiContext == null || SynchronizationContext.Current == uiContext)
            {
                action();
                return;
            }

            uiContext.Post(_ => action(), null);
        }

#region event handlers
        static void menuItem_settings_Click(object sender, EventArgs eventArgs)
        {
            ShowSettings();
        }

        static void menuItem_pause_Click(object sender, EventArgs eventArgs)
        {
            TogglePause();
        }

        static void menuItem_exit_Click(object sender, EventArgs e)
        {
            gestureParser.Stop();
            Application.ExitThread();
        }


#endregion

        //仅在启动一段时间后检查一次更新，
        static void ScheduledUpdateCheck(object sender, NotifyIcon tray)
        {
            if (!config.Settings.AutoCheckForUpdate) return;
            return;
            var checker = new VersionChecker(AppSettings.CheckForUpdateUrl);
            checker.Finished += info =>
            {
                var whatsNew = info.WhatsNew.Length > 50 ? info.WhatsNew.Substring(0, 50) : info.WhatsNew;
                
                if (info.Version != Application.ProductVersion)
                {
                    tray.BalloonTipClicked += (o, args) =>
                    {
                        if (info.Version == Application.ProductVersion) return;
                        using (var frm = new UpdateInfoForm(ConfigurationManager.AppSettings.Get(Constants.ProductHomePageAppSettingKey), info))
                        {
                            frm.ShowDialog();
                            tray.Visible = config.Settings.TrayIconVisible;
                        }
                    };
                    if (!tray.Visible)
                    {
                        tray.Visible = true;
                    }
                    
                    tray.ShowBalloonTip(1000 * 15, Application.ProductName + "新版本可用!", "版本:" + info.Version + "\n" + whatsNew, ToolTipIcon.Info);
                }

                checker.Dispose();
                checker = null;

                GC.Collect();
            };
            checker.ErrorHappened += e =>
            {
                Debug.WriteLine("Program.ScheduledUpdateCheck Error:" + e.Message);
                checker.Dispose();
                checker = null;

                GC.Collect();
            };

            checker.CheckAsync();
        }

        [Obsolete]
        static void ToggleTrayIconVisibility()
        {            
            //如果图标当前可见， 而config中设置的值是不可见， 则说明是临时显示; 如果不是临时显示， 才需要修改config
            if (!(trayIcon.Visible && !config.Settings.TrayIconVisible))
            {
                config.Settings.TrayIconVisible = !trayIcon.Visible;
                config.Save();
            }

            if(trayIcon.Visible)
            {
                trayIcon.ShowBalloonTip(10*1000, "MGestures图标将隐藏", "按 Shift+左键+中键 恢复显示\n再次运行程序可打开设置界面", ToolTipIcon.Info);
             }else
            {
                trayIcon.Visible = true;
            }
        }

        static void ShowSettings()
        {
            if (settingsFormController != null)
            {
                settingsFormController.BringToFront();
                return;
            }
            using (settingsFormController = new SettingsFormController(config, gestureParser,
                (Win32MousePathTracker2)gestureParser.PathTracker, intentStore, gestureView, hotkeyMgr))
            {
                //进程如果优先为Hight，设置窗口上执行手势会响应非常迟钝（原因不明）
               //using (var proc = Process.GetCurrentProcess()) proc.PriorityClass = ProcessPriorityClass.Normal;
                settingsFormController.ShowDialog();
                //using (var proc = Process.GetCurrentProcess()) proc.PriorityClass = ProcessPriorityClass.High;
            }
            settingsFormController = null;
        }

        //用配置信息去同步自启动
        static void SyncAutoStartState()
        {
            //var fact = AutoStarter.IsRegistered(Constants.Identifier, Application.ExecutablePath);

            bool fact = AutoStarter.IsTaskScheduled("MGesturesAutoStart");
            bool correctlyConfigured = fact && AutoStarter.HasExpectedAdminAutoStartSettings("MGesturesAutoStart");

            var conf = config.Settings.AutoStart;

            // 旧版本创建的任务即使存在，也可能带有“切换到电池即停止”的默认条件。
            // 自启动开启时，检测到旧任务便覆盖注册；关闭时仅在任务确实不存在时跳过。
            if (!isFirstRun && ((conf && correctlyConfigured) || (!conf && !fact))) return;

            try
            {
                AutoStarter.SetAdminAutoStart(conf, "MGesturesAutoStart");
                //可能被杀毒软件阻止
                //if (conf) AutoStarter.Register(Constants.Identifier, Application.ExecutablePath);
                //else
                //{
                //    AutoStarter.Unregister(Constants.Identifier);
                //}
            }
            catch (Exception)
            {
#if DEBUG
                throw;
#endif
            }
        }

        static void ShowQuickStartGuide(bool isFirstRun=false)
        {
            var t = new Thread(() =>
            {
                bool createdNew;
                var mut = new Mutex(true, Constants.Identifier + "QuickStartGuideWindow", out createdNew);
                if (!createdNew) return;
                
                using (var frm = new QuickStartGuideForm())
                {
                    Application.Run(frm);
                    mut.Close();
                }

                if(isFirstRun)
                {
                    //Open again to show settings
                    //Process.Start(Application.ExecutablePath);
                }

                GC.Collect();
            }) { IsBackground = true };

            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        static string GetPauseResumeHotkeyString()
        {
            var hk = hotkeyMgr.GetRegisteredHotKeyById(ConfigKeys.PauseResumeHotKey);

            if (hk != null) return hk.Value.ToString();

            return "";
        }

        static NotifyIcon CreateNotifyIcon()
        {
            var notifyIcon = new NotifyIcon();
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            notifyIcon.Text = Application.ProductName +" "+ version + " by Jack";

            var contextMenu1 = new ContextMenuStrip();

            var menuItem_exit = new ToolStripMenuItem() { Text = "退出" };
            menuItem_exit.Click += menuItem_exit_Click;

            menuItem_pause = new ToolStripMenuItem() { Text = "暂停" };
            menuItem_pause.Click += menuItem_pause_Click;

            var menuItem_settings = new ToolStripMenuItem() { Text = "设置" };
            menuItem_settings.Click += menuItem_settings_Click;

            //var menuItem_showQuickStart = new ToolStripMenuItem() { Text = "快速入门" };
            //menuItem_showQuickStart.Click += (sender, args) => ShowQuickStartGuide();

            /*var menuItem_toggleTray = new MenuItem() { Text = "隐藏 (Shift + 左键 + 中键)" };
            menuItem_toggleTray.Click += (sender, args) =>
            {
               ToggleTrayIconVisibility();
            };*/

            contextMenu1.Items.AddRange(new[] { /*menuItem_toggleTray, */menuItem_pause, new ToolStripMenuItem("-"), menuItem_settings,new ToolStripMenuItem("-"), menuItem_exit });
            notifyIcon.Icon = Resources.trayIcon;
            //notifyIcon.Text = Application.ProductName;
            notifyIcon.ContextMenuStrip = contextMenu1;
            notifyIcon.Visible = true;
            
            //todo: move out
            gestureParser.StateChanged += GestureParser_StateChanged;


            return notifyIcon;
        }

        private static void GestureParser_StateChanged(GestureParser.State s)
        {
            RunOnUiThread(UpdateTray);
        }

        private static void UpdateTray()
        {
            if (trayIcon == null) return;

            var hotKeyStr = GetPauseResumeHotkeyString();
            hotKeyStr = string.IsNullOrEmpty(hotKeyStr) ? "" : string.Format("({0})", hotKeyStr);
            if (gestureParser.IsPaused)
            {
                menuItem_pause.Text = "继续 " + hotKeyStr;
                trayIcon.Icon = Resources.trayIcon_bw;
            }
            else
            {
                menuItem_pause.Text = "暂停 " + hotKeyStr;
                trayIcon.Icon = Resources.trayIcon;
            }
        }

        static void Warning360Safe()
        {
            var proc360 = Process.GetProcessesByName("360Safe");
            var proc360Tray = Process.GetProcessesByName("360Tray");
            
            if(proc360.Length + proc360Tray.Length > 0)
            {
                using (var warn = new Warn360())
                {
                    warn.ShowDialog();
                }
            }
        }

        static void Dispose()
        {
            try
            {
                SystemEvents.SessionSwitch -= SystemEvents_SessionSwitch;
                SystemEvents.PowerModeChanged -= SystemEvents_PowerModeChanged;

                if (gestureParser != null) gestureParser.Stop();
                if (trayIcon != null) trayIcon.Dispose();

                foreach (var disposable in componentsToDispose)
                {
                    if (disposable != null) disposable.Dispose();
                }

                componentsToDispose.Clear();
                Resources.ResourceManager.ReleaseAllResources();
            }
            finally
            {
                if (mutext != null) mutext.ReleaseMutex();
            }
        }
    }
}
