using System;
using System.IO;
using static Dosh.CLI.Const.CLIConst;

namespace Dosh.CLI.Helper
{
    /// <summary>
    /// File helper
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        /// Default testfile name.
        /// </summary>
        public const string DEFAULT_TESTFILENAME = ".dosh.yml";

        /// <summary>
        /// Dosh application folder.(C:\Users\UserLocal\AppData\Roaming\Dosh)
        /// </summary>
        public readonly static string DOSH_APP_DIRECTORY = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APP_NAME);

        /// <summary>
        /// Dosh application log folder.
        /// </summary>
        public readonly static string DOSH_LOG_DIRECTORY = Path.Combine(DOSH_APP_DIRECTORY, "Log");

        /// <summary>
        /// Dosh application report folder.
        /// </summary>
        public readonly static string DOSH_REPORT_DIRECTORY = Path.Combine(DOSH_APP_DIRECTORY, "Report");

        /// <summary>
        /// Dosh application temporary folder.
        /// </summary>
        public readonly static string DOSH_TEMPORARY_DIRECTORY = Path.Combine(DOSH_APP_DIRECTORY, "Temp");

        /// <summary>
        /// Dosh application plugin folder.
        /// </summary>
        public readonly static string DOSH_PLUGIN_DIRECTORY = Path.Combine(DOSH_APP_DIRECTORY, "Plugin");

        /// <summary>
        /// Dosh application Initializer plugin folder.
        /// </summary>
        public readonly static string DOSH_INITIALIZER_PLUGIN_DIRECTORY = Path.Combine(DOSH_PLUGIN_DIRECTORY, "Initializer");

        /// <summary>
        /// Dosh application Injector plugin folder.
        /// </summary>
        public readonly static string DOSH_INJECTOR_PLUGIN_DIRECTORY = Path.Combine(DOSH_PLUGIN_DIRECTORY, "Injector");

        /// <summary>
        /// Dosh application Crawler plugin folder.
        /// </summary>
        public readonly static string DOSH_CRAWLER_PLUGIN_DIRECTORY = Path.Combine(DOSH_PLUGIN_DIRECTORY, "Crawler");

        /// <summary>
        /// Test directory for workspace
        /// </summary>
        public readonly static string DOSH_WORKSPACE_TEST_DIRECTORY = Path.Combine(Directory.GetCurrentDirectory(), "__test__");

        /// <summary>
        /// Get a test case folder.
        /// </summary>
        /// <param name="id">test case id</param>
        /// <returns>folder path</returns>
        public static string GetTestIdDirectory(string id)
        {
            return Path.Combine(DOSH_WORKSPACE_TEST_DIRECTORY, id);
        }

        /// <summary>
        /// Completes the path passed to the argument and returns it.
        /// </summary>
        /// <param name="path">path</param>
        /// <returns>Completed path</returns>
        public static string GetCompletedPath(string path)
        {
            if ("." == path)
            {
                return DEFAULT_TESTFILENAME;
            }

            var fileExt = Path.GetExtension(path);
            if (string.IsNullOrEmpty(fileExt))
            {
                return Path.Combine(path, DEFAULT_TESTFILENAME);
            }
            else
            {
                return path;
            }
        }
    }
}
