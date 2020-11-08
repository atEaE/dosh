using System;
using System.Runtime.Serialization;

namespace Dosh.CLI.Exception
{
    /// <summary>
    /// Scafoold exception
    /// </summary>
    [Serializable()]
    public class DoshScaffoldException : System.Exception
    {
        /// <summary>
        /// Create instance.
        /// </summary>
        /// <param name="message">exception message</param>
        public DoshScaffoldException(string message)
            : base(message)
        { }

        /// <summary>
        /// Create instance.
        /// </summary>
        /// <param name="message">exception message</param>
        /// <param name="innerException">inner exception</param>
        public DoshScaffoldException(string message, System.Exception innerException)
            : base(message, innerException)
        { }

        /// <summary>
        /// Create instance.
        /// </summary>
        /// <param name="serialInfo">serial infomation</param>
        /// <param name="context">stream context</param>
        public DoshScaffoldException(SerializationInfo serialInfo, StreamingContext context)
            : base(serialInfo, context)
        { }
    }
}
