using MySql.Data.MySqlClient;

namespace UbaldoDataService
{
    public static class Database
    {
        // Default XAMPP Connection String
        private static string connString = "server=localhost;database=ubaldo_db;uid=root;pwd=;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connString);
        }
    }
}