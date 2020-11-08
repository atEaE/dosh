using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dosh.Core.Parser
{
    /// <summary>
    /// Parser interface
    /// </summary>
    public interface IParser<T>
    {
        T Parse(string value);
    }
}
