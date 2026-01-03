using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

            if (enable)
            {
                // 构造 schtasks 命令
                // /Create : 创建
                // /TN : 任务名称
                // /TR : 运行路径 (注意路径可能有空格，需要加转义引号)
                // /SC ONLOGON : 用户登录时触发
                // /RL HIGHEST : 关键点！使用最高权限运行（绕过UAC）
                // /F : 强制覆盖已存在的任务
                cmdArgs = $"/Create /TN \"{taskName}\" /TR \"\\\"{exePath}\\\"\" /SC ONLOGON /RL HIGHEST /F";
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
