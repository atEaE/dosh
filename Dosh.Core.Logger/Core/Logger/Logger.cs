using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace Dosh.Core.Logger
{
    /// <summary>
    /// Logger class.
    /// </summary>
    public class Logger : ILogger
    {
        /// <summary>
        /// file logger
        /// </summary>
        private readonly Serilog.Core.Logger fileLogger;

        /// <summary>
        /// log level table.
        /// </summary>
        private Dictionary<string, LogEventLevel> levelTable = new Dictionary<string, LogEventLevel>
        {
            { LogEventLevel.Verbose.ToString(), LogEventLevel.Verbose },
            {  LogEventLevel.Debug.ToString(), LogEventLevel.Debug },
            { LogEventLevel.Information.ToString(), LogEventLevel.Information },
            { LogEventLevel.Warning.ToString(), LogEventLevel.Warning },
            { LogEventLevel.Error.ToString(), LogEventLevel.Error }
        };

        /// <summary> 
        /// Resource disposed flag 
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Create instance.
        /// </summary>
        public Logger()
        {
            fileLogger = new LoggerConfiguration()
                .MinimumLevel.Is(getMinimumLevel())
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        /// <summary>
        /// Outputs the log.
        /// </summary>
        /// <param name="level">log level</param>
        /// <param name="message">message</param>
        public void OutputLog(LogEventLevel level, string message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Destroy the resource.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Destroy the resource.
        /// </summary>
        /// <param name="disposing">disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                { }

                fileLogger.Dispose();
                disposed = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private LogEventLevel getMinimumLevel()
        {
            var min = LogEventLevel.Information;
            var level = ConfigurationManager.AppSettings.Get("appLogLevel");
            if (levelTable.ContainsKey(level))
            {
                min = levelTable[level];
            }

            return min;
        }
    }
}
