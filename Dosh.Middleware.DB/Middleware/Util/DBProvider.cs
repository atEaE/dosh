namespace Dosh.Middleware.DB
{
    public readonly struct DBProviderName
    {
        public static readonly string SqlServer = "System.Data.SqlClient";
    }

    public static class DBProvider
    {
        public static string GetProviderName(string typeName)
        {
            string providerName;

            switch (typeName.ToLower())
            {
                case "sqlserver":
                    providerName = DBProviderName.SqlServer;
                    break;

                default:
                    providerName = null;
                    break;
            }

            return providerName;
        }
    }
}
