using Dosh.Core.Provider.Initializer;
using System;

namespace TestInitializerPlugin2
{
    /// <summary>
    /// TestInitializerPlugin1
    /// </summary>
    public class Initializer : IInitializer
    {
        /// <summary>
        /// Perform the specified initialization process.
        /// </summary>
        public void Initialize()
        {
            Console.WriteLine("TestInitializerPlugin2 Called.");
        }
    }
}
