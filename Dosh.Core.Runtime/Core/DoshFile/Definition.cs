using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
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
    }
}
