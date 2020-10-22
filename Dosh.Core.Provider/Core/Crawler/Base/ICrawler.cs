using System;

namespace Dosh.Core.Provider.Crawler
{
    /// <summary>
    /// Crawler Interface
    /// </summary>
    public interface ICrawler : IDisposable
    {
        void Gather();
    }
}
