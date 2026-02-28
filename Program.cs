namespace Ubaldo_Act2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                int ans,
                    ans2,
                    totalSpent,
                    Points = 0,
                    newPoints = 0;

                while (true)
                {

                    Console.WriteLine("LOYALTY POINTS");
                    Console.WriteLine("1. Add | 2.Use Points | 3. View Points");
                    Console.Write("Enter Number: ");
                    ans = Convert.ToInt16(Console.ReadLine());

                    if (ans == 1)
                    {
                        Console.Write("Enter your total Spent: ");
                        totalSpent = Convert.ToInt16(Console.ReadLine());

                        if (totalSpent >= 1000 && totalSpent < 2000)
                        {
                            newPoints = Points + 100;
                            Console.WriteLine("You have earned 100 points");
                        }
                        else if (totalSpent >= 2000 && totalSpent < 2999)
                        {
                            newPoints = Points + 150;
                            Console.WriteLine("You have earned 150 points");
                        }
                        else
                        {
                            Points = 0;
                            Console.WriteLine("No points added. Total spent is below 1000.");
                        }


                    }
                    else if (ans == 2)
                    {
                        Console.WriteLine("Your current points: " + newPoints);
                        Console.WriteLine("------- Available Offer to Use The Points -------");
                        Console.WriteLine("1. Discount = 100 points");
                        Console.WriteLine("2. N/A");
                        Console.WriteLine("3. N/A");

                        Console.Write("Enter the number where you wanna use your points: ");
                        ans2 = Convert.ToInt16(Console.ReadLine());

                        if (ans2 == 1)
                        {
                            newPoints = newPoints - 100;
                            Console.WriteLine("You have used 100 points for 10% Discount");
                            Console.WriteLine("Your current points: " + newPoints);
                        }
                        else
                        {
                            Console.WriteLine("Invalid.");
                        }
                    }
                    else if (ans == 3)
                    {
                        Console.WriteLine("Your current points: " + newPoints);
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please select 1, 2, or 3.");
                    }
                }
            }

        }
    }
}
