using MySql.Data.MySqlClient;
using LoyaltyPoints.Models;

namespace LoyaltyPoints.Data
{
    public class UserRepository
    {
        // Make sure this matches your XAMPP settings
        private string connStr = "Server=localhost;Database=ubaldo_db;Uid=root;Pwd=;";

        public User GetUser(string u, string p)
        {
            using var c = new MySqlConnection(connStr);
            c.Open();
            var cmd = new MySqlCommand("SELECT * FROM users WHERE username=@u AND password=@p", c);
            cmd.Parameters.AddWithValue("@u", u);
            cmd.Parameters.AddWithValue("@p", p);

            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;

            return new User
            {
                Id = r.GetInt32("id"),
                Username = r.GetString("username"),
                Points = r.GetInt32("points")
            };
        }

        public void UpdatePoints(int id, int pts)
        {
            using var c = new MySqlConnection(connStr);
            c.Open();
            var cmd = new MySqlCommand("UPDATE users SET points=@p WHERE id=@id", c);
            cmd.Parameters.AddWithValue("@p", pts);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        public void CreateUser(string u, string p)
        {
            using var c = new MySqlConnection(connStr);
            c.Open();
            var cmd = new MySqlCommand("INSERT INTO users (username, password, points) VALUES (@u, @p, 0)", c);
            cmd.Parameters.AddWithValue("@u", u);
            cmd.Parameters.AddWithValue("@p", p);
            cmd.ExecuteNonQuery();
        }
    }
}