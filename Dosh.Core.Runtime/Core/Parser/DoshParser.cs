using Dosh.Core.DoshFile;
using System;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Dosh.Core.Parser
{
    /// <summary>
    /// DoshFile parser
    /// </summary>
    public class DoshParser : IParser<DoshFileModel>
    {
        public DoshFileModel Parse(string value)
        {
            var deserializer = new DeserializerBuilder()
                                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                                    .Build();
            return deserializer.Deserialize<DoshFileModel>(value);
        }
    }
}
