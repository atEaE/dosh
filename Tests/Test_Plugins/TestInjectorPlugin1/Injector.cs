using Dosh.Core.Provider.Injector;
using System;

namespace TestInjectorPlugin1
{
    /// <summary>
    /// TestInjectorPlugin1
    /// </summary>
    public class Injector : IInjector
    {
        /// <summary>
        /// Inject the specified process or data.
        /// </summary>
        public void Inject()
        {
            Console.WriteLine("TestInjectorPlugin1 Called.");
        }
    }
}
