using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using IWshRuntimeLibrary;

namespace MGestures.Common.OsSpecific.Windows
{
    public static class AutoStarter
    {
        //private const string RunLocation = @"Software\Microsoft\Windows\CurrentVersion\Run";

        static string MakeShortcutPath(string identifier)
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Startup) + @"\" + identifier + ".lnk";
        }

        public static void Register(string identifier, string appPath)
        {
            Unregister(identifier);
            CreateShortcut(MakeShortcutPath(identifier), appPath);

            //var key = Registry.CurrentUser.CreateSubKey(RunLocation);
            //key.SetValue(identifier, appPath);
            /*using (var ts = new Microsoft.Win32.TaskScheduler.TaskService())
            {
                var userId = WindowsIdentity.GetCurrent().Name;

                var task = ts.NewTask();
                task.RegistrationInfo.Description = identifier;
                task.Settings.DisallowStartIfOnBatteries = false;
                task.Settings.ExecutionTimeLimit = TimeSpan.Zero;
                task.Settings.Hidden = false;

                task.Principal.LogonType = TaskLogonType.InteractiveToken;
                task.Principal.UserId = userId;

                task.Principal.RunLevel = TaskRunLevel.Highest;
                task.Settings.Priority = ProcessPriorityClass.High;

                task.Triggers.Add(new LogonTrigger());
                task.Actions.Add(new ExecAction(appPath, "",workingDirectory));

                ts.RootFolder.RegisterTaskDefinition(identifier, task, 
                    TaskCreation.CreateOrUpdate, userId, 
                    LogonType: TaskLogonType.InteractiveToken);
            }*/
        }

        public static void Unregister(string identifier)
        {
            System.IO.File.Delete(MakeShortcutPath(identifier));

            //ensure removing registry item added in older versions
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");

            key.DeleteValue(identifier,throwOnMissingValue: false);
            /* using (var ts = new TaskService())
             {
                 ts.RootFolder.DeleteTask(identifier);
             }*/
        }

        public static bool IsRegistered(string identifier,string appPath)
        {
            return System.IO.File.Exists(MakeShortcutPath(identifier));
            /*var key = Registry.CurrentUser.OpenSubKey(RunLocation);
            if (key == null)
                return false;

            var value = (string)key.GetValue(identifier);
            if (value == null)
                return false;

            return (value == appPath);*/
            /*
            using (var ts = new TaskService())
            {
                return ts.RootFolder.Tasks.Exists(identifier);
            }*/
        }

        public static void CreateShortcut(string shortcutPath, string targetFileLocation)
        {
            try
            {
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);

                shortcut.TargetPath = targetFileLocation;                 // The path of the file that will launch when the shortcut is run
                shortcut.Save();
            }catch(Exception e)
            {
                Debug.WriteLine(e);
                //may be intercepted by 360 etc. ignore...
            }

                                
        }

        /// <summary>
        /// 检查指定的任务计划是否存在
        /// </summary>
        /// <param name="taskName">任务名称</param>
        /// <returns>存在返回 true，不存在返回 false</returns>
        public static bool IsTaskScheduled(string taskName)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "schtasks",
                    // /Query : 查询
                    // /TN : 任务名称
                    // /FO CSV : (可选) 输出格式，这里其实只看退出代码即可，不加也没关系
                    Arguments = $"/Query /TN \"{taskName}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    //process.WaitForFinished();
                    process.WaitForExit();

                    // 关键点：
                    // ExitCode == 0 表示命令执行成功，即找到了该任务。
                    // ExitCode != 0 (通常是1) 表示系统找不到指定的文件（任务不存在）。
                    return process.ExitCode == 0;
                }
            }
            catch (Exception)
            {
                // 如果执行过程中出现异常（例如找不到 schtasks.exe），默认视为没有自启
                return false;
            }
        }

        /// <summary>
        /// 判断现有任务是否具备常驻托盘程序需要的关键设置。
        /// 不能只检查任务是否存在：旧版本创建的任务可能会在切换到电池时被系统终止。
        /// </summary>
        public static bool HasExpectedAdminAutoStartSettings(string taskName)
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "schtasks",
                    Arguments = $"/Query /TN \"{taskName}\" /XML",
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (var process = Process.Start(startInfo))
                {
                    var xml = process.StandardOutput.ReadToEnd();
                    process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    return process.ExitCode == 0
                        && xml.Contains("<DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>")
                        && xml.Contains("<StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>")
                        && xml.Contains("<ExecutionTimeLimit>PT0S</ExecutionTimeLimit>");
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// 设置或取消管理员身份开机自启动
        /// </summary>
        /// <param name="enable">true: 开启自启; false: 关闭自启</param>
        /// <param name="taskName">任务名称（建议用英文，需唯一，例如 "MyAppNameAutoRun"）</param>
        public static void SetAdminAutoStart(bool enable, string taskName)
        {
            // 1. 获取当前程序可执行文件的完整路径
            string exePath = Process.GetCurrentProcess().MainModule.FileName;

            // 2. 检查是否已经是管理员（只有管理员才能创建最高权限任务）
            if (!IsAdministrator())
            {
                MessageBox.Show("设置开机自启动需要管理员权限，请以管理员身份运行此程序。", "权限不足", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string cmdArgs;
            string taskXmlPath = null;

            if (enable)
            {
                // schtasks 的命令行无法完整指定电源条件和 ExecutionTimeLimit。
                // 使用 XML 注册，避免默认设置在切换到电池或运行满 72 小时时结束常驻程序。
                taskXmlPath = Path.Combine(Path.GetTempPath(), $"{taskName}-{Guid.NewGuid():N}.xml");
                System.IO.File.WriteAllText(taskXmlPath, CreateAdminAutoStartTaskXml(exePath));
                cmdArgs = $"/Create /TN \"{taskName}\" /XML \"{taskXmlPath}\" /F";
            }
            else
            {
                // 删除任务
                cmdArgs = $"/Delete /TN \"{taskName}\" /F";
            }

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "schtasks",
                    Arguments = cmdArgs,
                    CreateNoWindow = true,      // 不显示黑框
                    UseShellExecute = false,    // 不使用Shell启动
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (Process process = Process.Start(startInfo))
                {
                    //process.WaitForFinished();
                    process.WaitForExit();

                    // 可选：读取输出查看是否成功
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    if (process.ExitCode == 0)
                    {
                        // Console.WriteLine("操作成功"); 
                    }
                    else
                    {
                        // 记录日志或提示
                        MessageBox.Show($"设置自启动失败: {error}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"执行命令出错: {ex.Message}", "异常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (taskXmlPath != null && System.IO.File.Exists(taskXmlPath)) System.IO.File.Delete(taskXmlPath);
            }
        }

        private static string CreateAdminAutoStartTaskXml(string exePath)
        {
            var userSid = WindowsIdentity.GetCurrent().User?.Value
                ?? throw new InvalidOperationException("无法获取当前用户的 SID。");
            var escapedExePath = SecurityElement.Escape(exePath);
            var escapedWorkingDirectory = SecurityElement.Escape(Path.GetDirectoryName(exePath) ?? AppDomain.CurrentDomain.BaseDirectory);
            var startBoundary = DateTime.Now.ToString("s", CultureInfo.InvariantCulture);

            return $@"<?xml version=""1.0"" encoding=""UTF-16""?>
<Task version=""1.2"" xmlns=""http://schemas.microsoft.com/windows/2004/02/mit/task"">
  <RegistrationInfo><Date>{startBoundary}</Date><Author>{SecurityElement.Escape(WindowsIdentity.GetCurrent().Name)}</Author></RegistrationInfo>
  <Principals><Principal id=""Author""><UserId>{userSid}</UserId><LogonType>InteractiveToken</LogonType><RunLevel>HighestAvailable</RunLevel></Principal></Principals>
  <Settings>
    <MultipleInstancesPolicy>IgnoreNew</MultipleInstancesPolicy>
    <DisallowStartIfOnBatteries>false</DisallowStartIfOnBatteries>
    <StopIfGoingOnBatteries>false</StopIfGoingOnBatteries>
    <AllowHardTerminate>true</AllowHardTerminate>
    <StartWhenAvailable>true</StartWhenAvailable>
    <RunOnlyIfNetworkAvailable>false</RunOnlyIfNetworkAvailable>
    <IdleSettings><StopOnIdleEnd>false</StopOnIdleEnd><RestartOnIdle>false</RestartOnIdle></IdleSettings>
    <AllowStartOnDemand>true</AllowStartOnDemand>
    <Enabled>true</Enabled>
    <Hidden>false</Hidden>
    <RunOnlyIfIdle>false</RunOnlyIfIdle>
    <WakeToRun>false</WakeToRun>
    <ExecutionTimeLimit>PT0S</ExecutionTimeLimit>
    <Priority>7</Priority>
    <RestartOnFailure><Interval>PT1M</Interval><Count>3</Count></RestartOnFailure>
  </Settings>
  <Triggers><LogonTrigger><StartBoundary>{startBoundary}</StartBoundary><Enabled>true</Enabled></LogonTrigger></Triggers>
  <Actions Context=""Author""><Exec><Command>{escapedExePath}</Command><WorkingDirectory>{escapedWorkingDirectory}</WorkingDirectory></Exec></Actions>
</Task>";
        }

        /// <summary>
        /// 检查当前是否拥有管理员权限
        /// </summary>
        private static bool IsAdministrator()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }
    }
}
