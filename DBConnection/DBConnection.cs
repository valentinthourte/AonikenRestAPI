using Microsoft.Data.SqlClient;

namespace AonikenRestAPI.Connections
{
    public static class DBConnection
    {
        public static string getConnectionString(string DBName)
        {
            using IHost host = Host.CreateDefaultBuilder().Build();

            var config = host.Services.GetService<IConfiguration>();

            string cnxStr = config.GetConnectionString("cnxStr") ?? "";
            return cnxStr.Replace("%s", DBName);
        }

        public static async Task<SqlCommand> getConnectedSqlCommand(string query)
        {
            SqlCommand result;
            SqlConnection sql = new SqlConnection(getConnectionString("Aoniken")); // Un problema con esto es que se instancia una nueva conexión cada vez que se solicita un command
            await sql.OpenAsync();                                                 // Valdría la pena implementar en un proyecto de mayor escala
            result = new SqlCommand(query, sql);
            return result;
        }
    }
}
