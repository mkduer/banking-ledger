using Xunit;

namespace BankingLedger.UnitTests
{
    public class BankingLedger_TestTransaction
    {
        [Theory]
        [InlineData(1234.22, "deposit")]
        [InlineData(-924.00, "withdrawal")]
        public void CreateNewTransaction_CorrectValues(double amount, string type)
        {
            Transaction activity = new Transaction(amount, type);
            Assert.Equal(amount, activity.Amount);
            Assert.Equal(type, activity.Type);
        }
    }
}
