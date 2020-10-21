using System.Collections.Generic;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// CleanupConfig model
    /// </summary>
    [DataContract]
    public class CleanupConfig
    {
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        [YamlMember(Alias = "target")]
        public List<string> Target { get; set; }
    }
}
