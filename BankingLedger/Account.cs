using System;
using System.Collections.Generic;

namespace BankingLedger
{
    // The Account class handles the balance and ledger attributes 
    // while providing methods and their wrappers for modifying 
    // and displaying the attributes
    public class Account
    {
        private double _balance;

        public double Balance
        {
            get { return _balance; }
            set { _balance = value; }
        }

        private List<Transaction> _ledger;

        public List<Transaction> Ledger
        {
            get { return _ledger; }
        }

        public Account(double startingBalance = 0) 
        {
            this._balance = startingBalance;
            this._ledger = new List<Transaction>();
        }

        // wrapper: deposit the parameterized amount into the balance
        public bool deposit(double amount)
        {
            return _deposit(amount);
        }

        // wrapper: withdraw the parameterized amount from the balance
        public bool withdraw(double amount)
        {
            return _withdraw(amount);
        }

        // display the transactions
        public void displayTransactions()
        {
            int repeat = 52;
            String transactionBorder = new String('=', repeat);
            String totalBorder = new String('-', repeat);
            Console.WriteLine(transactionBorder);
            Ledger.ForEach(delegate(Transaction transaction)
            {
                if (this._convertUTCtoLocalSystem(transaction.Timestamp) != null) {
                    Console.WriteLine($"{this._convertUTCtoLocalSystem(transaction.Timestamp), -10} {transaction.Type, 15} {transaction.Amount, 15:C}");
                } else {
                    Console.WriteLine($"(UTC) {transaction.Timestamp, -10} {transaction.Type, 15} {transaction.Amount, 15:C}");
                }
            });
            Console.WriteLine(totalBorder);
            Console.WriteLine($"Current Balance: {Balance, 33:C}");
            Console.WriteLine(transactionBorder);
        }

        // deposit the parameterized amount into the balance
        // and record the transaction in the ledger
        private bool _deposit(double amount)
        {
            if (amount <= 0) {
                return false;
            }

            this._balance += amount;
            this._recordTransaction(amount, "deposit");
            return true;
        }

        // withdraw the parameterized amount from the balance
        // and record the transaction in the ledger
        // note: amount must be a positive value
        private bool _withdraw(double amount)
        {
            if (amount <= 0) {
                return false;
            }

            this._balance -= amount;
            this._recordTransaction(amount, "withdraw");
            return true;
        }

        // record the transaction in the ledger
        private bool _recordTransaction(double amount, string type)
        {
            DateTime timestamp = DateTime.UtcNow;

            if (amount <= 0) {
                return false;
            }

            if (type == "withdraw") {
                amount *= -1;
            }
            this._ledger.Add(new Transaction() { Timestamp = timestamp, Amount = amount, Type = type });
            return true;
        }

        // convert UTC time over to the user's local (system) time
        private DateTime? _convertUTCtoLocalSystem(DateTime timestamp)
        {
            if (timestamp == null) {
                return null;
            }
            // resource: https://stackoverflow.com/questions/12937968/converting-utc-datetime-to-local-datetime/12938028
            DateTime timestampKindSpecific = DateTime.SpecifyKind(timestamp, DateTimeKind.Utc);
            DateTime localTimestamp = timestampKindSpecific.ToLocalTime();
            return localTimestamp;
        }
    }
}