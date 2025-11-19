
namespace MGestures.App
{
    internal static class Constants
        {
        // TODO Jack
        public const string Identifier = "com.Jack.MGestures";
        public const string CheckForUpdateUrlAppSettingKey = "CheckForUpdateUrl";

        public const string ProductHomePageAppSettingKey = "ProductHomePage";

#if DEBUG
        public const int AutoCheckForUpdateInterval = 1000 * 3;
#else 
        public const int AutoCheckForUpdateInterval = 1000*30;
#endif
    }
}
