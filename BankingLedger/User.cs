using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BankingLedger
{
    public class User
    {
        private const int _BYTESIZE = 16;
        private const int _MAXBYTESIZE = 23;
        private const int _ITERATIONS = 3000;
        private string _userID;

        public string UserID
        { 
            get { return _userID; }
            set { _userID = value; } 
        }

        private string _firstname;

        public string FirstName
        { 
            get { return _firstname; }
            set { _firstname = value; } 
        }

        private string _lastname;

        public string LastName
        { 
            get { return _lastname; }
            set { _lastname = value; } 
        }

        private string _secureHash;

        private Account _checking;

        public User() {
            FirstName = "";
            LastName = "";
            UserID = "";
            _secureHash = null;
            _checking = new Account();
        }

        public void checkBalance()
        {
            Console.Write($"Your balance is ");
            _checking.displayBalance();
        }

        // user makes a withdrawal
        public bool makeWithdrawal()
        {
            bool valid = false;
            int promptCount = 0;
            int maxPrompt = 5;
            double amountFormatted;

            do {
                promptCount++;
                Console.WriteLine("How much do you want to withdraw?");
                Console.WriteLine("\nExample: 25.00\n");
                string amount = Console.ReadLine();
                amountFormatted = formatCurrency(amount);

                if (amountFormatted == (double) -1) {
                    Console.WriteLine("Invalid value was entered.");
                } else {
                    valid = true;
                }
            } while (!valid && promptCount <= maxPrompt);

            return _checking.withdraw(amountFormatted);
        }

        // user makes a deposit
        public bool makeDeposit()
        {
            bool valid = false;
            int promptCount = 0;
            int maxPrompt = 5;
            double amountFormatted;

            do {
                promptCount++;
                Console.WriteLine("How much do you want to deposit?");
                Console.WriteLine("\nExample: 25.00\n");
                string amount = Console.ReadLine();
                amountFormatted = formatCurrency(amount);

                if (amountFormatted == (double) -1) {
                    Console.WriteLine("Invalid value was entered.");
                } else {
                    valid = true;
                }
            } while (!valid && promptCount <= maxPrompt);

            return _checking.deposit(amountFormatted);
        }

        // format string into currency
        public double formatCurrency(string amountString)
        {
            double formatted = (double) -1;
            if (string.IsNullOrEmpty(amountString))
            {
                return formatted;
            }

            // Check that the parameter has a valid pattern
            string pattern = @"^$?\s*\d*.\d*\s*$";
            Regex regex = new Regex(pattern);

            // If the string is valid, convert to a double
            if (regex.IsMatch(amountString)) {
                try {
                    formatted = Convert.ToDouble(amountString);
                } catch (FormatException) {
                    Console.WriteLine($"Unable to convert '{amountString}' to a valid transaction value.");
                } catch (OverflowException) {
                    Console.WriteLine($"{amountString} is outside a double type range");
                }
            }
            return formatted;
        }

        // login user if valid
        public bool login()
        {
            string id = "";
            string temp = "";
            ConsoleKeyInfo key;

            Console.WriteLine("\nEnter your username:");
            id = Console.ReadLine();

            if (!this._verifyUser(ref id)) {
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

            if (!this._verifyPassword(ref temp)) {
                Console.WriteLine("Invalid Password");
                return false;
            }
            return true;
        }

        // create details for new user
        public bool createUser()
        {
            Console.Clear();
            Console.WriteLine("Let's create your account.");
            return promptUserID() && promptRealName() && promptPassword();
        }

        // prompt for user ID
        public bool promptUserID()
        {
            char confirmation = 'N';
            string response = "";
            string id = "";

            while (!confirmation.Equals('Y')) {
                Console.WriteLine("\nPlease create a username:");
                id = Console.ReadLine();

                if (!this._createUserID(id)) {
                    return false;
                }

                Console.WriteLine($"\nIs {id} the correct username? (y/n)");
                response = Console.ReadLine().ToUpper();
                confirmation = response[0];
            }

            return true;
        }

        // prompt for first and last names
        public bool promptRealName()
        {
            char confirmation = 'N';
            string response = "";
            string first = "";
            string last = "";

            while (!confirmation.Equals('Y')) {
                Console.Clear();
                Console.WriteLine("\nEnter your first name:");
                first = Console.ReadLine();
                Console.WriteLine("Enter your last name:");
                last = Console.ReadLine();

                if (!_createRealName(first, last)) {
                    return false;
                }

                Console.WriteLine($"\nIs {first} {last} correct? (y/n)");
                response = Console.ReadLine().ToUpper();
                confirmation = response[0];
            }
            return true;
        }

        // prompt for password
        public bool promptPassword()
        {
            string temp = "";
            int prompt = 0;
            int maxPrompt = 5;

            // Ensure the password is set and within the maximum prompts allowed
            Console.Clear();
            Console.WriteLine("\nEnter your password (minimum 8 characters):");

            while (prompt < maxPrompt && !this._createPassword(ref temp)) {
                prompt++;
                Console.WriteLine("\nEnter your password (minimum 8 characters):");
            }
            return this._createHashSalt(ref temp);
        }

        // verify that username matches
        private bool _verifyUser(ref string id)
        {
            return id == this.UserID;
        }

        // verify password
        private bool _verifyPassword(ref string temp)
        {
            // modified from source: https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
            // this version incorporates SHA256 explicitly, while many versions online use SHA1 behind the PBKF2 function
            if (string.IsNullOrEmpty(temp)) {
                return false;
            }

            byte[] hashBytes = Convert.FromBase64String(_secureHash);
            byte[] salt = new byte[_BYTESIZE];
            Array.Copy(hashBytes, 0, salt, 0, _BYTESIZE);

            // Hash provided password
            var derived = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(temp), salt, _ITERATIONS, HashAlgorithmName.SHA256);
            byte[] hash = derived.GetBytes(_MAXBYTESIZE);

            for (int i = 0; i < _BYTESIZE; i++) {
                if (hashBytes[i + _BYTESIZE] != hash[i]) {
                    return false;
                }
            }
            return true;
        }

        // create user ID
        private bool _createUserID(string id)
        {
            if (string.IsNullOrEmpty(id)) {
                return false;
            }
            this.UserID = id;
            return true;
        }


        // create first and last names
        private bool _createRealName(string first, string last)
        {
            if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(last)) {
                return false;
            }

            this.FirstName = first;
            this.LastName = last;

            return true;
        }

        // create a temp password
        private bool _createPassword(ref string temp)
        {
            ConsoleKeyInfo key;

            // Create a valid password allowing for a maximum of characters, symbols, numbers
            // this could be modified to not include invalid ascii values
            do {
                key = Console.ReadKey(true);

                if ((int) key.Key > 31 && (int) key.Key < 127) {
                    temp += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            if (temp.Length < 8 || string.IsNullOrEmpty(temp)) {
                return false;
            }
            return true;
        }

        // create hash and salt
        private bool _createHashSalt(ref string temp)
        {
            // modified from source: https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
            // this version incorporates SHA256 explicitly, while many versions online use SHA1 behind the PBKF2 function
            byte[] randSalt = new byte[_BYTESIZE];
            byte[] hashBytes = new byte[_BYTESIZE + _MAXBYTESIZE];

            if (string.IsNullOrEmpty(temp)) {
                return false;
            }

            // Create hash and salt
            RNGCryptoServiceProvider randCSP = new RNGCryptoServiceProvider();
            randCSP.GetBytes(randSalt);
            var derived = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(temp), randSalt, _ITERATIONS, HashAlgorithmName.SHA256);
            byte[] hash = derived.GetBytes(_MAXBYTESIZE);

            Array.Copy(randSalt, 0, hashBytes, 0, _BYTESIZE);
            Array.Copy(hash, 0, hashBytes, _BYTESIZE, _MAXBYTESIZE);
            this._secureHash = Convert.ToBase64String(hashBytes);

            return true;
        }
    }
}