using System.Data.SqlClient;

namespace Dosh.Middleware.DB.Middleware.Util
{
    public static class ConnectionString
    {
        public static string CreateConnectionString(string type, string host, string database, string userid, string password)
        {
            string connectionString;

            switch (type.ToLower())
            {
                case "sqlserver":
                    var builder = new SqlConnectionStringBuilder();
                    builder.DataSource = host;
                    builder.InitialCatalog = database;
                    builder.UserID = userid;
                    builder.Password = password;

                    connectionString = builder.ConnectionString;

                    break;

                default:
                    connectionString = null;
                    break;
            }

            return connectionString;

        }
    }
}
