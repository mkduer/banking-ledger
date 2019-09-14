using System;
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
        [InlineData(145, 45)]
        public void TestViewBalance_CorrectBalanceValue(double depositAmount, double withdrawAmount)
        {
            Account account = new Account();
            Assert.Equal(0, account.Balance);

            account.deposit(depositAmount);
            Assert.Equal(depositAmount, account.Balance);

            account.withdraw(withdrawAmount);
            Assert.Equal(depositAmount - withdrawAmount, account.Balance);
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
        public void TestInvalidNegativeDepositValue_BalanceRemainsZero(double amount)
        {
            Account account = new Account();
            account.deposit(amount);
            Assert.Equal(0, account.Balance);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(98)]
        [InlineData(746123484486.23498725)]
        [InlineData(999999999999.99999999)]
        public void TestValidWithdrawal_CorrectBalanceDecrease(double amount)
        {
            double expected = 10;
            Account account = new Account(amount + expected);
            account.withdraw(amount);
            Assert.Equal(expected, account.Balance);
        }

        [Theory]
        [InlineData(-25.00)]
        public void TestInvalidNegativeWithdrawalValue_BalanceRemainsZero(double amount)
        {
            Account account = new Account();
            account.withdraw(amount);
            Assert.Equal(0, account.Balance);
        }

        [Theory]
        [InlineData(15.50, "deposit")]
        public void TestRecordTransaction_CorrectLedgerAfterSingleDeposit(double amount, string type)
        {
            Account account = new Account();
            account.deposit(amount);
            Assert.Equal(amount, account.Ledger[0].Amount);
            Assert.Equal(type, account.Ledger[0].Type);
        }

        [Theory]
        [InlineData(29.89, "withdraw")]
        public void TestRecordTransaction_CorrectLedgerAfterSingleWithdrawal(double amount, string type)
        {
            Account account = new Account(500);
            account.withdraw(amount);
            Assert.Equal((amount * -1), account.Ledger[0].Amount);
            Assert.Equal(type, account.Ledger[0].Type);
        }

        [Theory]
        [InlineData(1.15, 3001.05, 25.00)]
        public void TestRecordTransaction_CorrectLedgerAfterMultipleDeposits(params double[] amounts)
        {
            string type = "deposit";
            Account account = new Account();
            Array.ForEach(amounts, amount => 
                account.deposit(amount)
            );

            int i = 0;
            foreach (double amount in amounts) {
                Assert.Equal(amount, account.Ledger[i].Amount);
                Assert.Equal(type, account.Ledger[i].Type);
                i += 1;
            }
        }

        [Theory]
        [InlineData(5.25, 155.05, 0.01)]
        public void TestRecordTransaction_CorrectLedgerAfterMultipleWithdrawals(params double[] amounts)
        {
            string type = "withdraw";
            Account account = new Account(500);
            Array.ForEach(amounts, amount => 
                account.withdraw(amount)
            );

            int i = 0;
            foreach (double amount in amounts) {
                Assert.Equal((amount * -1), account.Ledger[i].Amount);
                Assert.Equal(type, account.Ledger[i].Type);
                i += 1;
            }
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-210)]
        public void TestInvalidRecordTransactionWithDeposit_EmptyLedger(double amount)
        {
            Account account = new Account();
            account.deposit(amount);
            Assert.Empty(account.Ledger);
        }

        [Theory]
        [InlineData(-45.23)]
        [InlineData(0)]
        public void TestInvalidRecordTransactionWithWithdrawal_EmptyLedger(double amount)
        {
            Account account = new Account();
            account.withdraw(amount);
            Assert.Empty(account.Ledger);
        }

    }
}
