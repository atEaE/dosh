using Dosh.Core.Provider.Crawler;
using System;

namespace TestCrawlerPlugin1
{
    /// <summary>
    /// TestCrawlerPlugin1
    /// </summary>
    public class Crawler : ICrawler
    {
        /// <summary>
        /// Gather the data you need.
        /// </summary>
        public void Gather()
        {
            Console.WriteLine("TestCrawlerPlugin1 Called.");
        }

        /// <summary>
        /// Disposing
        /// </summary>
        public void Dispose()
        { }
    }
}
