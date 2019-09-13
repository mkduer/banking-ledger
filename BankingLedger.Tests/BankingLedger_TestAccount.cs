using Xunit;

namespace BankingLedger.UnitTests
{
    public class BankingLedger_TestAccount
    {
        [Fact]
        public void DefaultNewAccount_BalanceZero()
        {
            Account account = new Account();
            Assert.Equal(0, account.Balance);
        }

        [Fact]
        public void BonusNewAccount_BalanceAboveZero()
        {
            Account account = new Account(500);
            Assert.Equal(500, account.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(746123484486.23498725)]
        [InlineData(999999999999.99999999)]
        public void TestValidDeposit_CorrectBalanceIncrease(double money)
        {
            Account account = new Account();
            account.deposit(money);
            Assert.Equal(money, account.Balance);
        }

        [Theory]
        [InlineData(-100.00)]
        public void TestInvalidNegativeDeposit_BalanceRemainsZero(double money)
        {
            Account account = new Account();
            account.deposit(money);
            Assert.Equal(0, account.Balance);
        }

    }
}
