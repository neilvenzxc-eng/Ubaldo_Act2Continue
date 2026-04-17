using UbaldoModels;

namespace UbaldoDataService
{
    public interface IUserRepository
    {
        // Changed to int to support ID syncing
        int CreateUser(string username, string password, int existingId = 0);
        User GetUser(string username, string password);
        void UpdatePoints(int points);
        int GetCurrentPoints();
        void DeleteUser(string username);
    }
}