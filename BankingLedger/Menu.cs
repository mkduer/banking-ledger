using System;

namespace BankingLedger
{
    // The Menu class prompts the user with menu options
    public static class Menu
    {

        // A welcome menu that returns valid key stroke options
        public static ConsoleKey[] WelcomeMenu()
        {
            Console.WriteLine("\nWelcome to the Most Amazing Bank!\n");
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("(1) Login");
            Console.WriteLine("(2) Create Account");
            Console.WriteLine("(Esc) Exit Program");
            Console.WriteLine("Example: To create an account, you would type 2\n");

            return new ConsoleKey[] {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Escape};
        }

    }
}