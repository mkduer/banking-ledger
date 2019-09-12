using System;

namespace BankingLedger
{
    public class Account
    {
        private double balance { get; set; }
        public double Balance {
            get { return balance; }
            set { balance = value; }
        }

        public Account(double startingBalance = 0) 
        {
            balance = startingBalance;
        }

        public double getBalance() 
        {
            return balance;
        }
    }
}