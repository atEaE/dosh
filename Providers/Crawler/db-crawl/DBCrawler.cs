using CsvHelper;
using Dosh.Core.DoshFile;
using Dosh.Core.Logger;
using Dosh.Core.Provider.Crawler;
using Dosh.Middleware.DB.Middleware.Base;
using System;
using System.Globalization;
using System.IO;

namespace db_crawl
{
    public class DBCrawler : ICrawler
    {
        /// <summary>
        /// Logger
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// DBClient
        /// </summary>
        public IDBClient DBClient { get; set; }

        /// <summary>
        /// DoshFileModel
        /// </summary>
        public DoshFileModel DoshFileModel { get; set; }

        /// <summary>
        /// ExportDirectoryRootPath
        /// </summary>
        public string ExportDirectoryRootPath { get; set; }

        /// <summary>
        /// DBDefinition
        /// </summary>
        private readonly DBDefinition DBDefinition;

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
            exportRecordToCsv(directory);
        }

        /// <summary>
        /// Export records to CSV
        /// </summary>
        private void exportRecordToCsv(string exportDirectoryPath)
        {
            DBClient.Connect();

            foreach (var targetTable in BotConfig.Target)
            {
                var records = DBClient.ExecuteQuery($"select * from {targetTable}");

                using (var writer = new StreamWriter(Path.Combine(exportDirectoryPath, $"{targetTable}.csv"), true))
                using (var csvWriter = new CsvWriter(writer, CultureInfo.CurrentCulture))
                {
                    records.ForEach(record =>
                    {
                        csvWriter.WriteField(record);
                        csvWriter.NextRecord();
                    });
                    csvWriter.NextRecord();
                    csvWriter.Flush();
                }
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

            var directory = Path.Combine(exportDirectoryRootPath, "DB");

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
        ~DBCrawler()
        {
            Dispose(false);
        }
    }
}
