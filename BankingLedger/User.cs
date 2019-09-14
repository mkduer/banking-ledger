using System;

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

        private string _password;

        public string Password
        {
            set { _password = value; }
        }

        private Account _account;

        public User() {
            FirstName = "";
            LastName = "";
            UserID = "";
            _password = "";
            _account = new Account();
        }

        public bool createUserAccount()
        {
            Console.Clear();
            Console.WriteLine("Let's create your account.");
            return _createUserID() && _createRealName();
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
    }
}