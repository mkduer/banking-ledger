using System;
using System.Security.Cryptography;
using System.ComponentModel;
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

        public bool createUserAccount()
        {
            Console.Clear();
            Console.WriteLine("Let's create your account.");
            return _createUserID() && _createRealName() && _createPassword();
        }

        // create user ID
        private bool _createUserID()
        {
            char confirmation = 'N';
            string response = "";

            while (!confirmation.Equals('Y')) {
                Console.WriteLine("\nPlease create a username:");
                UserID = Console.ReadLine();

                Console.WriteLine($"Is {UserID} the correct username? (y/n)");
                response = Console.ReadLine().ToUpper();
                confirmation = response[0];
            }

            if (UserID == "" || UserID == null) {
                return false;
            }
            return true;
        }

        // Create first and last names
        private bool _createRealName()
        {
            char confirmation = 'N';
            string response = "";

            while (!confirmation.Equals('Y')) {
                Console.Clear();
                Console.WriteLine("\nEnter your first name:");
                FirstName = Console.ReadLine();
                Console.WriteLine("Enter your last name:");
                LastName = Console.ReadLine();

                Console.WriteLine($"Is {FirstName} {LastName} correct? (y/n)");
                response = Console.ReadLine().ToUpper();
                confirmation = response[0];
            }

            if (FirstName == "" || FirstName == null || LastName == "" || LastName == null) {
                return false;
            }
            return true;
        }

        // Create a password
        private bool _createPassword()
        {
            string temp = null;
            int prompt = 0;
            int maxPrompt = 5;
            int iterations = 3000;
            ConsoleKeyInfo key;

            // Ensure the password is set and within the maximum prompts allowed
            while (prompt < maxPrompt) {

                while (temp == null || temp.Length < 8) {

                    // Create a valid password allowing for a maximum of characters, symbols, numbers
                    // this could be modified to not include invalid ascii values
                    Console.Clear();
                    Console.WriteLine("\nEnter your password (minimum 8 characters):");
                    do {
                        key = Console.ReadKey(true);

                        if ((int) key.Key > 31 && (int) key.Key < 127) {
                            temp += key.KeyChar;
                            Console.Write("*");
                        }
                    } while (key.Key != ConsoleKey.Enter);
                }

                // Create hash and salt
                RNGCryptoServiceProvider randCSP = new RNGCryptoServiceProvider();
                byte[] randSalt = new byte[16];
                randCSP.GetBytes(randSalt);
                var derived = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(temp), randSalt, iterations, HashAlgorithmName.SHA256);
                byte[] hash = derived.GetBytes(23);

                byte[] hashBytes = new byte[39];
                Array.Copy(randSalt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 23);
                _secureHash = Convert.ToBase64String(hashBytes);
                return true;
            }
            return false;
        }
    }
}