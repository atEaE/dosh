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
    }
}
