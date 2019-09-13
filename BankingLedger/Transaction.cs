using System;

namespace BankingLedger
{
    // The Transaction class represents a single transaction
    // consisting of an amount and type of transaction
    public class Transaction
    {
        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private double _amount;

        public double Amount
        { 
            get { return _amount; }
            set { _amount = value; }
        }
        private string _type;

        public string Type
        { 
            get { return _type; }
            set { _type = value; }
        }
    }
}