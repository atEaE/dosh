using System.Collections.Generic;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// DoshFile model
    /// </summary>
    [DataContract]
    public class DoshFileModel
    {
        [YamlMember(Alias = "version")]
        public string Version { get; set; }

        [YamlMember(Alias = "definition")]
        public Definition Definition { get; set; }

        [YamlMember(Alias = "tests")]
        public Dictionary<string, TestSet> TestSets { get; set; }
    }
}
