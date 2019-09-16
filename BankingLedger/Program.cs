using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            bool accountMenu = false;
            User user = null;

            // Welcome user
            Interface.WelcomeMessage();

            // Provide welcome menu options and check for valid selection, 
            // until program is exited or next menu is entered
            do {
                Interface.resetPrompt();
                ConsoleKey[] validOptions = Interface.WelcomeMenu();
                ConsoleKey selection = Console.ReadKey(true).Key;

                // Re-prompting up to the maximum number of prompt allowances
                while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && Interface.promptCount < Interface.LIMIT) {
                    Interface.increasePromptCount();;
                    Console.WriteLine($"Invalid option selected. Please try again (Attempt {Interface.promptCount})\n");
                    _ = Interface.WelcomeMenu();
                    selection = Console.ReadKey(true).Key;
                }

                // Program exits if too many unsuccessful selections were attempted
                if (Interface.promptCount >= Interface.LIMIT) {
                    Interface.Exit_TooManyInvalidKeyPresses();
                }

                // Handle user's selection
                Console.Clear();
                switch (selection) {
                    case ConsoleKey.D1:
                        // User wants to login
                        Console.WriteLine("User Login");
                        if (!Interface.login(ref user)) {
                            Console.WriteLine($"\nYou may try logging in again, or if you continue to have difficulties");
                            Console.WriteLine("please contact support at {contact point} for further help.");
                        } else {
                            Console.WriteLine($"Credentials Verified.\n");
                            accountMenu = true;
                        }
                        break;
                    case ConsoleKey.D2:
                        // User wants to create account
                        Console.WriteLine("Create Account");
                        if (!Interface.createUser(ref user)) {
                            Console.WriteLine($"\nYou may try creating an account again, or if you continue to have difficulties");
                            Console.WriteLine("please contact support at {contact point} for further help.");
                        } else {
                            Console.WriteLine($"Your account was created successfully\n");
                            Console.WriteLine("ACCOUNT DETAILS:");
                            Console.WriteLine($"Username: {user.UserID}");
                            Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                        }
                        break;
                    default:
                        Console.WriteLine("Exit Program");
                        exit = true;
                        Interface.Exit();
                        break;
                }
            } while (!exit && !accountMenu);

            exit = false;
            Console.WriteLine($"Welcome {user.FirstName} {user.LastName}\n");

            do {
                Interface.resetPrompt();
                ConsoleKey[] validOptions = Interface.MainMenu();
                ConsoleKey selection = Console.ReadKey(true).Key;

                // Re-prompting up to the maximum number of prompt allowances
                while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && Interface.promptCount < Interface.LIMIT) {
                    Interface.increasePromptCount();;
                    Console.WriteLine($"Invalid option selected. Please try again (Attempt {Interface.promptCount})\n");
                    _ = Interface.MainMenu();
                    selection = Console.ReadKey(true).Key;
                }

                // Program exits if too many unsuccessful selections were attempted
                if (Interface.promptCount >= Interface.LIMIT) {
                    Interface.Exit_TooManyInvalidKeyPresses();
                }

                // Handle user's selection
                Console.Clear();
                switch (selection) {
                    case ConsoleKey.D1:
                        // User wants to make a deposit
                        Console.WriteLine("Deposit");
                        if (!user.makeDeposit()) {
                            Console.WriteLine("The amount was not deposited. Please try again.");
                        } else {
                            Console.WriteLine("Your transaction was successful.");
                        }
                        break;
                    case ConsoleKey.D2:
                        // User wants to make a withdrawal
                        Console.WriteLine("Make a Withdrawal");
                        if (!user.makeWithdrawal()) {
                            Console.WriteLine("The amount was not withdrawn.");
                        } else {
                            Console.WriteLine("Your transaction was successful.");
                        }
                        break;
                    case ConsoleKey.D3:
                        // User wants to check balance
                        Console.WriteLine("Check Balance");
                        Console.Write($"Your balance is ");
                        user.Checking.displayBalance();
                        break;
                    case ConsoleKey.D4:
                        // User wants to view transactions
                        Console.WriteLine("View transactions");
                        user.Checking.displayTransactions();
                        break;
                    default:
                        Console.WriteLine("Logout");
                        exit = true;
                        Interface.Exit();
                        break;
                }
            } while (!exit);
        }
    }
}
