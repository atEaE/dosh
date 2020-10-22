using System.Collections.Generic;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// TestSet model
    /// </summary>
    [DataContract]
    public class TestSet : IRefsAllowsModel
    {
        [YamlMember(Alias = "setup")]
        public List<SetupConfig> SetupConfig { get; set; }

        [YamlMember(Alias = "run")]
        public RunConfig RunConfig { get; set; }

        [YamlMember(Alias = "cleanup")]
        public List<CleanupConfig> CleanupConfig { get; set; }

        /// <summary>
        /// Reference resolution.
        /// </summary>
        /// <param name="definition">definition model</param>
        public void RefsResolution(Definition definition)
        {
            SetupConfig.ForEach(t => t.RefsResolution(definition));
            RunConfig.Steps.ForEach(t => t.RefsResolution(definition));
            CleanupConfig.ForEach(t => t.RefsResolution(definition));
        }
    }
}
