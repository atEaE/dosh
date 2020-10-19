using System;
using System.IO;
using Dosh.Core.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Dosh.Core.Runtime.Core.Parser
{
    [TestClass]
    public class Test_DoshParser
    {
        [TestMethod]
        public void TestParseDoshFile()
        {
            // setup
            var input = string.Empty;
            using (var sr = new StreamReader("./Tests/test_normal.yml"))
            {
                input = sr.ReadToEnd();
            }

            var parser = new DoshParser();
            var result = parser.Parse(input);

            // version check
            Assert.AreEqual("1.0", result.Version);
        }
    }
}
