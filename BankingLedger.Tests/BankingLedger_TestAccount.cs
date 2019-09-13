using System;
using Xunit;
using System.Collections.Generic;

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
        [InlineData(145)]
        public void TestViewBalance_CorrectBalanceValue(double amount)
        {
            Account account = new Account();
            Assert.Equal(0, account.Balance);

            account.deposit(amount);
            Assert.Equal(amount, account.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(746123484486.23498725)]
        [InlineData(999999999999.99999999)]
        public void TestValidDeposit_CorrectBalanceIncrease(double amount)
        {
            Account account = new Account();
            account.deposit(amount);
            Assert.Equal(amount, account.Balance);
        }

        [Theory]
        [InlineData(-100.00)]
        public void TestInvalidNegativeDeposit_BalanceRemainsZero(double amount)
        {
            Account account = new Account();
            account.deposit(amount);
            Assert.Equal(0, account.Balance);
        }

        [Theory]
        [InlineData(15.50, "deposit")]
        public void TestRecordTransaction_CorrectLedgerValues(double amount, string type)
        {
            Account account = new Account();
            account.deposit(amount);
            Assert.Equal(amount, account.Ledger[0].Amount);
            Assert.Equal(type, account.Ledger[0].Type);
        }

        [Theory]
        [InlineData(0, "deposit")]
        public void TestInvalidRecordTransaction_EmptyLedger(double amount, string type)
        {
            Account account = new Account();
            account.deposit(amount);
            Assert.Empty(account.Ledger);
        }

    }
}
