using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            int promptCount = 1;
            int maxPrompt = 5;
            bool exit = false;
            User user = new User();

            // Welcome user
            Menu.WelcomeMessage();

            // Provide welcome menu options and check for valid selection, 
            // until program is exited
            while (!exit) {
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
                        Console.WriteLine("User Login");
                        if (!user.login()) {
                            Console.WriteLine($"\nThere were unfortunately issues creating your account. Please contact support at super secret number for further help");
                        } else {
                            Console.Clear();
                            Console.WriteLine($"Credentials Verified. Welcome {user.UserID}!");
                        }
                        break;
                    case ConsoleKey.D2:
                        // User wants to create account
                        Console.WriteLine("Create Account");
                        if (!user.createUser()) {
                            Console.WriteLine($"\nThere were unfortunately issues creating your account. Please contact support at super secret number for further help");
                        } else {
                            Console.Clear();
                            Console.WriteLine($"Your account was created successfully\n");
                            Console.WriteLine("ACCOUNT DETAILS:");
                            Console.WriteLine($"Username: {user.UserID}");
                            Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                        }
                        break;
                    default:
                        Console.WriteLine("Exit Program");
                        exit = true;
                        Menu.Exit();
                        break;
                }
            }
        }
    }
}
