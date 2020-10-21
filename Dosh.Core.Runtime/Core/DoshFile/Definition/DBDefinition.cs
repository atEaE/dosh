using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// DB Definition
    /// </summary>
    [DataContract]
    public class DBDefinition
    {
        [YamlMember(Alias = "id")]
        public string ID { get; set; }

        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        [YamlMember(Alias = "host")]
        public string Host { get; set; }

        [YamlMember(Alias = "database")]
        public string Database { get; set; }

        [YamlMember(Alias = "user")]
        public string UserId { get; set; }

        [YamlMember(Alias = "pass")]
        public string Password { get; set; }

        [YamlMember(Alias = "schema")]
        public string Schema { get; set; }
    }
}
