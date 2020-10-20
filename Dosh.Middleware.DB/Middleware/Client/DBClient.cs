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
        ILogger Logger;

        public DBClient(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// Create dbConnection
        /// </summary>
        /// <param name="providerName">propvider name</param>
        /// <param name="connectionString">connection string</param>
        /// <returns>DbConnection Class. if the connection were not made, return null</returns>
        public DbConnection CreateDbConnection(string providerName, string connectionString)
        {
            DbConnection connection = null;

            if (connectionString != null)
            {
                try
                {
                    var factory = DbProviderFactories.GetFactory(providerName);

                    connection = factory.CreateConnection();
                    connection.ConnectionString = connectionString;
                }
                catch (Exception ex)
                {
                    if (connection != null)
                    {
                        connection = null;
                    }
                    Logger.OutputLog(LogEventLevel.Error, ex.Message);
                }
            }

            return connection;
        }

        /// <summary>
        /// Implement select query
        /// <summary>
        /// <param name="connection">Db connection</param>
        /// <param name="queryString">query string</param>
        public void DbCommandSelect(DbConnection connection, string queryString)
        {
            if (connection == null)
            {
                throw new Exception();
            }

            using (connection)
            {
                try
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = queryString;
                    command.CommandType = CommandType.Text;

                    connection.Open();

                    DbDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                    }
                }
                catch (Exception ex)
                {
                    Logger.OutputLog(LogEventLevel.Error, ex.Message);
                }
            }
        }

        /// <summary>
        /// Implement query
        /// </summary>
        /// <param name="connection">Db connection</param>
        /// <param name="queryString">query string</param>
        public void ExecuteDbCommand(DbConnection connection, string queryString)
        {
            if (connection == null)
            {
                throw new Exception();
            }

            using (connection)
            {
                try
                {
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = queryString;

                    connection.Open();

                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Logger.OutputLog(LogEventLevel.Error, ex.Message);
                }
            }
        }
    }
}
