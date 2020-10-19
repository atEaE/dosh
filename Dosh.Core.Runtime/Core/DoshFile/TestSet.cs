using System.Collections.Generic;
using System.Data.Common;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// TestSet model
    /// </summary>
    public class TestSet
    {
        [YamlMember(Alias = "id")]
        public string ID { get; set; }

        [YamlMember(Alias = "setup")]
        public List<SetupConfig> SetupConfig { get; set; }

        [YamlMember(Alias = "run")]
        public string Run { get; set; }

        [YamlMember(Alias = "cleanup")]
        public string CleanupConf { get; set; }
    }

    /// <summary>
    /// SetupConfig model
    /// </summary>
    public class SetupConfig
    {
        [YamlMember(Alias = "type")]
        public string Type { get; set; }
        
        [YamlMember(Alias = "resource")]
        public string Resource { get; set; }
    }
}
