using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKey[] validOptions = Menu.WelcomeMenu();
            ConsoleKey selection = Console.ReadKey(true).Key;

            Console.WriteLine($"user selected {selection}");
            if (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == true) {
                Console.WriteLine($"{selection} is in validOptions");
            }


        }
    }
}
