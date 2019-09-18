using System;
using System.Text.RegularExpressions;

namespace BankingLedger
{
    // The Menu class prompts the user with menu options
    public static class CLInterface
    {
        public const int MAXPROMPT = 4;
        public static int promptCount { get; set; }

        // a welcome message
        public static void welcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("\nWelcome to the Most Amazing Bank!\n");
        }

        // a main menu that returns valid key stroke options
        public static Func<ConsoleKey[]> mainMenuOptions = () =>
        {
            Console.WriteLine("\nPlease select from the following options:");
            Console.WriteLine("(1) Login");
            Console.WriteLine("(2) Create Account");
            Console.WriteLine("(Esc) Exit Program");
            Console.WriteLine("\nExample: To create an account, you would type 2\n");

            return new ConsoleKey[] {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Escape};
        };

        // a user account menu that returns valid key stroke options
        public static Func<ConsoleKey[]> accountMenuOptions = () =>
        {
            Console.WriteLine("\nPlease select from the following options:");
            Console.WriteLine("(1) Deposit");
            Console.WriteLine("(2) Withdrawal");
            Console.WriteLine("(3) Check Balance");
            Console.WriteLine("(4) View Recent Transactions");
            Console.WriteLine("(5) Logout\n");

            return new ConsoleKey[] {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, 
                                     ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.Escape};
        };

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

        // exit program
        public static void exit()
        {
            Console.WriteLine("Exit Program");
            _exitProgram();
        }

        // invalid option and prompt count message
        public static void invalidSelectionAttemptCount()
        {
            Console.WriteLine($"Invalid option selected. Please try again (Attempt {promptCount})\n");
        }

        // exit program due to too many invalid key presses
        public static void exit_TooManyInvalidKeyPresses()
        {
            Console.WriteLine("Too many invalid selections were made. Goodbye!");
            _exitProgram();
        }

        public static void confirmUserCreation(ref User user)
        {
            Console.WriteLine($"Your account was created successfully\n");
            Console.WriteLine("ACCOUNT DETAILS:");
            Console.WriteLine($"Username: {user.UserID}");
            Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
        }

        // create details for new user
        public static bool createUser(ref UsersCollection users, ref User user)
        {
            string id = "";
            string firstName = "";
            string lastName = "";
            string pass = "";

            Console.Clear();
            Console.WriteLine("Let's create your account.");

            if (promptUserID(ref users, ref id) && promptRealName(ref firstName, ref lastName) && promptPassword(ref pass)) 
            {
                try 
                {
                    // create a new user with valid parameters
                    user = new User(id, firstName, lastName, pass);
                    users.add(user);
                }
                catch (ArgumentNullException) 
                {
                    Console.WriteLine("Valid credentials must be used to create an account.");
                    return false;
                } 
                catch (ArgumentException) 
                {
                    Console.WriteLine("This username is already taken. Please choose a different one.");
                    return false;
                }
                return true;
            } 
            return false;
        }

        // prompt for user ID
        public static bool promptUserID(ref UsersCollection users, ref string tempID)
        {
            char confirmation = 'N';
            string id = "";

            while (!confirmation.Equals('Y')) 
            {
                Console.WriteLine("\nPlease create a username (cannot contain: \', \", \\, space):");
                id = Console.ReadLine();

                try
                {
                    UserUtility.validateUserID(ref users, id, ref tempID);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid username. Please try again.");
                    return false;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("That username is already taken. Please choose another.");
                    return false;
                }

                confirmation = askUserConfirmation("\nIs " + id + " the correct username? (y/n)");
            }
            return true;
        }

        // receive a y/n confirmation from user
        // and handle other possible entries such as the Enter key
        public static char askUserConfirmation(string question)
        {
            char confirmation = 'N';

            Console.WriteLine(question);
            string response = Console.ReadLine().ToUpper();

            if (response != "") 
                confirmation = response[0];

            return confirmation;
        }

        // prompt for first and last names
        public static bool promptRealName(ref string tempFirst, ref string tempLast)
        {
            string first = "";
            string last = "";
            char confirmation = 'N';

            while (!confirmation.Equals('Y')) 
            {
                Console.WriteLine("\nEnter your first name:");
                first = Console.ReadLine();
                Console.WriteLine("Enter your last name:");
                last = Console.ReadLine();

                if (!UserUtility.validateRealName(first, last, ref tempFirst, ref tempLast)) 
                    return false;

                confirmation = askUserConfirmation("\nIs " + first + " " + last + " correct? (y/n)");
            }
            return true;
        }

        // prompt for password
        public static bool promptPassword(ref string tempPass)
        {
            int prompt = 0;
            bool success = false;

            // Ensure the password is set and within the maximum prompts allowed
            Console.Clear();
            while (prompt < MAXPROMPT && !success) 
            {
                prompt++;
                Console.WriteLine("\nEnter your password (Minimum 8 characters. Cannot contain: \', \", \\):");
                try 
                {
                    success = createPassword(ref tempPass);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid Password");
                }
            }

            if (!success)
                return false;

            return UserUtility.createHashSalt(ref tempPass);
        }

        public static bool createPassword(ref string temp)
        {
            return _createPassword(ref temp);
        }

        // welcome user
        public static void welcomeUser(ref User user)
        {
            Console.WriteLine($"Welcome {user.FirstName} {user.LastName}!");
        }

        // login user if valid
        public static bool login(ref UsersCollection users, ref User user)
        {
            string id = "";
            string pass = "";

            Console.WriteLine("User Login");

            if (users == null) 
            {
                Console.WriteLine("Access Denied.");
                return false;
            }

            Console.WriteLine("\nEnter your username:");
            id = Console.ReadLine();

            try 
            {
                UserUtility.verifyUser(ref users, ref id);
            } 
            catch (ArgumentNullException) 
            {
                Console.WriteLine("Invalid Credentials.");
                return false;
            } 
            catch (UnauthorizedAccessException) 
            {
                Console.WriteLine("Invalid Credentials.");
                return false;
            }

            Console.WriteLine("\nEnter your password:");
            try 
            {
                createPassword(ref pass);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("Invalid Credentials");
                return false;
            }
            Console.WriteLine();

            try 
            {
                UserUtility.verifyPassword(ref users, ref user, id, ref pass);
            } 
            catch (UnauthorizedAccessException) 
            {
                Console.WriteLine("Invalid Credentials.");
                user = null;
                return false;
            }
            Console.WriteLine($"Credentials Verified.\n");
            return true;
        }

        // user makes a deposit
        public static void makeDeposit(ref User user)
        {
            bool valid = false;
            int promptCount = 0;
            double amountFormatted;

            Console.WriteLine("Deposit:");

            do {
                promptCount++;
                Console.WriteLine("How much do you want to deposit?");
                Console.WriteLine("\nExample: 25.00\n");
                string amount = Console.ReadLine();
                amountFormatted = formatCurrency(amount);

                if (amountFormatted == (double) -1) 
                {
                    Console.WriteLine("Invalid value was entered.");
                } 
                else 
                {
                    valid = true;
                }
            } while (!valid && promptCount < MAXPROMPT);

            if (!user.Checking.deposit(amountFormatted)) 
            {
                Console.WriteLine("The amount was not deposited. Please try again.");
            } 
            else 
            {
                Console.WriteLine("Your transaction was successful.");
            }
        }

        // format string into currency
        public static double formatCurrency(string amountString)
        {
            double formatted = (double) -1;
            if (string.IsNullOrEmpty(amountString))
                return formatted;

            // Check that the parameter has a valid pattern
            string pattern = @"^$?\s*\d*.\d*\s*$";
            Regex regex = new Regex(pattern);

            // If the string is valid, convert to a double
            if (regex.IsMatch(amountString)) 
            {
                try 
                {
                    formatted = Convert.ToDouble(amountString);
                } 
                catch (FormatException) 
                {
                    Console.WriteLine($"Unable to convert '{amountString}' to a valid transaction value.");
                } 
                catch (OverflowException) 
                {
                    Console.WriteLine($"{amountString} is outside a double type range");
                }
            }
            return formatted;
        }

        // user makes a withdrawal
        public static void makeWithdrawal(ref User user)
        {
            bool valid = false;
            int promptCount = 0;
            double amountFormatted;

            Console.WriteLine("Make a Withdrawal:");

            do 
            {
                promptCount++;
                Console.WriteLine("How much do you want to withdraw?");
                Console.WriteLine("\nExample: 25.00\n");
                string amount = Console.ReadLine();
                amountFormatted = formatCurrency(amount);

                if (amountFormatted == (double) -1) 
                {
                    Console.WriteLine("Invalid value was entered.");
                } 
                else 
                {
                    valid = true;
                }
            } while (!valid && promptCount < MAXPROMPT);

            char continueTransaction = checkOverdrawn(ref user, amountFormatted);

            if (!continueTransaction.Equals('Y') && user.Checking.withdraw(amountFormatted)) 
            {
                Console.WriteLine("Your transaction was successful.");
                return;
            } 
            Console.WriteLine("\nTransaction Cancelled");
        }

        // check if user's account will be overdrawn
        public static char checkOverdrawn(ref User user, double amount)
        {
            char confirmation = 'N';

            if (user.Checking.Balance - amount < 0) 
            {
                Console.WriteLine($"\nWithdrawing {amount:C} will overdraw your account.");
                confirmation = askUserConfirmation("\nWould you like to stop this transaction? (y/n)");
            }
            return confirmation;
        }

        // check user's balance
        public static void checkBalance(ref User user)
        {
            Console.WriteLine("Check Balance:");
            Console.WriteLine($"Your balance is {user.Checking.Balance:C}");
        }

        // change user's login status
        public static void logout(ref bool logout)
        {
            Console.WriteLine("Logout");
            logout = true;
        }

        // view user's transactions
        public static void viewTransactions(ref User user)
        {
            int repeat = 52;
            String transactionBorder = new String('=', repeat);
            String totalBorder = new String('-', repeat);

            Console.WriteLine("View Transactions:");

            Console.WriteLine(transactionBorder);
            user.Checking.Ledger.ForEach(delegate(Transaction transaction)
            {
                var time = UserUtility.convertTime(transaction.Timestamp);
                string action = "";

                if (transaction.Type == TransactionType.Deposit)
                {
                    action = "deposit";
                }
                else
                {
                    action = "withdraw";
                }

                if (time != null) 
                {
                    Console.WriteLine($"{time, -10} {action, 15} {transaction.Amount, 15:C}");
                } 
                else 
                {
                    Console.WriteLine($"(UTC) {transaction.Timestamp, -10} {action, 15} {transaction.Amount, 15:C}");
                }
            });
            Console.WriteLine(totalBorder);
            Console.WriteLine($"Current Balance: {user.Checking.Balance, 33:C}");
            Console.WriteLine(transactionBorder);
        }

        public static void contactSupport(string attemptedAction)
        {
            Console.WriteLine($"\n{attemptedAction} Failed.");
            Console.WriteLine($"You may try {attemptedAction.ToLower()} again, or if you continue to have difficulties");
            Console.WriteLine("Please contact support at {contact point} for further help.");
        }

        // create a temp password and check that it is valid
        private static bool _createPassword(ref string temp)
        {
            ConsoleKeyInfo key;
            temp = "";

            // Create a valid password allowing for a maximum of characters, symbols, numbers
            // this could be modified to not include invalid ascii values
            do 
            {
                key = Console.ReadKey(true);

                // if a valid key is entered
                if (((int) key.Key > 31 && (int) key.Key < 127))
                {
                    temp += key.KeyChar;
                    Console.Write("*");
                } 

                // if the user deletes
                if ((key.Key == ConsoleKey.Backspace || key.Key == ConsoleKey.Delete) && temp.Length > 0) 
                {
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    temp = temp.Substring(0, temp.Length - 1);
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            string pattern = @"[\\'""]+";
            Regex regex = new Regex(pattern);

            if (temp.Length < 8 || string.IsNullOrEmpty(temp) || regex.IsMatch(temp)) 
            {
                temp = "";
                throw new ArgumentException();
            }
            return true;
        }

        // exit program
        private static void _exitProgram()
        {
            Environment.Exit(1);
        }
    }
}