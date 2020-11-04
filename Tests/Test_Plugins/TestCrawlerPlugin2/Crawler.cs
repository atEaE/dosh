using Dosh.Core.Provider.Crawler;
using System;

namespace TestCrawlerPlugin2
{
    /// <summary>
    /// TestCrawlerPlugin2
    /// </summary>
    public class Crawler : ICrawler
    {
        /// <summary>
        /// Gather the data you need.
        /// </summary>
        public void Gather()
        {
            Console.WriteLine("TestCrawlerPlugin2 Called.");
        }

        /// <summary>
        /// Disposing
        /// </summary>
        public void Dispose()
        { }
    }
}
