using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            bool accountMenu = false;
            User user = new User();

            // Welcome user
            Menu.WelcomeMessage();

            // Provide welcome menu options and check for valid selection, 
            // until program is exited or next menu is entered
            do {
                Menu.resetPrompt();
                ConsoleKey[] validOptions = Menu.WelcomeMenu();
                ConsoleKey selection = Console.ReadKey(true).Key;

                // Re-prompting up to the maximum number of prompt allowances
                while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && Menu.promptCount < Menu.LIMIT) {
                    Menu.increasePromptCount();;
                    Console.WriteLine($"Invalid option selected. Please try again (Attempt {Menu.promptCount})\n");
                    _ = Menu.WelcomeMenu();
                    selection = Console.ReadKey(true).Key;
                }

                // Program exits if too many unsuccessful selections were attempted
                if (Menu.promptCount >= Menu.LIMIT) {
                    Menu.Exit_TooManyInvalidKeyPresses();
                }

                // Handle user's selection
                switch (selection) {
                    case ConsoleKey.D1:
                        // User wants to login
                        Console.WriteLine("User Login");
                        if (!user.login()) {
                            Console.WriteLine($"\nYou may try logging in again, or if you continue to have difficulties");
                            Console.WriteLine("please contact support at {contact point} for further help.");
                        } else {
                            Console.Clear();
                            Console.WriteLine($"Credentials Verified.\n");
                            accountMenu = true;
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
            } while (!exit && !accountMenu);

            exit = false;
            Console.WriteLine($"Welcome {user.FirstName} {user.LastName}\n");

            do {
                Menu.resetPrompt();
                ConsoleKey[] validOptions = Menu.MainMenu();
                ConsoleKey selection = Console.ReadKey(true).Key;

                // Re-prompting up to the maximum number of prompt allowances
                while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && Menu.promptCount < Menu.LIMIT) {
                    Menu.increasePromptCount();;
                    Console.WriteLine($"Invalid option selected. Please try again (Attempt {Menu.promptCount})\n");
                    _ = Menu.MainMenu();
                    selection = Console.ReadKey(true).Key;
                }

                // Program exits if too many unsuccessful selections were attempted
                if (Menu.promptCount >= Menu.LIMIT) {
                    Menu.Exit_TooManyInvalidKeyPresses();
                }

                // Handle user's selection
                switch (selection) {
                    case ConsoleKey.D1:
                        // User wants to make a deposit
                        Console.WriteLine("Deposit");
                        break;
                    case ConsoleKey.D2:
                        // User wants to make a withdrawal
                        Console.WriteLine("Make a Withdrawal");
                        break;
                    case ConsoleKey.D3:
                        // User wants to check balance
                        Console.WriteLine("Check Balance");
                        break;
                    case ConsoleKey.D4:
                        // User wants to view transactions
                        Console.WriteLine("View transactions");
                        break;
                    default:
                        Console.WriteLine("Logout");
                        exit = true;
                        Menu.Exit();
                        break;
                }
            } while (!exit);
        }
    }
}
