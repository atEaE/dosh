using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// RunConfig model
    /// </summary>
    public class RunConfig
    {
        /// <summary> 
        /// Test steps
        /// </summary>
        public List<Step> Steps { get; set; }
    }

    /// <summary>
    /// Step model
    /// </summary>
    public class Step : IRefsAllowsModel
    {
        [YamlMember(Alias = "type")]
        public string Type { get; set; }

        [YamlMember(Alias = "resource")]
        public string Resouce { get; set; }

        [YamlMember(Alias = "command")]
        public string Command { get; set; }

        [YamlMember(Alias = "crawler")]
        public CrawlerConfig Crawler { get; set; }

        /// <summary>
        /// Reference resolution.
        /// </summary>
        /// <param name="definition">definition model</param>
        public void RefsResolution(Definition definition)
        {
            Crawler.RefsResolution(definition);
        }
    }
}
