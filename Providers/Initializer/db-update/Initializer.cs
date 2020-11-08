using Dosh.Core.Provider.Initializer;
using System;

namespace DBUpdateInitializer
{
    /// <summary>
    /// DB insert initializer
    /// </summary>
    public class Initializer : IInitializer
    {
        public void Initialize()
        {
            Console.WriteLine("DB Update");
        }
    }
}
