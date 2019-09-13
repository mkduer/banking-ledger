namespace BankingLedger
{
    // The Transaction class represents a single transaction
    // consisting of an amount and type of transaction
    public class Transaction
    {
        private double _amount;
        private string _type;

        public double Amount
        { 
            get { return _amount; }
            set { _amount = value; }
        }

        public string Type
        { 
            get { return _type; }
            set { _type = value; }
        }
    }
}