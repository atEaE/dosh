using Dosh.Core.Logger;
using Dosh.Middleware.DB.Middleware.Client;
using Dosh.Middleware.DB.Middleware.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Dosh.Middleware.DB.Middleware.Context
{
    /// <summary>
    /// DBClient unit test.
    /// </summary>
    [TestClass]
    public class Test_DBClient
    {
        [TestMethod]
        public void CheckCreateDbConnection()
        {
        }

        private class LoggerMock : ILogger
        {
            public void Dispose()
            { }

            public void OutputLog(Serilog.Events.LogEventLevel level, string message)
            { }
        }
    }
}
