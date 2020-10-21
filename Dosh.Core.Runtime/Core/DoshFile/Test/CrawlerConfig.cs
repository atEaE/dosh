using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace Dosh.Core.DoshFile
{
    /// <summary>
    /// Crawler config
    /// </summary>
    public class CrawlerConfig
    {
        [YamlMember(Alias = "trigger")]
        public string Trigger { get; set; }

        [YamlMember(Alias = "bots")]
        public Dictionary<string, BotConfig> Bots { get; set; }
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
