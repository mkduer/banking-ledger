using System;

namespace BankingLedger
{
    // The Menu class prompts the user with menu options
    public static class Interface
    {
        public static int promptCount { get; set; }
        public const int LIMIT = 4;

        // A welcome message
        public static void WelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("\nWelcome to the Most Amazing Bank!\n");
        }

        // A welcome menu that returns valid key stroke options
        public static ConsoleKey[] WelcomeMenu()
        {
            Console.WriteLine("\nPlease select from the following options:");
            Console.WriteLine("(1) Login");
            Console.WriteLine("(2) Create Account");
            Console.WriteLine("(Esc) Exit Program");
            Console.WriteLine("\nExample: To create an account, you would type 2\n");

            return new ConsoleKey[] {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.Escape};
        }

        // A user account menu that returns valid key stroke options
        public static ConsoleKey[] MainMenu()
        {
            Console.WriteLine("\nPlease select from the following options:");
            Console.WriteLine("(1) Deposit");
            Console.WriteLine("(2) Withdrawal");
            Console.WriteLine("(3) Check Balance");
            Console.WriteLine("(4) View Recent Transactions");
            Console.WriteLine("(5) Logout\n");

            return new ConsoleKey[] {ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, 
                                     ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.Escape};
        }

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

        // wrapper: exit program
        public static void Exit()
        {
            _exitProgram();
        }

        // exit program due to too many invalid key presses
        public static void Exit_TooManyInvalidKeyPresses()
        {
            Console.WriteLine("Too many invalid selections were made. Goodbye!");
            _exitProgram();
        }

        // exit program
        private static void _exitProgram()
        {
            Environment.Exit(1);
        }

        // create details for new user
        public static bool createUser(ref User user)
        {
            string id = "";
            string firstName = "";
            string lastName = "";
            string pass = "";

            Console.Clear();
            Console.WriteLine("Let's create your account.");

            if (!promptUserID(ref id)) {
                return false;
            }
            
            if (!promptRealName(ref firstName, ref lastName)) {
                return false;
            }
            
            if (!promptPassword(ref pass)) {
                return false;
            }

            // create a new user if all fields were created and validated
            user = new User(id, firstName, lastName, pass);

            return true;
        }

        // prompt for user ID
        public static bool promptUserID(ref string tempUser)
        {
            char confirmation = 'N';
            string response = "";
            string id = "";

            while (!confirmation.Equals('Y')) {
                Console.WriteLine("\nPlease create a username:");
                id = Console.ReadLine();

                if (!UserUtility.validateUserID(id, ref tempUser)) {
                    return false;
                }

                Console.WriteLine($"\nIs {id} the correct username? (y/n)");
                response = Console.ReadLine().ToUpper();
                confirmation = response[0];
            }
            return true;
        }

        // prompt for first and last names
        public static bool promptRealName(ref string tempFirst, ref string tempLast)
        {
            string first = "";
            string last = "";
            char confirmation = 'N';
            string response = "";

            while (!confirmation.Equals('Y')) {
                // Console.Clear();  //TODO uncomment
                Console.WriteLine("\nEnter your first name:");
                first = Console.ReadLine();
                Console.WriteLine("Enter your last name:");
                last = Console.ReadLine();

                if (!UserUtility.validateRealName(first, last, ref tempFirst, ref tempLast)) {
                    return false;
                }

                Console.WriteLine($"\nIs {first} {last} correct? (y/n)");
                response = Console.ReadLine().ToUpper();
                confirmation = response[0];
            }
            return true;
        }

        // prompt for password
        public static bool promptPassword(ref string tempPass)
        {
            int prompt = 0;
            int maxPrompt = 5;

            // Ensure the password is set and within the maximum prompts allowed
            Console.Clear();
            Console.WriteLine("\nEnter your password (minimum 8 characters):");

            while (prompt < maxPrompt && !UserUtility.createPassword(ref tempPass)) {
                prompt++;
                Console.WriteLine("\nEnter your password (minimum 8 characters):");
            }
            return UserUtility.createHashSalt(ref tempPass);
        }

        // login user if valid
        public static bool login(ref User user)
        {
            string id = "";
            string temp = "";
            ConsoleKeyInfo key;

            if (user == null) {
                Console.WriteLine("User does not exist.");
                return false;
            }

            Console.WriteLine("\nEnter your username:");
            id = Console.ReadLine();

            if (!UserUtility.verifyUser(ref user, ref id)) {
                Console.WriteLine("The entered username does not exist");
                return false;
            }

            Console.WriteLine("\nEnter your password:");
            do {
                key = Console.ReadKey(true);

                if ((int) key.Key > 31 && (int) key.Key < 127) {
                    temp += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            if (!UserUtility.verifyPassword(ref user, ref temp)) {
                Console.WriteLine("Invalid Password");
                return false;
            }
            return true;
        }
    }
}