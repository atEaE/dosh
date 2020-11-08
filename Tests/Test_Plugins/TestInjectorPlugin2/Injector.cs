using Dosh.Core.Provider.Injector;
using System;

namespace TestInjectorPlugin2
{
    /// <summary>
    /// TestInjectorPlugin2
    /// </summary>
    public class Injector : IInjector
    {
        /// <summary>
        /// Inject the specified process or data.
        /// </summary>
        public void Inject()
        {
            Console.WriteLine("TestInjectorPlugin2 Called.");
        }
    }
}
