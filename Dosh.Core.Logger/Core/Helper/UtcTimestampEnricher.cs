using Serilog.Core;
using Serilog.Events;

namespace Dosh.Core.Helper
{
    /// <summary>
    /// UtcTemplate log enricher.
    /// </summary>
    public class UtcTimestampEnricher : ILogEventEnricher
    {
        /// <summary>
        /// UtcTemplate Enrich
        /// </summary>
        /// <param name="logEvent">log event</param>
        /// <param name="pf">log eventProperty factory</param>
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory pf)
        {
            logEvent.AddPropertyIfAbsent(pf.CreateProperty("UtcTimestamp", logEvent.Timestamp.UtcDateTime));
        }
    }
}
