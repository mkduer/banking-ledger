using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            int promptCount = 1;
            int maxPrompt = 5;

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
                    Console.WriteLine("selected option 1");
                    break;
                case ConsoleKey.D2:
                    Console.WriteLine("selected option 2");
                    break;
                default:
                    Menu.Exit();
                    break;
            }
        }
    }
}
