using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            int promptCount = 1;
            Menu.WelcomeMessage();
            ConsoleKey[] validOptions = Menu.WelcomeMenu();
            ConsoleKey selection = Console.ReadKey(true).Key;
            Console.WriteLine($"user selected {selection}");

            while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && promptCount < 5) {
                promptCount++;
                Console.WriteLine($"Invalid option selected. Please try again (Attempt {promptCount})\n");
                _ = Menu.WelcomeMenu();
                selection = Console.ReadKey(true).Key;
                Console.WriteLine($"user selected {selection}");
            }

            if (promptCount >= 5) {
                Menu.Goodbye_TooManyInvalidKeyPresses();
            }

        }
    }
}
