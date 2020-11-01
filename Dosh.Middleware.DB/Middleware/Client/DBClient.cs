using Dosh.Core.Logger;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using static Dosh.Middleware.DB.Properties.Resources;

namespace Dosh.Middleware.DB
{
    /// <summary>
    /// DBClient Class.
    /// </summary>
    public class DBClient : IDBClient
    {
        /// <summary>
        /// DB propvider
        /// </summary>
        private readonly string provider;

        /// <summary>
        /// Connectin strings
        /// </summary>
        private readonly string connectionStrings;

        /// <summary>
        /// DbConnection
        /// </summary>
        private DbConnection dbConn;

        /// <summary> 
        /// Resource disposed flag 
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Logger
        /// </summary>
        private readonly ILogger Logger;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="provider">DB provider</param>
        /// <param name="connectionStrings">Connection strings</param>
        /// <param name="logger">logger</param>
        public DBClient(string provider, string connectionStrings, ILogger logger)
        {
            if (string.IsNullOrEmpty(provider))
            {
                throw new ArgumentNullException(nameof(provider));
            }

            if (string.IsNullOrEmpty(connectionStrings))
            {
                throw new ArgumentNullException(nameof(connectionStrings));
            }

            this.provider = provider;
            this.connectionStrings = connectionStrings;
            Logger = logger;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbConn">DB connection</param>
        public DBClient(DbConnection dbConn)
        {
            this.dbConn = dbConn;
        }

        /// <summary>
        /// connect database.
        /// </summary>
        public void Connect()
        {
            try
            {
                if (dbConn != null)
                {
                    openConnect();
                }
                else
                {
                    dbConn = newConnect();
                }

                Logger.OutputLog(LogEventLevel.Debug, string.Format(DB_0001, dbConn.Database));
            }
            catch(Exception ex)
            {
                if (dbConn != null)
                {
                    dbConn.Close();
                }
                throw ex;
            }
        }

        private void openConnect()
        {
            if (dbConn.State == ConnectionState.Broken)
            {
                throw new InvalidOperationException("The connection is broken.");
            }

            if (dbConn.State == ConnectionState.Closed)
            {
                dbConn.Open();
            }
        }

        private DbConnection newConnect()
        {
            var factory = DbProviderFactories.GetFactory(provider);
            var dbConn = factory.CreateConnection();
            dbConn.ConnectionString = connectionStrings;
            dbConn.Open();
            return dbConn;
        }

        /// <summary>
        /// disconnect database.
        /// </summary>
        public void Disconnect()
        {
            dbConn.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns></returns>
        public List<Record> ExecuteQuery(string query)
        {
            if (dbConn == null)
            {
                throw new InvalidOperationException("The connection to the DB does not exist.");
            }

            if (dbConn.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("The DB connection is not open.");
            }

            var records = new List<Record>();

            var command = dbConn.CreateCommand();
            command.CommandText = query;
            command.CommandType = CommandType.Text;
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var record = new Record();

                if (records.Count == 0)
                {
                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        record.Add(reader.GetName(i));
                    }
                    records.Add(record);
                    record = new Record();
                }

                for (var i = 0; i < reader.FieldCount; i++)
                {
                    record.Add(reader.GetValue(i).ToString());
                }

                records.Add(record);
                Logger.OutputLog(LogEventLevel.Debug, string.Format(DB_0002, query, record));
            }

            if (!reader.IsClosed) reader.Close();

            return records;
        }

        /// <summary>
        /// Implement query
        /// </summary>
        /// <param name="query">query string</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query)
        {
            if (dbConn == null)
            {
                throw new InvalidOperationException("The connection to the DB does not exist.");
            }

            if (dbConn.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("The DB connection is not open.");
            }

            var command = dbConn.CreateCommand();
            command.CommandText = query;
            var result = command.ExecuteNonQuery();

            Logger.OutputLog(LogEventLevel.Debug, string.Format(DB_0003, query));
            return result;
        }

        /// <summary>
        /// getProviderFactory
        /// </summary>
        /// <param name="providerName">providerName</param>
        /// <returns>DbProviderFactory</returns>
        private DbProviderFactory getProviderFactory(string providerName)
        {

            DbProviderFactory factory;

            if (dbConn != null) factory = DbProviderFactories.GetFactory(dbConn);
            else factory = DbProviderFactories.GetFactory(providerName);
            
            return factory;
        }

        #region Dispose function

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
                {
                    Disconnect();
                }
                disposed = true;
            }
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~DBClient()
        {
            Dispose(false);
        }

        #endregion
    }
}
