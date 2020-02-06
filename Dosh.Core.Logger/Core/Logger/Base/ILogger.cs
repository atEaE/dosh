using Serilog.Events;
using System;

namespace Dosh.Core.Logger
{
    /// <summary>
    /// Logger interface.
    /// </summary>
    public interface ILogger : IDisposable
    {
        /// <summary>
        /// Outputs the log.
        /// </summary>
        /// <param name="level">log level</param>
        /// <param name="message">message</param>
        void OutputLog(LogEventLevel level, string message);
    }
}
