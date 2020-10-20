using Dosh.Core.Logger;
using Dosh.Middleware.DB.Middleware.Base;
using Serilog.Events;
using System;
using System.Data;
using System.Data.Common;

namespace Dosh.Middleware.DB.Middleware.Client
{
    /// <summary>
    /// DBClient Class.
    /// </summary>
    public class DBClient : IDBClient
    {

        /// <summary>
        /// DbConnection
        /// </summary>
        private DbConnection Connection = null;

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
        /// <param name="logger"></param>
        public DBClient(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Create dbConnection
        /// </summary>
        /// <param name="providerName">propvider name</param>
        /// <param name="connectionString">Connection string</param>
        /// <returns>DbConnection Class. if the Connection were not made, return null</returns>
        public DbConnection CreateDbConnection(string providerName, string connectionString)
        {
            if (connectionString != null)
            {
                try
                {
                    var factory = DbProviderFactories.GetFactory(providerName);

                    Connection = factory.CreateConnection();
                    Connection.ConnectionString = connectionString;
                    Connection.Open();
                }
                catch (Exception ex)
                {
                    if (Connection != null)
                    {
                        Connection = null;
                    }
                    Logger.OutputLog(LogEventLevel.Error, ex.Message);
                }
            }

            return Connection;
        }

        /// <summary>
        /// Implement select query
        /// <summary>
        /// <param name="queryString">query string</param>
        public void DbCommandSelect(string queryString)
        {
            if (Connection == null)
            {
                throw new Exception();
            }

            try
            {
                DbCommand command = Connection.CreateCommand();
                command.CommandText = queryString;
                command.CommandType = CommandType.Text;

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                }

                if (!reader.IsClosed) reader.Close();
            }
            catch (Exception ex)
            {
                Logger.OutputLog(LogEventLevel.Error, ex.Message);
            }
        }

        /// <summary>
        /// Implement query
        /// </summary>
        /// <param name="queryString">query string</param>
        public void ExecuteDbCommand(string queryString)
        {
            if (Connection == null)
            {
                throw new Exception();
            }

            try
            {
                DbCommand command = Connection.CreateCommand();
                command.CommandText = queryString;

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.OutputLog(LogEventLevel.Error, ex.Message);
            }
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
                Connection.Close();
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
    }
}
