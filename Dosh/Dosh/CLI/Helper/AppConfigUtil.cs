using System.Configuration;

namespace Dosh.CLI.Helper
{
    /// <summary>
    /// AppConfig helper
    /// </summary>
    public static class AppConfigUtil
    {
        /// <summary>
        /// app.config keys
        /// </summary>
        public const string INITIALIZER_PLUGINPATH_KEY = "initializerPluginPath";
        /// <summary>
        /// app.config keys
        /// </summary>
        public const string INJECTOR_PLUGINPATH_KEY = "injectorPluginPath";
        /// <summary>
        /// app.config keys
        /// </summary>
        public const string CRAWLER_PLUGINPATH_KEY = "crawlerPluginPath";

        /// <summary>
        /// Initializer plugin path.
        /// </summary>
        public static string InitializerPluginPath { get { return getAppSetting(INITIALIZER_PLUGINPATH_KEY); } }

        /// <summary>
        /// Injector plugin path.
        /// </summary>
        public static string InjectorPluginPath { get { return getAppSetting(INJECTOR_PLUGINPATH_KEY); } }

        /// <summary>
        /// Crawler plugin path.
        /// </summary>
        public static string CrawlerPluginPath { get { return getAppSetting(CRAWLER_PLUGINPATH_KEY); } }

        /// <summary>
        /// Set initializer plugin path.
        /// </summary>
        public static void SetInitializerPluginPath(Configuration config, string value)
        {
            setAppSetting(config, INITIALIZER_PLUGINPATH_KEY, value);
        }

        /// <summary>
        /// Set injector plugin path.
        /// </summary>
        public static void SetInjectorPluginPath(Configuration config, string value)
        {
            setAppSetting(config, INJECTOR_PLUGINPATH_KEY, value);
        }

        /// <summary>
        /// Set crawler plugin path.
        /// </summary>
        public static void SetCrawlerPluginPath(Configuration config, string value)
        {
            setAppSetting(config, CRAWLER_PLUGINPATH_KEY, value);
        }

        /// <summary>
        /// Get the value set to the key from the App.config file.
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        private static string getAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// Set the value for a given key.
        /// </summary>
        /// <param name="config">Configuration instance.</param>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        private static void setAppSetting(Configuration config, string key, string value)
        {
            config.AppSettings.Settings[key].Value = value;
        }
    }
}
