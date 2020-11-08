using Dosh.Middleware.DB;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test_Dosh.Middleware.DB.Middleware.Util
{
    /// <summary>
    /// ConnectionStringClass unit test.
    /// </summary>
    [TestClass]
    public class Test_ConnectionString
    {
        [TestMethod]
        public void CheckCreateConnectionString()
        {
            var expect = "Data Source=localhost;Initial Catalog=TestDB;User ID=TestID;Password=Password";

            var testConnectionString = ConnectionString.CreateConnectionString("SQLServer", "localhost", "TestDB", "TestID", "Password");

            Assert.AreEqual(expect, testConnectionString);
        }
    }
}
