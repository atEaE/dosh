using Dosh.Core.Logger;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Serilog.Events;

namespace Test_Dosh.Core.Logger.Core.Logger
{
    /// <summary>
    /// Logger unit test.
    /// </summary>
    [TestClass]
    public class Test_Logger
    {
        [TestMethod]
        public void CheckgetMinimumLevel_Normal()
        {
            // setup
            var pObj = new PrivateObject(new Dosh.Core.Logger.Logger());

            // verbose
            var verbose = pObj.Invoke("getMinimumLevel", "Verbose");
            Assert.AreEqual(LogEventLevel.Verbose, verbose);

            // debug
            var debug = pObj.Invoke("getMinimumLevel", "Debug");
            Assert.AreEqual(LogEventLevel.Debug, debug);

            // information
            var info = pObj.Invoke("getMinimumLevel", "Information");
            Assert.AreEqual(LogEventLevel.Information, info);

            // warning
            var warn = pObj.Invoke("getMinimumLevel", "Warning");
            Assert.AreEqual(LogEventLevel.Warning, warn);

            // error
            var eror = pObj.Invoke("getMinimumLevel", "Error");
            Assert.AreEqual(LogEventLevel.Error, eror);
        }

        [TestMethod]
        public void CheckgetMinimumLevel_NonValue()
        {
            // setup
            var pObj = new PrivateObject(new Dosh.Core.Logger.Logger());

            // verbose
            var result = pObj.Invoke("getMinimumLevel", "HogeHoge");
            Assert.AreEqual(LogEventLevel.Information, result);
        }

        [TestMethod]
        public void CheckOutputLog()
        {
            // setup
            var logger = new Dosh.Core.Logger.Logger();
            logger.OutputLog(LogEventLevel.Information, "test");
        }
    }
}
