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
                CLInterface.invalidSelectionAttemptCount();
                CLInterface.mainMenuOptions(ref validOptions);
                selection = Console.ReadKey(true).Key;
            }

            // Program exits if too many unsuccessful selections were attempted
            if (CLInterface.promptCount >= CLInterface.MAXPROMPT) {
                CLInterface.exit_TooManyInvalidKeyPresses();
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
                    if (CLInterface.login(ref user)) {
                        CLInterface.welcomeUser(ref user);
                        accountMenuPrompt(ref user);
                    } else {
                        CLInterface.contactSupport("Login");
                    }
                    break;
                case ConsoleKey.D2:
                    if (CLInterface.createUser(ref user)) {
                        CLInterface.confirmUserCreation(ref user);
                    } else {
                        CLInterface.contactSupport("Account Creation");
                    }
                    break;
                default:
                    exit = true;
                    CLInterface.exit();
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
                    CLInterface.invalidSelectionAttemptCount();
                    CLInterface.accountMenuOptions(ref validOptions);
                    selection = Console.ReadKey(true).Key;
                }

                // Program exits if too many unsuccessful selections were attempted
                if (CLInterface.promptCount >= CLInterface.MAXPROMPT) {
                    CLInterface.exit_TooManyInvalidKeyPresses();
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
                    CLInterface.makeDeposit(ref user);
                    break;
                case ConsoleKey.D2:
                    CLInterface.makeWithdrawal(ref user);
                    break;
                case ConsoleKey.D3:
                    CLInterface.checkBalance(ref user);
                    break;
                case ConsoleKey.D4:
                    CLInterface.viewTransactions(ref user);
                    break;
                default:
                    CLInterface.logout(ref logout);
                    break;
            }
            return logout;
        }
    }
}
