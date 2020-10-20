using CommandLine;
using Dosh.CLI.Commands;
using System.Configuration;
using System.IO;
using static Dosh.CLI.Helper.FileHelper;

namespace Dosh.CLI
{
    /// <summary>
    /// Entry class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point
        /// </summary>
        /// <param name="args">command line arguments</param>
        public static void Main(string[] args)
        {
            if (!Directory.Exists(DOSH_APP_DIRECTORY))
            {
                createApplicationFolder();
            }

            initializeAppConfig();

            var result = Parser.Default.ParseArguments<Init, Run, Config>(args);
            result.WithParsed<Init>(cmd => cmd.Execute())
                  .WithParsed<Run>(cmd => cmd.Execute())
                  .WithParsed<Config>(cmd => cmd.Execute());
        }

        /// <summary>
        /// Create application folder.
        /// </summary>
        private static void createApplicationFolder()
        {
            Directory.CreateDirectory(DOSH_APP_DIRECTORY);
            Directory.CreateDirectory(DOSH_LOG_DIRECTORY);
            Directory.CreateDirectory(DOSH_REPORT_DIRECTORY);
            Directory.CreateDirectory(DOSH_TEMPORARY_DIRECTORY);
        }

        /// <summary>
        /// Initialize app.exe.config
        /// </summary>
        private static void initializeAppConfig()
        {
            var conf = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conf.AppSettings.Settings["appLogFile"].Value = Path.Combine(DOSH_LOG_DIRECTORY, "dosh-.log");
            conf.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
