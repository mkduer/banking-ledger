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

        // display the resulting balance
        public void viewBalance() 
        {
            Console.WriteLine($"BALANCE: ${Balance}");  // TODO: check that this value updates correctly 
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

        // record the transaction in the ledger
        private bool _recordTransaction(double amount, string type)
        {
            if (amount == 0 || (type == "deposit" && amount < 0) || (type == "withdrawal" && amount > 0)) {
                return false;
            }

            this._ledger.Add(new Transaction() { Amount = amount, Type = type });
            return true;
        }
    }
}