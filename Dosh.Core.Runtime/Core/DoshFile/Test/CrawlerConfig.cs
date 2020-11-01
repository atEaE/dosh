using System.Collections.Generic;
using System.Configuration;
using YamlDotNet.Serialization;


namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// Crawler config
    /// </summary>
    public class CrawlerConfig : IRefsAllowsModel
    {
        [YamlMember(Alias = "trigger")]
        public string Trigger { get; set; }

        [YamlMember(Alias = "bots")]
        public Dictionary<string, BotConfig> Bots { get; set; }

        [YamlMember(Alias = "refs")]
        public string Refs { get; set; }

        /// <summary>
        /// Reference resolution.
        /// </summary>
        /// <param name="definition">definition model</param>
        public void RefsResolution(Definition definition)
        {
            if (string.IsNullOrEmpty(Refs))
            {
                return;
            }

            if (!Refs.StartsWith("$"))
            {
                throw new ConfigurationErrorsException(string.Format("A reference identifier('$') does not exist in the '{0}' reference definition.", "crawler"));
            }

            var address = Refs.Substring(1);
            var defIdAndKey = address.Split('/');

            var crawDef = definition.CrawlerDefinitions[defIdAndKey[1]];
            if (crawDef == null)
            {
                throw new ConfigurationErrorsException(string.Format("There is no {0} definition with the specified ID({1}).", "crawler", defIdAndKey[1]));
            }

            Trigger = crawDef.Trigger;
            Bots = crawDef.Bots;
            Refs = null;
        }
    }

    /// <summary>
    /// Bot config
    /// </summary>
    public class BotConfig
    {
        [YamlMember(Alias = "type")]
        public string Type { get; set; }


        [YamlMember(Alias = "target")]
        public List<string> Target { get; set; }
    }
}
