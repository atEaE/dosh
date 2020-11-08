using Dosh.Core.Plugin;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Test_Dosh.Core.Runtime.Plugin
{
    [TestClass]
    public class Test_PluginLoader
    {
        [TestMethod]
        public void TestPluginLoaderNotFoundDirectory()
        {
            AssertEx.Catch<DirectoryNotFoundException>(() => { var plugins = PluginLoader.LoadInitializerPlugins("Tests/XXXXX/YYYYYY"); });    
        }

        [TestMethod]
        public void TestLoadInitializerPlugins()
        {
            // setup
            var plugins = PluginLoader.LoadInitializerPlugins("Tests/Plugin/Initializer");
            var testCases = new string[] { "TestInitializerPlugin1", "TestInitializerPlugin2" };

            foreach(var tc in testCases)
            {
                var plugin = plugins[tc];
                plugin.Initialize();
            }
        }

        [TestMethod]
        public void TestLoadEvaluatorPlugins()
        {
            // setup 
            var plugins = PluginLoader.LoadEvaluatorPlugins("Tests/Plugin/Evaluator");
            var testCases = new string[] { "TestEvaluatorPlugin1", "TestEvaluatorPlugin2" };

            foreach(var tc in testCases)
            {
                var plugin = plugins[tc];
                plugin.Evaluate();
            }
        }

        [TestMethod]
        public void TestLoadCrawlerPlugins()
        {
            // setup 
            var plugins = PluginLoader.LoadCrawlerPlugins("Tests/Plugin/Crawler");
            var testCases = new string[] { "TestCrawlerPlugin1", "TestCrawlerPlugin2" };

            foreach (var tc in testCases)
            {
                var plugin = plugins[tc];
                plugin.Gather();
            }
        }

        [TestMethod]
        public void TestLoadInjectorPlugins()
        {
            // setup 
            var plugins = PluginLoader.LoadInjectorPlugins("Tests/Plugin/Injector");
            var testCases = new string[] { "TestInjectorPlugin1", "TestInjectorPlugin2" };

            foreach (var tc in testCases)
            {
                var plugin = plugins[tc];
                plugin.Inject();
            }
        }
    }
}
