using Dosh.Core.DoshFile;
using Dosh.Core.Logger;
using Dosh.Core.Provider.Crawler;
using Dosh.Middleware.MQ.IBM;
using System;
using System.IO;

namespace mq_crawl
{
    public class MQCrawler : ICrawler
    {
        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// IBMMQQueue
        /// </summary>
        public IIBMMQQueue MQQueue { get; set; }

        /// <summary>
        /// DoshFileModel
        /// </summary>
        public DoshFileModel DoshFileModel { get; set; }

        /// <summary>
        /// ExportDirectoryRootPath
        /// </summary>
        public string ExportDirectoryRootPath { get; set; }

        /// <summary>
        /// BotConfig
        /// </summary>
        private readonly BotConfig BotConfig;

        /// <summary> 
        /// Resource disposed flag 
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Gathering the result
        /// </summary>
        public void Gather()
        {
            var directory = createExportDirectory(ExportDirectoryRootPath);
            exportQueueMessageToFile(directory);
        }

        /// <summary>
        /// Export queue messages to text file
        /// </summary>
        private void exportQueueMessageToFile(string exportDirectoryPath)
        {
            using (var writer = new StreamWriter(Path.Combine(exportDirectoryPath, $"{MQQueue.Name}.txt")))
            {
                BotConfig.Target.ForEach(queue =>
                {
                    var queueMessage = MQQueue.Get();

                    writer.WriteLine(queueMessage);

                    writer.Flush();
                });
            }
        }

        /// <summary>
        /// Create directory
        /// </summary>
        private string createExportDirectory(string exportDirectoryRootPath)
        {
            if (!Directory.Exists(exportDirectoryRootPath))
            {
                throw new Exception();
            }

            var directory = Path.Combine(exportDirectoryRootPath, "MQ");

            try
            {
                Directory.CreateDirectory(directory);
            }
            catch (IOException ioEx)
            {
            }
            catch (UnauthorizedAccessException anAuthEx)
            {
            }

            return directory;
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
                Logger.Dispose();
                disposed = true;
            }
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~MQCrawler()
        {
            Dispose(false);
        }
    }
}
