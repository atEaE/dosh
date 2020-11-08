using Dosh.Core.DoshFile;
using Dosh.Core.TestExec;
using System.Collections.Generic;

namespace Dosh.Core.SemanticsAnalyzer
{
    /// <summary>
    /// Semantics analyzer interface
    /// </summary>
    public interface ISemanticsAnalyzer
    {
        List<ITestExec> Analyze(DoshFileModel doshFile);
    }
}
