using IBM.WMQ;
using System;

namespace Dosh.Middleware.MQ.IBM
{
    public class IBMMQQueue : IIBMMQQueue
    {
        /// <summary>
        /// WMQ MQQueue
        /// </summary>
        private readonly MQQueue mQQueue;

        /// <summary> 
        /// Resource disposed flag 
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Queue name
        /// </summary>
        public string Name => mQQueue.Name.Trim();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mQQueue"></param>
        public IBMMQQueue(MQQueue mQQueue)
        {
            this.mQQueue = mQQueue;
        }

        /// <summary>
        /// Get a message from Queue.
        /// </summary>
        /// <returns>message.</returns>
        public string Get()
        {
            var result = string.Empty;
            try
            {
                var mqMsg = new MQMessage();
                mQQueue.Get(mqMsg);

                if (mqMsg.TotalMessageLength != 0)
                {
                    result = mqMsg.ReadString(mqMsg.TotalMessageLength);
                }
            }
            catch(MQException ex)
            {
                // If the WaitInterval option is specified and you can't get it within the specified time, an exceptin is raised.
                // As for processing, it is processed as follows: no message = string.empty. If any other exceptions are raised, it is thrown to the caller.
                if (ex.Reason != MQC.MQRC_NO_MSG_AVAILABLE) throw ex;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="applicationIdData"></param>
        /// <param name="expiry"></param>
        /// <param name="priority"></param>
        public void Put(string message, string applicationIdData = null, int? expiry = null, int? priority = null)
        {
            var mqMsg = new MQMessage();
            mqMsg.WriteString(message);

            mQQueue.Put(mqMsg);
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
        protected void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                { }

                mQQueue.Close();
                disposed = true;
            }
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~IBMMQQueue()
        {
            Dispose(false);
        }
    }
}
