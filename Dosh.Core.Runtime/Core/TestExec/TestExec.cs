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
        /// <summary>
        /// Test Initilizer
        /// </summary>
        public IEnumerable<IInitializer> Initializers { get; set; }

        /// <summary>
        /// Data Injector
        /// </summary>
        public List<IInjector> Injectors { get; set; }

        public Func<object> Trigger { get; set; }

        /// <summary>
        /// Data Craler
        /// </summary>
        public List<ICrawler> Crawlers { get; set; }

        public List<object> Finalizer { get; set; }

        /// <summary>
        /// Test end monitoring flag
        /// </summary>
        public bool IsFinished { get; set; } = false;

        /// <summary>
        /// Test end monitoring flag
        /// </summary>
        public void Execute()
        {
            if (Initializers != null && Initializers.Any())
            {
                foreach(var init in Initializers)
                {
                    init.Initialize();
                }
            }            

            //Task.Run(async () =>
            //{
            //    var tmp = await Task.Run(() => Trigger());
            //    if (Crawlers != null && Crawlers.Any())
            //    {
            //        Crawlers.AsParallel().ForAll(c => 
            //        { 
            //            try
            //            {
            //                c.Gather();
            //            }
            //            finally
            //            {
            //                c.Dispose();
            //            }
                        
            //        });
            //    }

            //    Finalizer.AsParallel().ForAll(c => c.ToString());
            //    IsFinished = true;
            //});

            if (Injectors != null && Injectors.Any())
            {
                Injectors.ForEach(i => i.Inject());
            }
        }


    }
}
