using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            bool logout = false;
            bool accountMenu = false;
            User user = null;

            // Welcome user
            CLInterface.welcomeMessage();

            // Provide welcome menu options and check for valid selection, 
            // until program is exited or next menu is entered
            do {
                CLInterface.resetPrompt();
                ConsoleKey[] validOptions = CLInterface.welcomeMenu();
                ConsoleKey selection = Console.ReadKey(true).Key;

                // Re-prompting up to the maximum number of prompt allowances
                while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && CLInterface.promptCount < CLInterface.LIMIT) {
                    CLInterface.increasePromptCount();;
                    Console.WriteLine($"Invalid option selected. Please try again (Attempt {CLInterface.promptCount})\n");
                    _ = CLInterface.welcomeMenu();
                    selection = Console.ReadKey(true).Key;
                }

                // Program exits if too many unsuccessful selections were attempted
                if (CLInterface.promptCount >= CLInterface.LIMIT) {
                    CLInterface.Exit_TooManyInvalidKeyPresses();
                }

                // Handle user's selection
                Console.Clear();
                switch (selection) {
                    case ConsoleKey.D1:
                        // User wants to login
                        Console.WriteLine("User Login");
                        if (!CLInterface.login(ref user)) {
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
                        if (!CLInterface.createUser(ref user)) {
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
                        // User wants to exit the program
                        Console.WriteLine("Exit Program");
                        exit = true;
                        CLInterface.Exit();
                        break;
                }
            } while (!exit && !accountMenu);

            // Welcome validated user
            Console.WriteLine($"Welcome {user.FirstName} {user.LastName}!");
            exit = false;

            // Provide user banking menu options 
            // check for valid selection until program is exited
            do {
                CLInterface.resetPrompt();
                ConsoleKey[] validOptions = CLInterface.mainMenu();
                ConsoleKey selection = Console.ReadKey(true).Key;

                // Re-prompting up to the maximum number of prompt allowances
                while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && CLInterface.promptCount < CLInterface.LIMIT) {
                    CLInterface.increasePromptCount();;
                    Console.WriteLine($"Invalid option selected. Please try again (Attempt {CLInterface.promptCount})\n");
                    _ = CLInterface.mainMenu();
                    selection = Console.ReadKey(true).Key;
                }

                // Program exits if too many unsuccessful selections were attempted
                if (CLInterface.promptCount >= CLInterface.LIMIT) {
                    CLInterface.Exit_TooManyInvalidKeyPresses();
                }

                // Handle user's selection
                Console.Clear();
                switch (selection) {
                    case ConsoleKey.D1:
                        // Make a deposit
                        Console.WriteLine("Deposit:");
                        if (!CLInterface.makeDeposit(ref user)) {
                            Console.WriteLine("The amount was not deposited. Please try again.");
                        } else {
                            Console.WriteLine("Your transaction was successful.");
                        }
                        break;
                    case ConsoleKey.D2:
                        // Make a withdrawal
                        Console.WriteLine("Make a Withdrawal:");
                        if (!CLInterface.makeWithdrawal(ref user)) {
                            Console.WriteLine("The amount was not withdrawn.");
                        } else {
                            Console.WriteLine("Your transaction was successful.");
                        }
                        break;
                    case ConsoleKey.D3:
                        // Check balance
                        Console.WriteLine("Check Balance:");
                        CLInterface.checkBalance(ref user);
                        break;
                    case ConsoleKey.D4:
                        // View transactions
                        Console.WriteLine("View Transactions:");
                        CLInterface.viewTransactions(ref user);
                        break;
                    default:
                        // Logout
                        Console.WriteLine("Logout");
                        logout = true;
                        break;
                }
            } while (!logout);
        }
    }
}
