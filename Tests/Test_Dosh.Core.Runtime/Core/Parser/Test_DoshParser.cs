using Dosh.Core.DoshFile;
using Dosh.Core.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

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

            // define check(DB)
            var dbTestCase = new[]
            {
                new { Key = "test-db-a", Type = "sqlserver", Host = "localhost:8888", Database = "test-db-1", UserId = "test-user-1", Password = "test-pass-1", Schema = "default-1" },
                new { Key = "test-db-b", Type = "mysql", Host = "localhost:9999", Database = "test-db-2", UserId = "test-user-2", Password = "test-pass-2", Schema = "default-2" },
            };
            foreach(var tc in dbTestCase)
            {
                var dbDef = result.Definition.DBDefinitions[tc.Key];
                Assert.IsNotNull(dbDef);
                Assert.AreEqual(tc.Type, dbDef.Type);
                Assert.AreEqual(tc.Host, dbDef.Host);
                Assert.AreEqual(tc.Database, dbDef.Database);
                Assert.AreEqual(tc.UserId, dbDef.UserId);
                Assert.AreEqual(tc.Password, dbDef.Password);
                Assert.AreEqual(tc.Schema, dbDef.Schema);
            }

            // define check(MQ)
            var mqTestCase = new[]
            {
                new { Key = "MQW888",  Host = "localhost:6666", Port = 1234, Channel = "mq-channel-1", MQManagerName = "mq-manager-1" },
                new { Key = "MQW999",  Host = "localhost:7777", Port = 5678, Channel = "mq-channel-2", MQManagerName = "mq-manager-2" },
            };
            foreach(var tc in mqTestCase)
            {
                var mqDef = result.Definition.MQDefinitions[tc.Key];
                Assert.IsNotNull(mqDef);
                Assert.AreEqual(tc.Host, mqDef.Host);
                Assert.AreEqual(tc.Port, mqDef.Port);
                Assert.AreEqual(tc.Channel, mqDef.Channel);
                Assert.AreEqual(tc.MQManagerName, mqDef.MQManagerName);
            }

            // tests check
            var testSetTestCase = new[]
            {
                new
                {
                    Key = "case1",
                    SetupConfig = new[]
                    {
                        new SetupConfig(){ Type = "db", Resource = "./test/data/db/init.sql" },
                        new SetupConfig(){ Type = "mq", Resource = "./test/data/mq/init.xml" }
                    },
                    RunConfig = new RunConfig()
                    {
                        Type = "mq",
                        Resouce = "./test/initdata/ini.xml",
                        Command = "cat text.txt",
                        Crawler = "db"
                    },
                    CleanupConfig = new[]
                    {
                        new CleanupConfig() { Type = "db", Target = new List<string>(){ "user_table", "department_table", "employ_table" }}
                    }
                }
            };
            foreach(var tc in testSetTestCase)
            {
                var testSet = result.TestSets[tc.Key];
                Assert.IsNotNull(testSet);
                
                // setup check
                for (var i = 0; i < tc.SetupConfig.Length; i++)
                {
                    Assert.AreEqual(tc.SetupConfig[i].Type, testSet.SetupConfig[i].Type);
                    Assert.AreEqual(tc.SetupConfig[i].Resource, testSet.SetupConfig[i].Resource);
                }

                // run check
                Assert.AreEqual(tc.RunConfig.Type, testSet.RunConfig.Type);
                Assert.AreEqual(tc.RunConfig.Resouce, testSet.RunConfig.Resouce);
                Assert.AreEqual(tc.RunConfig.Command, testSet.RunConfig.Command);
                Assert.AreEqual(tc.RunConfig.Crawler, testSet.RunConfig.Crawler);

                // cleanup check
                for (var i = 0; i < tc.CleanupConfig.Length; i++)
                {
                    Assert.AreEqual(tc.CleanupConfig[i].Type, testSet.CleanupConfig[i].Type);
                    for (var j = 0; j < tc.CleanupConfig[i].Target.Count; j++)
                    {
                        Assert.AreEqual(tc.CleanupConfig[i].Target[j], testSet.CleanupConfig[i].Target[j]);
                    }
                }
            }
        }
    }
}
