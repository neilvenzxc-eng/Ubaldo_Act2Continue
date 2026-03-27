namespace UbaldoModels
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int Points { get; set; }

        public User() { }

        public User(string username, string password, int points = 0)
        {
            Username = username;
            Password = password;
            Points = points;
        }
    }
}