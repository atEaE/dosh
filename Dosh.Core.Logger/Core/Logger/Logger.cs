using Dosh.Core.Helper;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;

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
        /// log template.
        /// </summary>
        private const string LOG_MESSAGE_TEMPLATE = "{Level:u4},{UtcTimestamp:yyyy/MM/dd HH:mm:ss.fff},{MachineName},ThreadId:{ThreadId},{Message:j}{NewLine}{Exception}";

        /// <summary>
        /// log level table.
        /// </summary>
        private Dictionary<string, LogEventLevel> levelTable = new Dictionary<string, LogEventLevel>
        {
            { LogEventLevel.Verbose.ToString(), LogEventLevel.Verbose },
            { LogEventLevel.Debug.ToString(), LogEventLevel.Debug },
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
                .MinimumLevel.Is(getMinimumLevel(ConfigurationManager.AppSettings.Get("appLogLevel")))
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.With(new UtcTimestampEnricher())
                .WriteTo.File(
                    path: ConfigurationManager.AppSettings["appLogFile"],
                    outputTemplate: LOG_MESSAGE_TEMPLATE,
                    rollingInterval: RollingInterval.Day, 
                    shared: true,
                    fileSizeLimitBytes: null)
                .CreateLogger();
        }

        /// <summary>
        /// Outputs the log.
        /// </summary>
        /// <param name="level">log level</param>
        /// <param name="message">message</param>
        public void OutputLog(LogEventLevel level, string message)
        {
            var frame = new System.Diagnostics.StackTrace().GetFrame(0);
            var className = frame.GetMethod().ReflectedType.FullName;
            var method = frame.GetMethod().Name;

            outputLog(level, message, className, method);
        }

        /// <summary>
        /// Outputs the log.
        /// </summary>
        /// <param name="level">log level</param>
        /// <param name="message">message</param>
        /// <param name="className">Caller class name</param>
        /// <param name="method">Caller method name</param>
        private void outputLog(LogEventLevel level, string message, string className, string method)
        {
            var assembly = Assembly.GetEntryAssembly();
            var moduleName = "UnknownModule";
            if (assembly != null)
            {
                moduleName = Path.GetFileNameWithoutExtension(assembly.Location);
            }

            var logFmt = $"{moduleName},{message},{className},{method}";
            fileLogger.Write(level, logFmt);
        }

        /// <summary>
        /// Get the lowest level of the log.
        /// </summary>
        /// <param name="level">log level</param>
        /// <returns>minimum log level</returns>
        private LogEventLevel getMinimumLevel(string level)
        {
            var min = LogEventLevel.Information;
            if (levelTable.ContainsKey(level ?? string.Empty))
            {
                min = levelTable[level];
            }

            return min;
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
    }
}
