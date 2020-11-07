using System.Configuration;

namespace Dosh.CLI.Helper
{
    /// <summary>
    /// AppConfig helper
    /// </summary>
    public static class AppConfigUtil
    {
        /// <summary>
        /// Initializer plugin path.
        /// </summary>
        public static string InitializerPluginPath { get { return getAppSetting("initializerPluginPath"); } }

        /// <summary>
        /// Injector plugin path.
        /// </summary>
        public static string InjectorPluginPath { get { return getAppSetting("injectorPluginPath"); } }

        /// <summary>
        /// Crawler plugin path.
        /// </summary>
        public static string CrawlerPluginPath { get { return getAppSetting("crawlerPluginPath"); } }

        /// <summary>
        /// Get the value set to the key from the App.config file.
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        private static string getAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
