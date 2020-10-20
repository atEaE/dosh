using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// TestSet model
    /// </summary>
    [DataContract]
    public class TestSet
    {
        [YamlMember(Alias = "id")]
        public string ID { get; set; }

        [YamlMember(Alias = "setup")]
        public List<SetupConfig> SetupConfig { get; set; }

        [YamlMember(Alias = "run")]
        public RunConfig RunConfig { get; set; }

        [YamlMember(Alias = "cleanup")]
        public List<CleanupConfig> CleanupConfig { get; set; }
    }

    /// <summary>
    /// SetupConfig model
    /// </summary>
    [DataContract]
    public class SetupConfig
    {
        [YamlMember(Alias = "type")]
        public string Type { get; set; }
        
        [YamlMember(Alias = "resource")]
        public string Resource { get; set; }
    }

    /// <summary>
    /// RunConfig model
    /// </summary>
    public class RunConfig
    {
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        [YamlMember(Alias = "resource")]
        public string Resouce { get; set; }

        [YamlMember(Alias = "command")]
        public string Command { get; set; }

        [YamlMember(Alias = "crawler")]
        public string Crawler { get; set; }
    }

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
