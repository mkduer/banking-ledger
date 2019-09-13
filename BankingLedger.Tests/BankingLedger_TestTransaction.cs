using Xunit;
using System;

namespace BankingLedger.UnitTests
{
    public class BankingLedger_TestTransaction
    {
        [Theory]
        [InlineData(1234.22, "deposit")]
        [InlineData(-924.00, "withdrawal")]
        public void CreateNewTransaction_CorrectValues(double amount, string type)
        {
            DateTime timestamp = DateTime.UtcNow;
            Transaction activity = new Transaction() { Timestamp = timestamp, Amount = amount, Type = type };
            Assert.Equal(timestamp, activity.Timestamp);
            Assert.Equal(amount, activity.Amount);
            Assert.Equal(type, activity.Type);
        }
    }
}
