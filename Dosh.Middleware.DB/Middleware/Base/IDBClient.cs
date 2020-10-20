using System;
using System.Data.Common;

namespace Dosh.Middleware.DB.Middleware.Base
{
    public interface IDBClient : IDisposable
    {
        DbConnection CreateDbConnection(string providerName, string connectionString);

        void DbCommandSelect(string queryString);

        void ExecuteDbCommand(string queryString);
    }
}
