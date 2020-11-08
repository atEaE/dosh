using System.Collections.Generic;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// Definition
    /// </summary>
    [DataContract]
    public class Definition
    {
        [YamlMember(Alias = "db")]
        public Dictionary<string, DBDefinition> DBDefinitions { get; set; }

        [YamlMember(Alias = "mq")]
        public Dictionary<string, MQDefinition> MQDefinitions { get; set; }

        [YamlMember(Alias = "crawler")]
        public Dictionary<string, CrawlerConfig> CrawlerDefinitions { get; set; }
    }
}
