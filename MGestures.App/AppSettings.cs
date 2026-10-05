using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MGestures.Common.Product;

namespace MGestures.App
{
    internal static class AppSettings
    {
        // 填写公开仓库地址后重新编译，例如 https://github.com/your-name/MGestures
        public const string GitHubRepositoryUrl = "https://github.com/JackZ2024/MGestures";

        public static string UserDataDirectory
        {
            get
            {
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    Application.ProductName);
            }
        }



        public static string ConfigFilePath
        {
            get { return Path.Combine(UserDataDirectory, "settings.json"); }
        }

        public static string GesturesFilePath
        {
            get { return Path.Combine(UserDataDirectory, "gestures.json"); }
        }

        public static string DefaultGesturesFilePath
        {
            get { return Path.Combine(Application.StartupPath, "defaults", "gestures.json"); }
        }


    }
}
