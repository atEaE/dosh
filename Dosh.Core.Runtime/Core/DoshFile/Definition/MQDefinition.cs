using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// MQ Definition
    /// </summary>
    [DataContract]
    public class MQDefinition
    {
        [YamlMember(Alias = "id")]
        public string ID { get; set; }

        [YamlMember(Alias = "host")]
        public string Host { get; set; }

        [YamlMember(Alias = "port")]
        public int Port { get; set; }

        [YamlMember(Alias = "channel")]
        public string Channel { get; set; }

        [YamlMember(Alias = "mqMgr")]
        public string MQManagerName { get; set; }
    }
}
