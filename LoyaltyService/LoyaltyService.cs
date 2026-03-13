using UbaldoModels;
using UbaldoDataService;

namespace UbaldoAppService
{
    public class LoyaltyService
    {
        private UserRepository repo;

        public LoyaltyService(UserRepository repository)
        {
            repo = repository;
        }

        public bool Login(string username, string password)
        {
            User user = repo.GetUser();

            if (user.Username == username && user.Password == password)
            {
                return true;
            }

            return false;
        }

        public void CreateAccount(string username, string password)
        {
            repo.CreateUser(username, password);
        }

        public int GetPoints()
        {
            return repo.GetUser().Points;
        }

        public int AddPoints(int moneySpent)
        {
            int points = repo.GetUser().Points;
            int earned = 0;

            if (moneySpent < 500)
            {
                earned = 0;
            }
            else if (moneySpent < 1000)
            {
                earned = 5;
            }
            else if (moneySpent < 2000)
            {
                earned = 15;
            }
            else if (moneySpent < 3000)
            {
                earned = 30;
            }
            else if (moneySpent < 4000)
            {
                earned = 50;
            }
            else if (moneySpent < 5000)
            {
                earned = 75;
            }
            else if (moneySpent < 7000)
            {
                earned = 100;
            }
            else
            {
                earned = 150;
            }

            points = points + earned;

            repo.UpdatePoints(points);

            return earned;
        }

        public bool UsePoints(int option)
        {
            int points = repo.GetUser().Points;

            switch (option)
            {
                case 1:
                    if (points >= 100) { points -= 100; repo.UpdatePoints(points); return true; }
                    break;
                case 2:
                    if (points >= 200) { points -= 200; repo.UpdatePoints(points); return true; }
                    break;
                case 3:
                    if (points >= 400) { points -= 400; repo.UpdatePoints(points); return true; }
                    break;
            }

            return false;
        }
    }
}