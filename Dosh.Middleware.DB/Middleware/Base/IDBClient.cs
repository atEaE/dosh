using System;
using System.Collections.Generic;
using System.Data;

namespace Dosh.Middleware.DB.Middleware.Base
{
    public interface IDBClient : IDisposable
    {
        IDbConnection CreateDbConnection(string providerName, string connectionString);

        List<List<string>> DbCommandSelect(string targetTable);

        void ExecuteDbCommand(string queryString);
    }
}
