using System;
using UbaldoAppService;
using UbaldoDataService;

namespace UbaldoLoyaltyProgram
{
    internal class Program
    {
        // Initialized with the combined UserRepository which handles both SQL and JSON
        static LoyaltyService service = new LoyaltyService(new UserRepository());

        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("=====================================");
                    Console.WriteLine("       WELCOME TO NEIL'S SHOP        ");
                    Console.WriteLine("=====================================");
                    Console.Write("Are you a member? (Y/N): ");

                    string input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input)) continue;

                    char memberAns = Char.ToLower(input[0]);

                    if (memberAns == 'y')
                    {
                        if (Login()) LoyaltyMenu();
                    }
                    else if (memberAns == 'n')
                    {
                        CreateAccount();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n[CRITICAL ERROR] Check if XAMPP MySQL is running!");
                Console.WriteLine("Details: " + ex.Message);
                Console.WriteLine("\nPress Enter to exit...");
                Console.ReadLine();
            }
        }

        static void CreateAccount()
        {
            Console.Clear();
            Console.WriteLine("--- CREATE NEW ACCOUNT ---");
            Console.Write("Do you want to create an account? (Y/N): ");

            string input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && Char.ToLower(input[0]) == 'y')
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine();
                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                service.CreateAccount(username, password);

                Console.WriteLine("\n[SUCCESS] Account saved to MySQL and JSON!");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }

        static bool Login()
        {
            for (int i = 0; i < 3; i++)
            {
                Console.Clear();
                Console.WriteLine("--- MEMBER LOGIN ---");
                Console.Write("Username: ");
                string u = Console.ReadLine();
                Console.Write("Password: ");
                string p = Console.ReadLine();

                if (service.Login(u, p))
                {
                    Console.WriteLine("\nLogin Successful!");
                    System.Threading.Thread.Sleep(1000);
                    return true;
                }

                Console.WriteLine($"\nInvalid credentials. Attempts left: {2 - i}");
                if (i < 2) Console.WriteLine("Press Enter to try again...");
                Console.ReadLine();
            }
            return false;
        }

        static void LoyaltyMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("--- LOYALTY POINTS MENU ---");
                Console.WriteLine("1. Add Points\n2. Use Points\n3. View Points\n4. Logout");
                Console.Write("\nChoice: ");

                if (!int.TryParse(Console.ReadLine(), out int ans)) continue;
                if (ans == 4) break;

                switch (ans)
                {
                    case 1: AddPoints(); break;
                    case 2: UsePoints(); break;
                    case 3: ViewPoints(); break;
                }
            }
        }

        static void AddPoints()
        {
            Console.Write("\nEnter Total Spent: ");
            if (int.TryParse(Console.ReadLine(), out int spent))
            {
                int earned = service.AddPoints(spent);
                Console.WriteLine(earned > 0 ? $"[SUCCESS] Earned {earned} points!" : "[INFO] Amount below 500.");
            }
            else Console.WriteLine("[ERROR] Invalid number.");

            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }

        static void UsePoints()
        {
            Console.Clear();
            Console.WriteLine($"Current Balance: {service.GetPoints()} points");
            Console.WriteLine("------------------------------");
            Console.WriteLine("1. 5% Discount (100 pts)\n2. 10% Discount (200 pts)\n3. 20% Discount (400 pts)");
            Console.WriteLine("------------------------------");
            Console.Write("Select Reward: ");

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (service.UsePoints(choice)) Console.WriteLine("\n[SUCCESS] Reward applied!");
                else Console.WriteLine("\n[FAILED] Insufficient points or invalid choice.");
            }

            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }

        static void ViewPoints()
        {
            Console.Clear();
            Console.WriteLine("--- POINT BALANCE ---");
            Console.WriteLine($"\nUser Account: Total Points = {service.GetPoints()}");
            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }
    }
}