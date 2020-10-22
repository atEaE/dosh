using System.Collections.Generic;
using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// CleanupConfig model
    /// </summary>
    [DataContract]
    public class CleanupConfig : IRefsAllowsModel
    {
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        [YamlMember(Alias = "target")]
        public List<string> Target { get; set; }

        /// <summary>
        /// Reference resolution.
        /// </summary>
        /// <param name="definition">definition model</param>
        public void RefsResolution(Definition definition)
        { }
    }
}
