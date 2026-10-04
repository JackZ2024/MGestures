using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MGestures.Common.Product;

namespace MGestures.App
{
    internal static class AppSettings
    {
        public static string CheckForUpdateUrl
        {
            get
            {

#if DEBUG
                return ConfigurationManager.AppSettings.Get(Constants.CheckForUpdateUrlAppSettingKey);// "http://localhost:1226/projects/latestVersion?product=MGestures";

#else
                return ConfigurationManager.AppSettings.Get(Constants.CheckForUpdateUrlAppSettingKey);

#endif
            }
        }

        public static string ProductHomePage
        {
            get { return ConfigurationManager.AppSettings.Get(Constants.ProductHomePageAppSettingKey); }
        }

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
