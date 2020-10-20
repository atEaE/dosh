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
    }
}
