using System;
using System.Collections.Generic;
using System.Configuration;
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
            get { return Application.LocalUserAppDataPath; }
        }



        public static string ConfigFilePath
        {
            get { return UserDataDirectory + @"\config.plist"; }
        }

        public static string GesturesFilePath
        {
            get { return UserDataDirectory + @"\gestures.json"; }
        }

        public static string DefaultGesturesFilePath
        {
            get { return Application.StartupPath + @"\defaults\gestures.json"; }
        }


        public static string ConfigFileVersion
        {
            get { return "1"; }
        }

        public static string GesturesFileVersion
        {
            get { return "3"; }
        }
        
    }
}
