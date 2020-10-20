using CommandLine;
using Dosh.Core.DoshFile;
using Dosh.Core.Parser;
using System;
using System.IO;
using static Dosh.CLI.Const.CLIConst;

namespace Dosh.CLI.Commands
{
    /// <summary>
    ///  Test start command
    /// </summary>
    [Verb("run")]
    public class Run : CommandBase
    {
        [Option(shortName: 't', longName: "test", Required = true)]
        public string TestFilePath { get; set; }

        /// <summary>
        /// Execute inner command.
        /// </summary>
        protected override void OnExecute()
        {
            if (!File.Exists(TestFilePath))
            {
                Console.WriteLine(".dosh.yml file does not exist.");
                return;
            }

            var input = string.Empty;       
            using (var sr = new StreamReader(TestFilePath))
            {
                input = sr.ReadToEnd();
            }

            DoshFileModel result;
            try
            {
                result = new DoshParser().Parse(input);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to parse the .dosh.yml file. {0}", ex);
                return;
            }

            if (!ensureTestExecute(result))
            {
                return;
            }

            var testIdPath = Path.Combine(DOSH_WORKSPACE_TESTFOLDER, result.ID);
            foreach (var test in result.TestSets)
            {
                var casePath = Path.Combine(testIdPath, test.Key);
                if (!Directory.Exists(casePath))
                {
                    Directory.CreateDirectory(casePath);
                }
            }
        }

        /// <summary>
        /// Verify that the test is viable.
        /// </summary>
        private bool ensureTestExecute(DoshFileModel doshConfig)
        {
            var result = false;
            var testIdPath = Path.Combine(DOSH_WORKSPACE_TESTFOLDER, doshConfig.ID);
            if (!Directory.Exists(testIdPath))
            {
                try
                {
                    var testIdDir = Directory.CreateDirectory(testIdPath);
                    result = true;
                }
                catch (IOException ioEx)
                {
                    Console.WriteLine("'__test__' directory creation failed. {0}", ioEx);
                }
                catch (UnauthorizedAccessException anAuthEx)
                {
                    Console.WriteLine("Permission denied. {0}", anAuthEx);
                }
            }
            else
            {
                result = true;
            }

            return result;
        }
    }
}
