using Dosh.Core.DoshFile;
using System;
using System.IO;
using System.Linq;
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

            var doshFile = deserializer.Deserialize<DoshFileModel>(value);
            doshFile.TestSets.AsParallel().ForAll(t => t.Value.RefsResolution(doshFile.Definition));

            return doshFile;
        }
    }
}
