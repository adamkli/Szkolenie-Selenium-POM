using System.Configuration;

namespace POM.Tests.Tools
{
    public static class GlobalSettings
    {
        public static string BaseURL => GetValue("BaseURL");
        public static string BrowserType => GetValue("BrowserType");
        private static int GetIntValue(string key, int defaultValue)
        {
            int ret;
            return int.TryParse(GetValue(key), out ret) ? ret : defaultValue;
        }

        private static string GetValue(string key)
        {
            //try to read value from enviroment variable (set by TeamCity), if not set, use app.config
            return System.Environment.GetEnvironmentVariable("TC_" + key) ?? ConfigurationManager.AppSettings[key] ?? "";
        }
    }
}
