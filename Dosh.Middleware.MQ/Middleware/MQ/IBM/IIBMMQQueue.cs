using System;

namespace Dosh.Middleware.MQ.IBM
{
    /// <summary>
    /// IBMMQ Queue interface
    /// </summary>
    public interface IIBMMQQueue : IDisposable
    {
        /// <summary>
        /// Queue name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Message get.
        /// </summary>
        /// <returns>message.</returns>
        string Get();

        /// <summary>
        /// Message put.
        /// </summary>
        /// <param name="message">message.</param>
        /// <param name="applicationIdData">application id</param>
        /// <param name="expiry">expiry time</param>
        /// <param name="priority">message priority</param>
        void Put(string message, string applicationIdData = null, int? expiry = null, int? priority = null);
    }
}
