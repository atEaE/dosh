using System;
using System.Collections.Generic;

namespace Dosh.Middleware.DB.Middleware.Base
{
    /// <summary>
    /// DBClient interface
    /// </summary>
    public interface IDBClient : IDisposable
    {
        void Connect();

        List<List<string>> ExecuteQuery(string targetTable);

        int ExecuteNonQuery(string query);
    }
}
