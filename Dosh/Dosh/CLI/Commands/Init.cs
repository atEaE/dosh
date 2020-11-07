using CommandLine;
using System;
using System.IO;
using static Dosh.CLI.Helper.FileHelper;

namespace Dosh.CLI.Commands
{
    /// <summary>
    /// Initialize command
    /// </summary>
    [Verb(name: "init", HelpText = "Dosh initialization. Generate a test folder in the current directory.")]
    public class Init : CommandBase
    {
        /// <summary>
        /// Execute inner command.
        /// </summary>
        protected override void OnExecute()
        {
            var testDirPath = DOSH_WORKSPACE_TEST_DIRECTORY;
            if (Directory.Exists(testDirPath))
            {
                Console.WriteLine($"The '__test__' directory already exists.");
                return; 
            }

            try
            {
                var testDir = Directory.CreateDirectory(testDirPath);
                Console.WriteLine("Initialize dosh.");
            }
            catch(IOException ioEx)
            {
                Console.WriteLine("'__test__' directory creation failed. {0}", ioEx);
            }
            catch(UnauthorizedAccessException anAuthEx)
            {
                Console.WriteLine("Permission denied. {0}", anAuthEx);
            }
            catch(System.Exception ex)
            {
                Console.WriteLine("An unexpected error has occurred. {0}", ex);
            }
        }
    }
}
