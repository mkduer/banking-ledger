using System;

namespace BankingLedger
{
    // The Program class is the program that must be run to start the banking ledger program
    class Program
    {
        // provides the main structure of the program from start to exit
        static void Main(string[] args)
        {
            bool exit = false;
            UsersCollection users = new UsersCollection();

            // Welcome user
            CLInterface.welcomeMessage();

            // Provide welcome menu options and 
            // check for valid selection until program is exited 
            do
            {
                ConsoleKey selection = menuPrompt(CLInterface.mainMenuOptions);
                exit = mainMenu(ref users, selection);
            } while (!exit);
        }

        // provides a menu prompt by taking a parameterized function
        // that displays the menu and returns the user's selection from the menu
        private static ConsoleKey menuPrompt(Func<ConsoleKey[]> menuOptions)
        {
            ConsoleKey[] validOptions = null;
            CLInterface.resetPrompt();
            validOptions = menuOptions();
            ConsoleKey selection = Console.ReadKey(true).Key;

            // Re-prompting up to the maximum number of prompt allowances
            while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && CLInterface.promptCount < CLInterface.MAXPROMPT) 
            {
                CLInterface.increasePromptCount();;
                CLInterface.invalidSelectionAttemptCount();
                validOptions = menuOptions();
                selection = Console.ReadKey(true).Key;
            }

            // Program exits if too many unsuccessful selections were attempted
            if (CLInterface.promptCount >= CLInterface.MAXPROMPT) 
                CLInterface.exit_TooManyInvalidKeyPresses();

            return selection;
        }

        // create main menu
        private static bool mainMenu(ref UsersCollection users, ConsoleKey selection)
        {
            bool exit = false;
            bool logout = false;
            User createUser = null;
            User loginUser = null;

            // Handle user's selection
            Console.Clear();
            switch (selection) {
                case ConsoleKey.D1:
                    if (CLInterface.login(ref users, ref loginUser)) 
                    {
                        CLInterface.welcomeUser(ref loginUser);
                        do 
                        {
                            ConsoleKey option = menuPrompt(CLInterface.accountMenuOptions);
                            logout = accountMenu(option, ref loginUser);
                        } while (!logout);

                        logout = false;
                    } 
                    else 
                    {
                        CLInterface.contactSupport("Login");
                    }
                    break;
                case ConsoleKey.D2:
                    if (CLInterface.createUser(ref users, ref createUser)) 
                    {
                        CLInterface.confirmUserCreation(ref createUser);
                    } 
                    else 
                    {
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
