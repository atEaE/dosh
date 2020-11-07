using Dosh.Core.DoshFile;
using Dosh.Core.Provider.Initializer;
using Dosh.Core.SemanticsAnalyzer;
using Dosh.Core.TestExec;
using System.Collections.Generic;
using System.Linq;

namespace Dosh.Core.SementicsAnalyzer
{
    public class DoshFileSemanticsAnalyzer : ISemanticsAnalyzer
    {
        /// <summary>
        /// Initializer plugin path.
        /// </summary>
        private string initializerPluginPath;

        /// <summary>
        /// Injector plugin path.
        /// </summary>
        private string injectorPluginPath;

        /// <summary>
        /// Crawler plugin path.
        /// </summary>
        private string crawlerPluginPath;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="initializerPluginPath">Initializer plugin path.</param>
        /// <param name="injectorPluginPath">Injector plugin path.</param>
        /// <param name="crawlerPluginPath">Crawler plugin path.</param>
        public DoshFileSemanticsAnalyzer(string initializerPluginPath, string injectorPluginPath, string crawlerPluginPath)
        {
            this.initializerPluginPath = initializerPluginPath;
            this.injectorPluginPath = injectorPluginPath;
            this.crawlerPluginPath = crawlerPluginPath;
        }

        public List<ITestExec> Analyze(DoshFileModel doshFile)
        {
            var tests = new List<ITestExec>();
            foreach(var test in doshFile.TestSets)
            {
                var testCase = new TestExec.TestExec();
                testCase.Initializers = analyzeSetup(test.Value.SetupConfig);
                test.Value.RunConfig.Steps.AsParallel().ForAll(r => analyzeRunStep(r));
                test.Value.CleanupConfig.AsParallel().ForAll(c => analyzeCleanup(c));
                tests.Add(testCase);
            }

            return tests;
        }

        private IEnumerable<IInitializer> analyzeSetup(IEnumerable<SetupConfig> setups)
        {
            var initlisers = Plugin.PluginLoader.LoadInitializerPlugins(initializerPluginPath);
            foreach(var setup in setups)
            {
                if (initlisers.ContainsKey(setup.Type))
                {
                    yield return initlisers[setup.Type];
                }
            }
        }

        private void analyzeRunStep(Step step)
        {

        }

        private void analyzeCleanup(CleanupConfig cleanup)
        {

        }
    }
}
