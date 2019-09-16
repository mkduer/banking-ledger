namespace BankingLedger
{
    // The User class represents a single user
    // consisting of credentials required for a user to exist
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

        public string Hash 
        { 
            get { return _secureHash; }
            set { _secureHash = value; } 
        }

        private Account _checking;

        public Account Checking
        {
            get { return _checking; }
        }

        public User(string id, string firstName, string lastName, string hash) {
            this.UserID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            _secureHash = hash;
            _checking = new Account();
        }
    }
}