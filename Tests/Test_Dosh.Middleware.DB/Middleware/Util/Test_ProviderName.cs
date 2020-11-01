using Dosh.Middleware.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Dosh.Middleware.DB.Middleware.Util
{
    /// <summary>
    /// ProviderNameClass unit test.
    /// </summary>
    [TestClass]
    public class Test_ProviderName
    {
        [TestMethod]
        public void CheckGetProviderName()
        {
            var expect = "System.Data.SqlClient";

            var providerName = DBProvider.GetProviderName("sqlserver");

            Assert.AreEqual(expect, providerName);
        }
    }
}
