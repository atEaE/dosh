using Dosh.Core.Provider.Crawler;
using Dosh.Core.Provider.Initializer;
using Dosh.Core.Provider.Injector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dosh.Core.TestExec
{
    public class TestExec : ITestExec
    {
        public List<IInitializer> Initializers { get; set; }
        public List<IInjector> Injectors { get; set; }
        public Func<object> Trigger { get; set; }
        public List<ICrawler> Crawlers { get; set; }

        public List<object> Finalizer { get; set; }

        public bool IsFinished { get; set; } = false;

        public void Execute()
        {
            if (Initializers != null && Initializers.Any())
            {
                Initializers.ForEach(i => i.Initialize());
            }            

            Task.Run(async () =>
            {
                var tmp = await Task.Run(() => Trigger());
                if (Crawlers != null && Crawlers.Any())
                {
                    Crawlers.AsParallel().ForAll(c => 
                    { 
                        try
                        {
                            c.Gather();
                        }
                        finally
                        {
                            c.Dispose();
                        }
                        
                    });
                }

                Finalizer.AsParallel().ForAll(c => c.ToString());
                IsFinished = true;
            });

            if (Injectors != null && Injectors.Any())
            {
                Injectors.ForEach(i => i.Inject());
            }
        }


    }
}
