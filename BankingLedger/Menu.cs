using System;

namespace BankingLedger
{
    // The Menu class prompts the user with menu options
    public static class Menu
    {
        public static int promptCount { get; set; }
        public const int LIMIT = 4;

        // A welcome message
        public static void WelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("\nWelcome to the Most Amazing Bank!\n");
        }

        // A welcome menu that returns valid key stroke options
        public static ConsoleKey[] WelcomeMenu()
        {
            Console.WriteLine("\nPlease select from the following options:");
            Console.WriteLine("(1) Login");
            Console.WriteLine("(2) Create Account");
            Console.WriteLine("(Esc) Exit Program");
            Console.WriteLine("\nExample: To create an account, you would type 2\n");

            return new ConsoleKey[] {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Escape};
        }

        // A user account menu that returns valid key stroke options
        public static ConsoleKey[] MainMenu()
        {
            Console.WriteLine("\nPlease select from the following options:");
            Console.WriteLine("(1) Deposit");
            Console.WriteLine("(2) Withdrawal");
            Console.WriteLine("(3) Check Balance");
            Console.WriteLine("(4) View Recent Transactions");
            Console.WriteLine("(5) Logout\n");

            return new ConsoleKey[] {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, 
                                     ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.Escape};
        }

        // increment prompt count
        public static void increasePromptCount()
        {
            promptCount++;
        }

        // reset prompt
        public static void resetPrompt()
        {
            promptCount = 0;
        }

        // wrapper: exit program
        public static void Exit()
        {
            _exitProgram();
        }

        // exit program due to too many invalid key presses
        public static void Exit_TooManyInvalidKeyPresses()
        {
            Console.WriteLine("Too many invalid selections were made. Goodbye!");
            _exitProgram();
        }

        // exit program
        private static void _exitProgram()
        {
            Environment.Exit(1);
        }


    }
}