using System;

namespace BankingLedger
{
    // The Menu class prompts the user with menu options
    public static class Menu
    {
        // A welcome message
        public static void WelcomeMessage()
        {
            Console.WriteLine("\nWelcome to the Most Amazing Bank!\n");
        }

        // A welcome menu that returns valid key stroke options
        public static ConsoleKey[] WelcomeMenu()
        {
            Console.WriteLine("Please select from the following options:");
            Console.WriteLine("(1) Login");
            Console.WriteLine("(2) Create Account");
            Console.WriteLine("(Esc) Exit Program");
            Console.WriteLine("Example: To create an account, you would type 2\n");

            return new ConsoleKey[] {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Escape};
        }


        public static void Goodbye_TooManyInvalidKeyPresses()
        {
            Console.WriteLine("Too many invalid selections were made. Goodbye!");
            Environment.Exit(1);
        }
    }
}