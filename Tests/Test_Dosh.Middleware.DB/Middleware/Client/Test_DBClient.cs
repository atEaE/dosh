using Dosh.Core.Logger;
using Dosh.Middleware.DB.Middleware.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;

namespace Test_Dosh.Middleware.DB.Middleware.Context
{
    /// <summary>
    /// DBClient unit test.
    /// </summary>
    [TestClass]
    public class Test_DBClient
    {
        [TestMethod]
        public void CheckCreateDbConnection_Success()
        {
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = ":memory:" };

            var sqliteConnection = new SQLiteConnection(sqlConnectionSb.ConnectionString);

            var logger = new LoggerMock();
            var client = new DBClient(logger);

            var type = client.GetType();
            var propName = type.GetField("Connection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            propName.SetValue(client, sqliteConnection);

            using (var connection = client.CreateDbConnection("", sqlConnectionSb.ConnectionString))
            { }
        }

        [TestMethod]
        public void CheckCreateDbConnection_connectionStringIsNull()
        {
            var logger = new LoggerMock();
            var client = new DBClient(logger);

            var connection = client.CreateDbConnection("", null);

            Assert.IsNull(connection);
        }

        [TestMethod]
        public void CheckCreateDbConnection_connectionFailed()
        {
            var logger = new LoggerMock();
            var client = new DBClient(logger);

            var connection = client.CreateDbConnection("", "connectionString");

            Assert.IsNull(connection);
        }

        [TestMethod]
        public void CheckDbCommandSelect_Success()
        {
            var expect = new List<List<string>>();
            expect.Add(new List<string>() { "KeyString", "Id"});
            expect.Add(new List<string>() { "TestDataString", "12"});

            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = ":memory:" };

            var sqliteConnection = new SQLiteConnection(sqlConnectionSb.ConnectionString);

            var logger = new LoggerMock();
            var client = new DBClient(logger);

            var type = client.GetType();
            var propName = type.GetField("Connection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            propName.SetValue(client, sqliteConnection);

            using (var connection = client.CreateDbConnection("", sqlConnectionSb.ConnectionString))
            {
                client.ExecuteDbCommand("CREATE TABLE TESTDB(KeyString TEXT NOT NULL PRIMARY KEY, Id INTEGER)");
                client.ExecuteDbCommand("INSERT INTO TESTDB(KeyString, Id) VALUES('TestDataString', 12)");

                var data = client.DbCommandSelect("select * from TESTDB");

                Assert.AreEqual(expect[0][0], data[0][0]);
                Assert.AreEqual(expect[0][1], data[0][1]);
                Assert.AreEqual(expect[1][0], data[1][0]);
                Assert.AreEqual(expect[1][1], data[1][1]);
            }
        }

        [TestMethod]
        public void CheckExecuteDbCommand_Success()
        {
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = ":memory:" };

            var sqliteConnection = new SQLiteConnection(sqlConnectionSb.ConnectionString);

            var logger = new LoggerMock();
            var client = new DBClient(logger);

            var type = client.GetType();
            var propName = type.GetField("Connection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            propName.SetValue(client, sqliteConnection);

            using (var connection = client.CreateDbConnection("", sqlConnectionSb.ConnectionString))
            {
                client.ExecuteDbCommand("CREATE TABLE TESTDB(KeyString TEXT NOT NULL PRIMARY KEY, Id INTEGER)");
            }
        }

        [TestMethod]
        public void CheckgetProviderFactory_Success()
        {
            var sqlConnectionSb = new SQLiteConnectionStringBuilder { DataSource = ":memory:" };

            var sqliteConnection = new SQLiteConnection(sqlConnectionSb.ConnectionString);

            var logger = new LoggerMock();
            var client = new DBClient(logger);

            var type = client.GetType();
            var propName = type.GetField("Connection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            propName.SetValue(client, sqliteConnection);

            var service = new PrivateObject(client);

            var providerFactory = service.Invoke("getProviderFactory", "");

            Assert.IsInstanceOfType(providerFactory, typeof(DbProviderFactory));
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
