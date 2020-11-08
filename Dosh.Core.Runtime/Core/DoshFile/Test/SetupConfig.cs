using System.Runtime.Serialization;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// SetupConfig model
    /// </summary>
    [DataContract]
    public class SetupConfig : IRefsAllowsModel
    {
        /// <summary>
        /// initializer type (Set the dll name.)
        /// </summary>
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        [YamlMember(Alias = "resource")]
        public string Resource { get; set; }

        /// <summary>
        /// Reference resolution.
        /// </summary>
        /// <param name="definition">definition model</param>
        public void RefsResolution(Definition definition)
        { }
    }
}
