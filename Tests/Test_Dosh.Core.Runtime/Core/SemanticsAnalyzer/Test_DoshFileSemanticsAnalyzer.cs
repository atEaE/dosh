using Dosh.Core.Initializer;
using Dosh.Core.Injector;
using Dosh.Core.Parser;
using Dosh.Core.SementicsAnalyzer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Threading;

namespace Test_Dosh.Core.Runtime.Core.SemanticsAnalyzer
{
    [TestClass]
    public class Test_DoshFileSemanticsAnalyzer
    {
        [TestMethod]
        public void TestSemanticsAnalyze()
        {
            // setup
            var input = string.Empty;
            using (var sr = new StreamReader("./Tests/test_normal.yml"))
            {
                input = sr.ReadToEnd();
            }
            var parser = new DoshParser();
            var result = parser.Parse(input);

            // semantics
            var analyzer = new DoshFileSemanticsAnalyzer();
            var exes = analyzer.Analyze(result);
            Assert.IsNotNull(exes);


            // cleanup check
            var cleanupTestCase = new[]
            {
                new { Type = typeof(DBInitializer) }
            };
        }
    }
}
