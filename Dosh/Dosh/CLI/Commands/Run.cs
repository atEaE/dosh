using CommandLine;
using Dosh.CLI.Exception;
using Dosh.Core.DoshFile;
using Dosh.Core.Parser;
using Dosh.Core.SementicsAnalyzer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using static Dosh.CLI.Helper.FileHelper;
using static Dosh.Properties.Resources;

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
            var completedPath = GetCompletedPath(TestFilePath);
            if (!File.Exists(completedPath))
            {
                Console.WriteLine(CLI_00104);
                return;
            }

            DoshFileModel doshConfig;
            try
            {
                using (var sr = new StreamReader(completedPath))
                {
                    var input = sr.ReadToEnd();
                    doshConfig = new DoshParser().Parse(input);
                }
            }
            catch(System.Exception ex)
            {
                Console.WriteLine(CLI_00105, ex);
                return;
            }

            if (!ensureTestExecute(doshConfig))
            {
                return;
            }

            scaffoldTestCases(doshConfig);
            var semanticsAnalyzer = new DoshFileSemanticsAnalyzer(initPluginPath: "");
            var exes = semanticsAnalyzer.Analyze(doshConfig);
            exes.ForEach(e =>
            {
                e.Execute();
                while (!e.IsFinished)
                {
                    Thread.Sleep(200);
                }
            });
        }

        /// <summary>
        /// Verify that the test is viable.
        /// </summary>
        /// <param name="doshConfig"><see cref="DoshFileModel"/></param>
        private bool ensureTestExecute(DoshFileModel doshConfig)
        {
            var result = false;
            var testIdPath = GetTestIdDirectory(doshConfig.ID);
            if (!Directory.Exists(testIdPath))
            {
                try
                {
                    var testIdDir = Directory.CreateDirectory(testIdPath);
                    result = true;
                }
                catch (IOException ioEx)
                {
                    var msg = string.Format(CLI_00102, "__test__", ioEx);
                    throw new DoshScaffoldException(msg, ioEx);
                }
                catch (UnauthorizedAccessException anAuthEx)
                {
                    var msg = string.Format(CLI_00101, anAuthEx);
                    throw new DoshScaffoldException(msg, anAuthEx);
                }
            }
            else
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="doshConfig"></param>
        private void scaffoldTestCases(DoshFileModel doshConfig)
        {
            var scaffolds = doshConfig.TestSets.Select<KeyValuePair<string, TestSet>, Action>(t => () =>
            {
                var testIdPath = GetTestIdDirectory(doshConfig.ID);
                if (!Directory.Exists(testIdPath))
                {
                    throw new DoshScaffoldException(CLI_00103);
                }

                var testCasePath = Path.Combine(testIdPath, t.Key);
                if (!Directory.Exists(testCasePath))
                {
                    try
                    {
                        Directory.CreateDirectory(testCasePath);
                    }
                    catch (IOException ioEx)
                    {
                        var msg = string.Format(CLI_00102, $"__test__/{t.Key}", ioEx);
                        throw new DoshScaffoldException(msg, ioEx);
                    }
                    catch (UnauthorizedAccessException anAuthEx)
                    {
                        var msg = string.Format(CLI_00101, anAuthEx);
                        throw new DoshScaffoldException(msg, anAuthEx);
                    }
                    catch (System.Exception ex)
                    {
                        var msg = string.Format(CLI_00100, ex);
                        throw new DoshScaffoldException(msg, ex);
                    }
                }

                scaffoldWkInitDirectory(testCasePath, t.Value);
                scaffoldWkDataDirectory(testCasePath, t.Value);
                scaffoldWkEvidenceDirectory(testCasePath, t.Value);

            }).AsParallel();
            scaffolds.ForAll(s => s.Invoke());
        }

        private void scaffoldWkInitDirectory(string testCasePath, TestSet testSet)
        {
            var initPath = Path.Combine(testCasePath, "__init__");
            if (!Directory.Exists(initPath))
            {
                try
                {
                    Directory.CreateDirectory(initPath);
                }
                catch (IOException ioEx)
                {
                    var msg = string.Format(CLI_00102, testCasePath, ioEx);
                    throw new DoshScaffoldException(msg, ioEx);
                }
                catch (UnauthorizedAccessException anAuthEx)
                {
                    var msg = string.Format(CLI_00101, anAuthEx);
                    throw new DoshScaffoldException(msg, anAuthEx);
                }
                catch (System.Exception ex)
                {
                    var msg = string.Format(CLI_00100, ex);
                    throw new DoshScaffoldException(msg, ex);
                }
            }
            testSet.SetupConfig.ForEach(s =>
            {
                var iniTypePath = Path.Combine(initPath, s.Type);
                if (!Directory.Exists(iniTypePath))
                {
                    try
                    {
                        Directory.CreateDirectory(iniTypePath);
                    }
                    catch (IOException ioEx)
                    {
                        var msg = string.Format(CLI_00102, iniTypePath, ioEx);
                        throw new DoshScaffoldException(msg, ioEx);
                    }
                    catch (UnauthorizedAccessException anAuthEx)
                    {
                        var msg = string.Format(CLI_00101, anAuthEx);
                        throw new DoshScaffoldException(msg, anAuthEx);
                    }
                    catch (System.Exception ex)
                    {
                        var msg = string.Format(CLI_00100, ex);
                        throw new DoshScaffoldException(msg, ex);
                    }
                }
            });
        }

        private void scaffoldWkDataDirectory(string testCasePath, TestSet testSet)
        {
            var dataPath = Path.Combine(testCasePath, "data");
            if (!Directory.Exists(dataPath))
            {
                try
                {
                    Directory.CreateDirectory(dataPath);
                }
                catch (IOException ioEx)
                {
                    var msg = string.Format(CLI_00102, dataPath, ioEx);
                    throw new DoshScaffoldException(msg, ioEx);
                }
                catch (UnauthorizedAccessException anAuthEx)
                {
                    var msg = string.Format(CLI_00101, anAuthEx);
                    throw new DoshScaffoldException(msg, anAuthEx);
                }
                catch (System.Exception ex)
                {
                    var msg = string.Format(CLI_00100, ex);
                    throw new DoshScaffoldException(msg, ex);
                }
            }
            var stepCnt = 1;
            testSet.RunConfig.Steps.ForEach(r =>
            {
                var stepPath = Path.Combine(dataPath, $"step{stepCnt}");
                if (!Directory.Exists(stepPath))
                {
                    try
                    {
                        Directory.CreateDirectory(stepPath);
                    }
                    catch (IOException ioEx)
                    {
                        var msg = string.Format(CLI_00102, stepPath, ioEx);
                        throw new DoshScaffoldException(msg, ioEx);
                    }
                    catch (UnauthorizedAccessException anAuthEx)
                    {
                        var msg = string.Format(CLI_00101, anAuthEx);
                        throw new DoshScaffoldException(msg, anAuthEx);
                    }
                    catch (System.Exception ex)
                    {
                        var msg = string.Format(CLI_00100, ex);
                        throw new DoshScaffoldException(msg, ex);
                    }
                }
                stepCnt++;
            });
        }

        private void scaffoldWkEvidenceDirectory(string testCasePath, TestSet testSet)
        {
            var evidencePath = Path.Combine(testCasePath, "evidence");
            if (!Directory.Exists(evidencePath))
            {
                try
                {
                    Directory.CreateDirectory(evidencePath);
                }
                catch (IOException ioEx)
                {
                    var msg = string.Format(CLI_00102, evidencePath, ioEx);
                    throw new DoshScaffoldException(msg, ioEx);
                }
                catch (UnauthorizedAccessException anAuthEx)
                {
                    var msg = string.Format(CLI_00101, anAuthEx);
                    throw new DoshScaffoldException(msg, anAuthEx);
                }
                catch (System.Exception ex)
                {
                    var msg = string.Format(CLI_00100, ex);
                    throw new DoshScaffoldException(msg, ex);
                }
            }
            var stepCnt = 1;
            testSet.RunConfig.Steps.ForEach(r =>
            {
                var stepPath = Path.Combine(evidencePath, $"step{stepCnt}");
                if (!Directory.Exists(stepPath))
                {
                    try
                    {
                        Directory.CreateDirectory(stepPath);
                    }
                    catch (IOException ioEx)
                    {
                        var msg = string.Format(CLI_00102, stepPath, ioEx);
                        throw new DoshScaffoldException(msg, ioEx);
                    }
                    catch (UnauthorizedAccessException anAuthEx)
                    {
                        var msg = string.Format(CLI_00101, anAuthEx);
                        throw new DoshScaffoldException(msg, anAuthEx);
                    }
                    catch (System.Exception ex)
                    {
                        var msg = string.Format(CLI_00100, ex);
                        throw new DoshScaffoldException(msg, ex);
                    }
                }
                stepCnt++;
            });
        }
    }
}
