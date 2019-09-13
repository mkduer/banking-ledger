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
        [InlineData(145, -45)]
        public void TestViewBalance_CorrectBalanceValue(double depositAmount, double withdrawAmount)
        {
            Account account = new Account();
            Assert.Equal(0, account.Balance);

            account.deposit(depositAmount);
            Assert.Equal(depositAmount, account.Balance);

            account.withdraw(withdrawAmount);
            Assert.Equal(depositAmount + withdrawAmount, account.Balance);
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
        [InlineData(0)]
        [InlineData(-98)]
        [InlineData(-746123484486.23498725)]
        [InlineData(-999999999999.99999999)]
        public void TestValidWithdrawal_CorrectBalanceDecrease(double amount)
        {
            double expected = 10;
            Account account = new Account((amount * -1) + expected);
            account.withdraw(amount);
            Assert.Equal(expected, account.Balance);
        }

        [Theory]
        [InlineData(25.00)]
        public void TestInvalidPositiveWithdrawal_BalanceRemainsZero(double amount)
        {
            Account account = new Account();
            account.withdraw(amount);
            Assert.Equal(0, account.Balance);
        }

        [Theory]
        [InlineData(15.50, "deposit")]
        public void TestRecordTransaction_CorrectLedgerAfterDeposit(double amount, string type)
        {
            Account account = new Account();
            account.deposit(amount);
            Assert.Equal(amount, account.Ledger[0].Amount);
            Assert.Equal(type, account.Ledger[0].Type);
        }

        [Theory]
        [InlineData(-29.89, "withdraw")]
        public void TestRecordTransaction_CorrectLedgerAfterWithdrawal(double amount, string type)
        {
            Account account = new Account(500);
            account.withdraw(amount);
            Assert.Equal(amount, account.Ledger[0].Amount);
            Assert.Equal(type, account.Ledger[0].Type);
        }

        [Theory]
        [InlineData(0, "deposit")]
        [InlineData(-210, "deposit")]
        public void TestInvalidRecordTransactionWithDeposit_EmptyLedger(double amount, string type)
        {
            Account account = new Account();
            account.deposit(amount);
            Assert.Empty(account.Ledger);
        }

        [Theory]
        [InlineData(45.23, "withdraw")]
        [InlineData(0, "withdraw")]
        public void TestInvalidRecordTransactionWithWithdrawal_EmptyLedger(double amount, string type)
        {
            Account account = new Account();
            account.withdraw(amount);
            Assert.Empty(account.Ledger);
        }

    }
}
