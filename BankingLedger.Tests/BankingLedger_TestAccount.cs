using System;
using BankingLedger;
using Xunit;

namespace BankingLedger.UnitTests
{
    public class BankingLedger_TestAccount
    {
        [Fact]
        public void DefaultNewAccount_BalanceZero()
        {
            Account newAccount = new Account();
            Assert.Equal(0, newAccount.Balance);
        }

        [Fact]
        public void BonusNewAccount_BalanceAboveZero()
        {
            Account newAccount = new Account(500);
            Assert.Equal(500, newAccount.Balance);
        }
    }
}
