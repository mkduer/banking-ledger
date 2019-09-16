using System;
using System.Text.RegularExpressions;

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

            if (_checking.Balance - amountFormatted < 0) {
                Console.WriteLine($"\nWithdrawing {amountFormatted:C} will overdraw your account.");
                Console.WriteLine("Would you like to continue with the withdrawal? (y/n)");
                string response = Console.ReadLine().ToUpper();
                char confirmation = response[0];

                if (!confirmation.Equals('Y')) {
                    Console.WriteLine("\nTransaction Cancelled");
                    return false;
                }
            }

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
    }
}