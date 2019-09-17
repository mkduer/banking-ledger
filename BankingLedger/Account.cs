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

        // deposit the parameterized amount into the balance
        public bool deposit(double amount)
        {
            return _deposit(amount);
        }

        // withdraw the parameterized amount from the balance
        public bool withdraw(double amount)
        {
            return _withdraw(amount);
        }

        // deposit the parameterized amount into the balance
        // and record the transaction in the ledger
        private bool _deposit(double amount)
        {
            if (amount <= 0) {
                return false;
            }

            this._balance += amount;
            this._recordTransaction(amount, TransactionType.Deposit);
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
            this._recordTransaction(amount, TransactionType.Withdraw);
            return true;
        }

        // record the transaction in the ledger
        private bool _recordTransaction(double amount, TransactionType type)
        {
            DateTime timestamp = DateTime.UtcNow;

            if (amount <= 0) {
                return false;
            }

            if (type == TransactionType.Withdraw) {
                amount *= -1;
            }
            this._ledger.Add(new Transaction() { Timestamp = timestamp, Amount = amount, Type = type });
            return true;
        }
    }
}