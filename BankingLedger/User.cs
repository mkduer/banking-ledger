using System;
using System.Security.Cryptography;
using System.Text;

namespace BankingLedger
{
    public class User
    {
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

        private Account _account;

        public User() {
            FirstName = "";
            LastName = "";
            UserID = "";
            _secureHash = null;
            _account = new Account();
        }

        // Create details for new user
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

                Console.WriteLine($"Is {id} the correct username? (y/n)");
                response = Console.ReadLine().ToUpper();
                confirmation = response[0];
            }

            return true;
        }

        // create user ID
        private bool _createUserID(string id)
        {
            if (id == "" || id == null) {
                return false;
            }
            UserID = id;
            return true;
        }

        // Prompt for first and last names
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

                Console.WriteLine($"Is {first} {last} correct? (y/n)");
                response = Console.ReadLine().ToUpper();
                confirmation = response[0];
            }
            return true;
        }

        // Create first and last names
        private bool _createRealName(string first, string last)
        {
            if (first == "" || first == null || last == "" || last == null) {
                return false;
            }
            FirstName = first;
            LastName = last;
            return true;
        }

        // Prompt for password
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

        // Create a temp password
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

            if (temp.Length < 8 || temp == "" || temp == null) {
                return false;
            }
            return true;
        }

        // Create hash and salt
        private bool _createHashSalt(ref string temp)
        {
            int iterations = 3000;
            byte[] randSalt = new byte[16];
            byte[] hashBytes = new byte[39];

            if (temp == "" || temp == null) {
                return false;
            }

            // Create hash and salt
            RNGCryptoServiceProvider randCSP = new RNGCryptoServiceProvider();
            randCSP.GetBytes(randSalt);
            var derived = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(temp), randSalt, iterations, HashAlgorithmName.SHA256);
            byte[] hash = derived.GetBytes(23);

            Array.Copy(randSalt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 23);
            _secureHash = Convert.ToBase64String(hashBytes);

            return true;
        }
    }
}