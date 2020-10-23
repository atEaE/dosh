using Dosh.Core.DoshFile;
using Dosh.Core.Logger;
using Dosh.Core.Provider.Crawler;
using System;
using System.IO;

namespace file_crawl
{
    public class FileCrawler : ICrawler
    {
        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger { get; set; }

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
            exportLogMessageToFile(directory);
        }

        /// <summary>
        /// Export log messages to text file
        /// </summary>
        private void exportLogMessageToFile(string exportDirectoryRootPath)
        {
            BotConfig.Target.ForEach(logFile =>
            {
                var fileName = Path.GetFileName(logFile);

                using (var writer = new StreamWriter(Path.Combine(exportDirectoryRootPath, fileName)))
                using (var reader = new StreamReader(logFile))
                {
                    var logMessage = reader.ReadToEnd();

                    writer.WriteLine(logMessage);

                    writer.Flush();
                }
            });
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

            var directory = Path.Combine(exportDirectoryRootPath, "LOG");

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
        ~FileCrawler()
        {
            Dispose(false);
        }
    }
}
