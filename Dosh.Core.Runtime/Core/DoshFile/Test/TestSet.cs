using System.Collections.Generic;
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
        [YamlMember(Alias = "setup")]
        public List<SetupConfig> SetupConfig { get; set; }

        [YamlMember(Alias = "run")]
        public RunConfig RunConfig { get; set; }

        [YamlMember(Alias = "cleanup")]
        public List<CleanupConfig> CleanupConfig { get; set; }
    }
}
