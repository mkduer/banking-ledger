using System;

namespace BankingLedger
{
    public class Account
    {
        private double balance;
        public double Balance {
            get { return balance; }
            set { balance = value; }
        }

        public Account(double startingBalance = 0) 
        {
            balance = startingBalance;
        }


    }
}