using Dosh.CLI.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Dosh.CLI.Helper
{
    [TestClass]
    public class Test_AppConfigUtil
    {
        [TestMethod]
        public void TestGetInitializerPluginPath()
        {
            // setup
            var expected = @"C:\Users\Test\AppData\Roaming\Dosh\Plugin\Initializer";
            AppConfigUtil.InitializerPluginPath.Is(expected);
        }

        [TestMethod]
        public void TestGetInjectorPluginPath()
        {
            // setup
            var expected = @"C:\Users\Test\AppData\Roaming\Dosh\Plugin\Injector";
            AppConfigUtil.InjectorPluginPath.Is(expected);
        }

        [TestMethod]
        public void TestGetCrawlerPluginPath()
        {
            // setup
            var expected = @"C:\Users\Test\AppData\Roaming\Dosh\Plugin\Crawler";
            AppConfigUtil.CrawlerPluginPath.Is(expected);
        }
    }
}
