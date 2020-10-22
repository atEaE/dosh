using Dosh.Core.Crawler;
using Dosh.Core.Initializer;
using Dosh.Core.Injector;
using System.Collections.Generic;

namespace Dosh.Core.TestExec
{
    /// <summary>
    /// TestExec interface
    /// </summary>
    public interface ITestExec
    {
        List<IInitializer> Initializers { get; set; }
        List<IInjector> Injectors { get; set; }
        List<ICrawler> Crawlers { get; set; }

        bool IsFinished { get; set; }

        void Execute();
    }
}
