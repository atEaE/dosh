using CommandLine;
using Dosh.CLI.Commands;
using System.IO;
using static Dosh.CLI.Const.CLIConst;

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
            if (!Directory.Exists(DOSH_APPFOLDER))
            {
                CreateApplicationFolder();
            }
                
            var result = Parser.Default.ParseArguments<Init, Run>(args);
            result.WithParsed<Init>(cmd => cmd.Execute())
                  .WithParsed<Run>(cmd => cmd.Execute());
        }

        /// <summary>
        /// Create application folder.
        /// </summary>
        public static void CreateApplicationFolder()
        {
            Directory.CreateDirectory(DOSH_APPFOLDER);

            Directory.CreateDirectory(DOSH_LOGFOLDER);
            Directory.CreateDirectory(DOSH_REPORT);
            Directory.CreateDirectory(DOSH_TEMPORARY);
        }
    }
}
