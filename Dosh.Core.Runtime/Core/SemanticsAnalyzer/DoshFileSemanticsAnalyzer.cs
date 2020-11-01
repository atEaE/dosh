using Dosh.Core.DoshFile;
using Dosh.Core.SemanticsAnalyzer;
using Dosh.Core.TestExec;
using System.Collections.Generic;
using System.Linq;

namespace Dosh.Core.SementicsAnalyzer
{
    public class DoshFileSemanticsAnalyzer : ISemanticsAnalyzer
    {
        public List<ITestExec> Analyze(DoshFileModel doshFile)
        {
            foreach(var test in doshFile.TestSets)
            {
                test.Value.SetupConfig.AsParallel().ForAll(s => analyzeSetup(s));
                test.Value.RunConfig.Steps.AsParallel().ForAll(r => analyzeRunStep(r));
                test.Value.CleanupConfig.AsParallel().ForAll(c => analyzeCleanup(c));
            }

            return new List<ITestExec>();
        }

        private void analyzeSetup(SetupConfig setup)
        {
            
        }

        private void analyzeRunStep(Step step)
        {

        }

        private void analyzeCleanup(CleanupConfig cleanup)
        {

        }
    }
}
