using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using MySql.Data.MySqlClient;
using UbaldoModels;

namespace UbaldoDataService
{
    // --- PART 1: SQL REPOSITORY ---
    public class SqlUserRepository : IUserRepository
    {
        private string _activeUser;

        public int CreateUser(string u, string p, int existingId = 0)
        {
            using var c = Database.GetConnection(); c.Open();
            // Insert and immediately ask MySQL for the ID it just generated
            var cmd = new MySqlCommand("INSERT INTO users (username, password, points) VALUES (@u, @p, 0); SELECT LAST_INSERT_ID();", c);
            cmd.Parameters.AddWithValue("@u", u);
            cmd.Parameters.AddWithValue("@p", p);
            return Convert.ToInt32(cmd.ExecuteScalar());
        }

        public User GetUser(string u, string p)
        {
            using var c = Database.GetConnection(); c.Open();
            var cmd = new MySqlCommand("SELECT * FROM users WHERE username=@u AND password=@p", c);
            cmd.Parameters.AddWithValue("@u", u); cmd.Parameters.AddWithValue("@p", p);
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return null;
            _activeUser = u;
            return new User { Id = r.GetInt32("id"), Username = u, Points = r.GetInt32("points") };
        }

        public void UpdatePoints(int pts)
        {
            using var c = Database.GetConnection(); c.Open();
            var cmd = new MySqlCommand("UPDATE users SET points=@p WHERE username=@u", c);
            cmd.Parameters.AddWithValue("@p", pts); cmd.Parameters.AddWithValue("@u", _activeUser);
            cmd.ExecuteNonQuery();
        }

        public int GetCurrentPoints()
        {
            using var c = Database.GetConnection(); c.Open();
            var cmd = new MySqlCommand("SELECT points FROM users WHERE username=@u", c);
            cmd.Parameters.AddWithValue("@u", _activeUser);
            object res = cmd.ExecuteScalar();
            return res != null ? Convert.ToInt32(res) : 0;
        }

        public void DeleteUser(string u)
        {
            using var c = Database.GetConnection(); c.Open();
            var cmd = new MySqlCommand("DELETE FROM users WHERE username=@u", c);
            cmd.Parameters.AddWithValue("@u", u);
            cmd.ExecuteNonQuery();
        }
    }

    // --- PART 2: JSON REPOSITORY ---
    public class JsonUserRepository : IUserRepository
    {
        private readonly string _path = "users.json";
        private string _activeUser;

        private List<User> Load() => File.Exists(_path) ? JsonSerializer.Deserialize<List<User>>(File.ReadAllText(_path)) : new List<User>();
        private void Save(List<User> list) => File.WriteAllText(_path, JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));

        public int CreateUser(string u, string p, int id = 0)
        {
            var l = Load();
            l.Add(new User { Id = id, Username = u, Password = p, Points = 0 });
            Save(l);
            return id;
        }

        public User GetUser(string u, string p)
        {
            var user = Load().FirstOrDefault(x => x.Username == u && x.Password == p);
            if (user != null) _activeUser = u;
            return user;
        }

        public void UpdatePoints(int pts)
        {
            var l = Load(); var u = l.FirstOrDefault(x => x.Username == _activeUser);
            if (u != null) { u.Points = pts; Save(l); }
        }

        public int GetCurrentPoints() => Load().FirstOrDefault(x => x.Username == _activeUser)?.Points ?? 0;

        public void DeleteUser(string u)
        {
            var l = Load();
            var item = l.FirstOrDefault(x => x.Username.Equals(u, StringComparison.OrdinalIgnoreCase));
            if (item != null) { l.Remove(item); Save(l); }
        }
    }

    // --- PART 3: THE MASTER (Syncs both) ---
    public class UserRepository : IUserRepository
    {
        private readonly IUserRepository _sql = new SqlUserRepository();
        private readonly IUserRepository _json = new JsonUserRepository();

        public int CreateUser(string u, string p, int id = 0)
        {
            // 1. Save to SQL and get the database ID
            int newId = _sql.CreateUser(u, p);
            // 2. Pass that exact ID to the JSON file
            _json.CreateUser(u, p, newId);
            return newId;
        }

        public User GetUser(string u, string p)
        {
            var res = _sql.GetUser(u, p);
            if (res != null) _json.GetUser(u, p);
            return res;
        }

        public void UpdatePoints(int pts) { _sql.UpdatePoints(pts); _json.UpdatePoints(pts); }
        public int GetCurrentPoints() => _sql.GetCurrentPoints();
        public void DeleteUser(string u) { _sql.DeleteUser(u); _json.DeleteUser(u); }
    }
}