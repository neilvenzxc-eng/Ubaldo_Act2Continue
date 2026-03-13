using System;
using UbaldoAppService;
using UbaldoDataService;

namespace UbaldoLoyaltyProgram
{
    internal class Program
    {
        static LoyaltyService service = new LoyaltyService(new UserRepository());

        static void Main(string[] args)
        {
            char memberAns;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("       WELCOME TO NEIL'S SHOP        ");
                Console.WriteLine("=====================================");
                Console.Write("Are you a member? (Y/N): ");
                memberAns = Char.ToLower(Console.ReadLine()[0]);

                if (memberAns == 'y')
                {
                    if (Login())
                    {
                        LoyaltyMenu();
                    }
                }
                else if (memberAns == 'n')
                {
                    CreateAccount();
                }
            }
        }

        static void CreateAccount()
        {
            Console.Clear();
            Console.Write("Do you want to create an account? (Y/N): ");
            char createAccAns = Char.ToLower(Console.ReadLine()[0]);

            if (createAccAns == 'y')
            {
                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                service.CreateAccount(username, password);

                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("        ACCOUNT CREATED!             ");
                Console.WriteLine(" You can now log in as a member.    ");
                Console.WriteLine("=====================================\n");
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }

        static bool Login()
        {
            int attempts = 0;

            while (attempts < 3)
            {
                Console.Clear();
                Console.WriteLine("=====================================");
                Console.WriteLine("          MEMBER LOGIN                ");
                Console.WriteLine("=====================================");

                Console.Write("Enter username: ");
                string username = Console.ReadLine();

                Console.Write("Enter password: ");
                string password = Console.ReadLine();

                if (service.Login(username, password))
                {
                    Console.Clear();
                    Console.WriteLine("=====================================");
                    Console.WriteLine("        LOGIN SUCCESSFUL!            ");
                    Console.WriteLine("=====================================\n");
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadLine();
                    return true;
                }

                attempts++;

                Console.WriteLine("\nIncorrect username or password. Attempts left: " + (3 - attempts));
                Console.WriteLine("Press Enter to try again...");
                Console.ReadLine();
            }

            Console.WriteLine("\nToo many failed attempts. Program will exit.");
            Console.ReadLine();
            return false;
        }

        static void LoyaltyMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n=====================================");
                Console.WriteLine("          LOYALTY POINTS MENU         ");
                Console.WriteLine("=====================================");
                Console.WriteLine("1. Add Points");
                Console.WriteLine("2. Use Points");
                Console.WriteLine("3. View Points");
                Console.WriteLine("4. Exit");
                Console.WriteLine("=====================================");
                Console.Write("Enter your choice: ");

                int ans = Convert.ToInt32(Console.ReadLine());

                switch (ans)
                {
                    case 1:
                        AddPoints();
                        break;
                    case 2:
                        UsePoints();
                        break;
                    case 3:
                        ViewPoints();
                        break;
                    case 4:
                        return;
                }
            }
        }

        static void AddPoints()
        {
            Console.Write("Enter your total Spent: ");
            int moneySpent = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            int earned = service.AddPoints(moneySpent);

            if (earned == 0)
                Console.WriteLine("No points added. Your Total Spent is below 500.");
            else
                Console.WriteLine("You have earned " + earned + " points");

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        static void UsePoints()
        {
            Console.Clear();

            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Your current points: " + service.GetPoints());
            Console.WriteLine("Available Discounts:");
            Console.WriteLine("1. 5% Discount = 100 points");
            Console.WriteLine("2. 10% Discount = 200 points");
            Console.WriteLine("3. 20% Discount = 400 points");
            Console.WriteLine("-------------------------------------");

            Console.Write("Enter the number where you want to use your points: ");
            int ans = Convert.ToInt32(Console.ReadLine());

            if (service.UsePoints(ans))
            {
                if (ans == 1)
                {
                    Console.WriteLine("You have used 100 points for 5% Discount");
                }
                else if (ans == 2)
                {
                    Console.WriteLine("You have used 200 points for 10% Discount");
                }
                else if (ans == 3)
                {
                    Console.WriteLine("You have used 400 points for 20% Discount");
                }
            }
            else
            {
                Console.WriteLine("Invalid option or not enough points.");
            }

            Console.WriteLine("Your current points: " + service.GetPoints());

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }

        static void ViewPoints()
        {
            Console.Clear();
            Console.WriteLine("-------------------------------------");
            Console.WriteLine("Your current points: " + service.GetPoints());
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("\nPress Enter to return to menu...");
            Console.ReadLine();
        }
    }
}