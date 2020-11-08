using Dosh.Core.Provider.Crawler;
using Dosh.Core.Provider.Initializer;
using Dosh.Core.Provider.Injector;
using System.Collections.Generic;

namespace Dosh.Core.TestExec
{
    /// <summary>
    /// TestExec interface
    /// </summary>
    public interface ITestExec
    {
        /// <summary>
        /// Test Initilizer
        /// </summary>
        IEnumerable<IInitializer> Initializers { get; set; }

        /// <summary>
        /// Data Injector
        /// </summary>
        List<IInjector> Injectors { get; set; }

        /// <summary>
        /// Data Craler
        /// </summary>
        List<ICrawler> Crawlers { get; set; }

        /// <summary>
        /// Test end monitoring flag
        /// </summary>
        bool IsFinished { get; set; }

        /// <summary>
        /// Test end monitoring flag
        /// </summary>
        void Execute();
    }
}
