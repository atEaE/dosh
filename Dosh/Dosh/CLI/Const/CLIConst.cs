using System;
using System.IO;

namespace Dosh.CLI.Const
{
    /// <summary>
    /// Dosh CLI Constant.
    /// </summary>
    public static class CLIConst
    {
        /// <summary>
        /// Dosh CLI application name.
        /// </summary>
        public const string APP_NAME = "Dosh";

        /// <summary>
        /// Dosh application folder.(C:\Users\UserLocal\AppData\Roaming\Dosh)
        /// </summary>
        public readonly static string DOSH_APPFOLDER = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), APP_NAME);

        /// <summary>
        /// Dosh application log folder.
        /// </summary>
        public readonly static string DOSH_LOGFOLDER = Path.Combine(DOSH_APPFOLDER, "Log");

        /// <summary>
        /// Dosh application report folder.
        /// </summary>
        public readonly static string DOSH_REPORT = Path.Combine(DOSH_APPFOLDER, "Report");

        /// <summary>
        /// Dosh application temporary folder.
        /// </summary>
        public readonly static string DOSH_TEMPORARY = Path.Combine(DOSH_APPFOLDER, "Temp");

        /// <summary>
        /// Test directory for workspace
        /// </summary>
        public readonly static string DOSH_WORKSPACE_TEST = "__test__";
    }
}
