using System;
using Dosh.CLI.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Dosh.CLI.Helper
{
    [TestClass]
    public class Test_FileHelper
    {
        [TestMethod]
        public void TestGetCompletedPath()
        {
            // setup
            var testCases = new[]
            {
                new { Input = ".", Expected = ".dosh.yml" },
                new { Input = "TestFolder", Expected = @"TestFolder\.dosh.yml" },
                new { Input = "TestFolder/Sample", Expected = @"TestFolder/Sample\.dosh.yml" },
                new { Input = ".dosh.yml", Expected = ".dosh.yml" },
                new { Input = "TestFolder/test.txt", Expected = "TestFolder/test.txt" },
                new { Input = "TestFolder/.dosh.yml", Expected = "TestFolder/.dosh.yml" },
            };

            foreach(var tc in testCases)
            {
                var actual = FileHelper.GetCompletedPath(tc.Input);
                actual.Is(tc.Expected);
            }
        }
    }
}
