using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            User user = null;

            // Welcome user
            CLInterface.welcomeMessage();

            // Provide welcome menu options and 
            // check for valid selection until program is exited 
            do {

                ConsoleKey selection = mainMenuPrompt();
                exit = mainMenu(ref user, selection);

            } while (!exit);

        }

        // provides main menu prompt
        private static ConsoleKey mainMenuPrompt()
        {
            ConsoleKey[] validOptions = null;
            CLInterface.resetPrompt();
            CLInterface.mainMenuOptions(ref validOptions);
            ConsoleKey selection = Console.ReadKey(true).Key;

            // Re-prompting up to the maximum number of prompt allowances
            while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && CLInterface.promptCount < CLInterface.MAXPROMPT) {
                CLInterface.increasePromptCount();;
                Console.WriteLine($"Invalid option selected. Please try again (Attempt {CLInterface.promptCount})\n");
                CLInterface.mainMenuOptions(ref validOptions);
                selection = Console.ReadKey(true).Key;
            }

            // Program exits if too many unsuccessful selections were attempted
            if (CLInterface.promptCount >= CLInterface.MAXPROMPT) {
                CLInterface.Exit_TooManyInvalidKeyPresses();
            }

            return selection;
        }

        // create welcome account menu
        private static bool mainMenu(ref User user, ConsoleKey selection)
        {
            bool exit = false;

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

                        // Welcome validated user
                        Console.WriteLine($"Welcome {user.FirstName} {user.LastName}!");

                        accountMenuPrompt(ref user);
                    }
                    break;
                case ConsoleKey.D2:
                    // User wants to create account
                    Console.WriteLine("Create Account");
                    if (!CLInterface.createUser(ref user)) {
                        Console.WriteLine("\nAccount Creation unsuccessful.");
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
            return exit;
        }

        // provides user account menu prompt
        private static void accountMenuPrompt(ref User user)
        {
            bool logout = false;

            // Provide user banking menu options 
            // check for valid selection until user logs out
            do {
                ConsoleKey[] validOptions = null;

                CLInterface.resetPrompt();
                CLInterface.accountMenuOptions(ref validOptions);
                ConsoleKey selection = Console.ReadKey(true).Key;

                // Re-prompting up to the maximum number of prompt allowances
                while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && CLInterface.promptCount < CLInterface.MAXPROMPT) {
                    CLInterface.increasePromptCount();;
                    Console.WriteLine($"Invalid option selected. Please try again (Attempt {CLInterface.promptCount})\n");
                    CLInterface.accountMenuOptions(ref validOptions);
                    selection = Console.ReadKey(true).Key;
                }

                // Program exits if too many unsuccessful selections were attempted
                if (CLInterface.promptCount >= CLInterface.MAXPROMPT) {
                    CLInterface.Exit_TooManyInvalidKeyPresses();
                }

                logout = accountMenu(selection, ref user);

            } while (!logout);
        }

        // create user account menu
        private static bool accountMenu(ConsoleKey selection, ref User user)
        {
            bool logout = false;

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
            return logout;
        }
    }
}
