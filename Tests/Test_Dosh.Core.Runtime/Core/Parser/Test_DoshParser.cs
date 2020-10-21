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

            // id check
            Assert.AreEqual("SampleService", result.ID);

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
                        Steps = new List<Step>()
                        {
                            new Step()
                            {
                                Type = "mq",
                                Resouce = "./test/initdata/ini1.xml",
                                Command = "cat text1.txt",
                                Crawler = new CrawlerConfig()
                                {
                                    Trigger = "mq-trigger",
                                    Bots = new Dictionary<string, BotConfig>()
                                    {
                                        ["db-craw"] = new BotConfig() { Type = "db", Target = new List<string>{ "sample_table1", "sample_table2" } },
                                        ["log-craw"] = new BotConfig() { Type = "file", Target = new List<string>{ "./test1.log" } },
                                        ["mq-craw"] = new BotConfig() { Type = "mq", Target = new List<string>{ "LCL.MQQ.123" }}
                                    }
                                }
                            },
                            new Step()
                            {
                                Type = "mq",
                                Resouce = "./test/initdata/ini2.xml",
                                Command = "cat text2.txt",
                                Crawler = new CrawlerConfig()
                                {
                                    Trigger = "mq-trigger",
                                    Bots = new Dictionary<string, BotConfig>()
                                    {
                                        ["db-craw"] = new BotConfig() { Type = "db", Target = new List<string>{ "sample_table3", "sample_table4" } },
                                        ["log-craw"] = new BotConfig() { Type = "file", Target = new List<string>{ "./test2.log" } },
                                        ["mq-craw"] = new BotConfig() { Type = "mq", Target = new List<string>{ "LCL.MQQ.456" }}
                                    }
                                }
                            },
                        }
                    },
                    CleanupConfig = new[]
                    {
                        new CleanupConfig() { Type = "db", Target = new List<string>(){ "user_table", "department_table", "employ_table" } }
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
                for (var i = 0; i < tc.RunConfig.Steps.Count; i++)
                {
                    Assert.AreEqual(tc.RunConfig.Steps[i].Type, testSet.RunConfig.Steps[i].Type);
                    Assert.AreEqual(tc.RunConfig.Steps[i].Resouce, testSet.RunConfig.Steps[i].Resouce);
                    Assert.AreEqual(tc.RunConfig.Steps[i].Command, testSet.RunConfig.Steps[i].Command);

                    // crawler check
                    Assert.AreEqual(tc.RunConfig.Steps[i].Crawler.Trigger, testSet.RunConfig.Steps[i].Crawler.Trigger);
                    foreach (var tcBot in tc.RunConfig.Steps[i].Crawler.Bots)
                    {
                        var bots = testSet.RunConfig.Steps[i].Crawler.Bots[tcBot.Key];
                        Assert.IsNotNull(bots);
                        Assert.AreEqual(tcBot.Value.Type, bots.Type);
                        CollectionAssert.AreEqual(tcBot.Value.Target, bots.Target);
                    }
                }

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
