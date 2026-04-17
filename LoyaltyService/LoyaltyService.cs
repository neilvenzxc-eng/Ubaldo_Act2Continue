using UbaldoModels;
using UbaldoDataService;

namespace UbaldoAppService
{
    public class LoyaltyService
    {
        private readonly IUserRepository _repo;

        public LoyaltyService(IUserRepository repository)
        {
            _repo = repository;
        }

        public bool Login(string username, string password)
        {
            User user = _repo.GetUser(username, password);
            return user != null;
        }

        public void CreateAccount(string username, string password)
        {
            _repo.CreateUser(username, password);
        }

        public int GetPoints()
        {
            return _repo.GetCurrentPoints();
        }

        public int AddPoints(int moneySpent)
        {
            int currentPoints = GetPoints();
            int earned = 0;

            if (moneySpent >= 7000) earned = 150;
            else if (moneySpent >= 5000) earned = 100;
            else if (moneySpent >= 4000) earned = 75;
            else if (moneySpent >= 3000) earned = 50;
            else if (moneySpent >= 2000) earned = 30;
            else if (moneySpent >= 1000) earned = 15;
            else if (moneySpent >= 500) earned = 5;

            if (earned > 0)
            {
                _repo.UpdatePoints(currentPoints + earned);
            }

            return earned;
        }

        public bool UsePoints(int option)
        {
            int currentPoints = GetPoints();
            int cost = 0;

            switch (option)
            {
                case 1: cost = 100; break; // 5% Discount
                case 2: cost = 200; break; // 10% Discount
                case 3: cost = 400; break; // 20% Discount
                default: return false;
            }

            if (currentPoints >= cost)
            {
                _repo.UpdatePoints(currentPoints - cost);
                return true;
            }

            return false;
        }

        public void DeleteAccount(string username)
        {
            _repo.DeleteUser(username);
        }
    }
}