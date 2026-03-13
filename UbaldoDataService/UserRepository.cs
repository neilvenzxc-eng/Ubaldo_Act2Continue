using UbaldoModels;

namespace UbaldoDataService
{
    public class UserRepository
    {
        private User user = new User();

        public void CreateUser(string username, string password)
        {
            user.Username = username;
            user.Password = password;
            user.Points = 0;
        }

        public User GetUser()
        {
            return user;
        }

        public void UpdatePoints(int points)
        {
            user.Points = points;
        }
    }
}