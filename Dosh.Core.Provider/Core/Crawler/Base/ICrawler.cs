using System;

namespace Dosh.Core.Provider.Crawler
{
    /// <summary>
    /// Crawler Interface
    /// </summary>
    public interface ICrawler : IDisposable
    {
        /// <summary>
        /// Gather the data you need.
        /// </summary>
        void Gather();
    }
}
