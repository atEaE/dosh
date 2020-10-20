using System.Data.Common;

namespace Dosh.Middleware.DB.Middleware.Base
{
    interface IDBClient
    {
        DbConnection CreateDbConnection(string providerName, string connectionString);

        void DbCommandSelect(DbConnection connection, string queryString);

        void ExecuteDbCommand(DbConnection connection, string queryString);
    }
}
