using System;
using System.Collections.Generic;

namespace Dosh.Middleware.DB
{
    /// <summary>
    /// DBClient interface
    /// </summary>
    public interface IDBClient : IDisposable
    {
        void Connect();

        List<Record> ExecuteQuery(string targetTable);

        int ExecuteNonQuery(string query);
    }
}
