namespace Ubaldo_Act2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                int ans,
                    ans2,
                    moneySpent,
                    Points = 0;

                char memberAns;

                while (true)
                {
                    Console.Write("Are you a member? (Y/N)");
                    memberAns = Console.ReadLine();
                    memberAns = memberAns.ToLower();

                    if (memberAns == 'y')
                    {
                        Console.WriteLine("");
                        Console.WriteLine("LOYALTY POINTS");
                        Console.WriteLine("");
                        Console.WriteLine("1. Add | 2.Use Points | 3. View Points");
                        Console.WriteLine("");
                        Console.Write("Enter Number: ");
                        ans = Convert.ToInt16(Console.ReadLine());

                        if (ans == 1)
                        {
                            Console.WriteLine("");
                            Console.Write("Enter your total Spent: ");
                            moneySpent = Convert.ToInt16(Console.ReadLine());
                            Console.WriteLine("");

                            if (moneySpent < 500)
                            {
                                Console.WriteLine("No points added. Your Total Spent is below 500.");
                            }
                            else if (moneySpent <= 500)
                            {
                                Points = Points + 5;
                                Console.WriteLine("You have earned 5 points");
                            }
                            else if (moneySpent >= 1000 && moneySpent < 2000)
                            {
                                Points = Points + 10;
                                Console.WriteLine("You have earned 10 points");
                            }
                            else if (moneySpent >= 2000 && moneySpent < 3000)
                            {
                                Points = Points + 20;
                                Console.WriteLine("You have earned 20 points");
                            }
                            else if (moneySpent >= 3000 && moneySpent < 4000)
                            {
                                Points = Points + 30;
                                Console.WriteLine("You have earned 30 points");
                            }
                            else if (moneySpent >= 4000 && moneySpent < 5000)
                            {
                                Points = Points + 40;
                                Console.WriteLine("You have earned 40 points");
                            }
                            else if (moneySpent >= 5000 && moneySpent < 6000)
                            {
                                Points = Points + 50;
                                Console.WriteLine("You have earned 50 points");
                            }
                            else if (moneySpent >= 6000 && moneySpent < 7000)
                            {
                                Points = Points + 60;
                                Console.WriteLine("You have earned 60 points");
                            }
                            else if (moneySpent >= 4000 && moneySpent < 5000)
                            {
                                Points = Points + 70;
                                Console.WriteLine("You have earned 70 points");
                            }
                            else if (moneySpent >= 5000 && moneySpent < 6000)
                            {
                                Points = Points + 80;
                                Console.WriteLine("You have earned 80 points");
                            }
                            else if (moneySpent >= 6000 && moneySpent < 7000)
                            {
                                Points = Points + 90;
                                Console.WriteLine("You have earned 90 points");
                            }
                            else if (moneySpent >= 1000)
                            {
                                Points = Points + 100;
                                Console.WriteLine("You have earned 100 points");
                            }
                            else
                            {
                                Console.WriteLine("Invalid.");
                            }


                        }
                        else if (ans == 2)
                        {
                            Console.WriteLine("-------------------------------------------------");
                            Console.WriteLine("Your current points: " + Points);
                            Console.WriteLine("------- Available Offer to Use The Points -------");
                            Console.WriteLine("1. Discount = 100 points");
                            Console.WriteLine("2. N/A");
                            Console.WriteLine("3. N/A");
                            Console.WriteLine("-------------------------------------------------");
                            Console.Write("Enter the number where you wanna use your points: ");
                            ans2 = Convert.ToInt16(Console.ReadLine());

                            if (ans2 == 1)
                            {
                                Console.WriteLine("(\"-------------------------------------------------\");");
                                Points = Points - 100;
                                Console.WriteLine("You have used 100 points for 10% Discount");
                                Console.WriteLine("Your current points: " + Points);
                            }
                            else
                            {
                                Console.WriteLine("-------------------------------------------------");
                                Console.WriteLine("Invalid.");
                            }
                        }
                        else if (ans == 3)
                        {
                            Console.WriteLine("-------------------------------------------------");
                            Console.WriteLine("Your current points: " + Points);
                        }
                        else
                        {
                            Console.WriteLine("-------------------------------------------------");
                            Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                        }
                    }
                    else if (memberAns == 'n')
                    {
                        Console.WriteLine("Do you want to create an account? (Y/N): ");
                        string createAccAns = Console.ReadLine();
                    }
                }
            }
        }
    }
}