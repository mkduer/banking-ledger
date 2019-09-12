namespace BankingLedger
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        private string _password;

        private Account _account;

        public User() {
            FirstName = "";
            LastName = "";
            Username = "";
            _password = "";
            _account = new Account();
        }
    }
}