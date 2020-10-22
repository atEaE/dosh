using System;

namespace Dosh.Core.Crawler
{
    /// <summary>
    /// Crawler Interface
    /// </summary>
    public interface ICrawler : IDisposable
    {
        void Gather();
    }
}
