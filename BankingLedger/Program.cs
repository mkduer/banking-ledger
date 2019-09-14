using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            int promptCount = 1;
            int maxPrompt = 5;
            User user = new User();

            // Welcome user
            Menu.WelcomeMessage();

            // Provide welcome menu options and check for valid selection
            ConsoleKey[] validOptions = Menu.WelcomeMenu();
            ConsoleKey selection = Console.ReadKey(true).Key;

            // Re-prompting up to the maximum number of prompt allowances
            while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && promptCount < maxPrompt) {
                promptCount++;
                Console.WriteLine($"Invalid option selected. Please try again (Attempt {promptCount})\n");
                _ = Menu.WelcomeMenu();
                selection = Console.ReadKey(true).Key;
            }

            // Program exits if too many unsuccessful selections were attempted
            if (promptCount >= 5) {
                Menu.Exit_TooManyInvalidKeyPresses();
            }

            // Handle user's selection
            switch (selection) {
                case ConsoleKey.D1:
                    // User wants to login
                    Console.WriteLine("Login");
                    break;
                case ConsoleKey.D2:
                    // User wants to create account
                    Console.WriteLine("Create Account");
                    if (!user.createUserAccount()) {
                        Console.Clear();
                        Console.WriteLine($"There were unfortunately issues creating your account. Please contact support at super secret number for further help");
                    } else {
                        Console.Clear();
                        Console.WriteLine($"Your account was created successfully {user.UserID}");
                    }
                    break;
                default:
                    Menu.Exit();
                    break;
            }
        }
    }
}
