using Dosh.Core.Provider.Initializer;
using System;

namespace DBInsertInitializer
{
    /// <summary>
    /// DB insert initializer
    /// </summary>
    public class Initializer : IInitializer
    {
        public void Initialize()
        {
            Console.WriteLine("DB Init");
        }
    }
}
