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

        // display the resulting balance
        public void displayBalance() 
        {
            Console.WriteLine($"{this.Balance:C}");
        }

        // display the transactions
        public void displayTransactions()
        {
            Console.WriteLine("===========================");
            Ledger.ForEach(delegate(Transaction transaction)
            {
                Console.WriteLine($"{transaction.Type, -10} {transaction.Amount, 15:C}");
            });
            Console.WriteLine("---------------------------");
            Console.Write("Current Balance: ");
            this.displayBalance();
            Console.WriteLine("===========================");
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
            if (amount <= 0) {
                return false;
            }

            if (type == "withdraw") {
                amount *= -1;
            }
            this._ledger.Add(new Transaction() { Amount = amount, Type = type });
            return true;
        }
    }
}