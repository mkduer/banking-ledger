using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            user.FirstName = "RandomFirstName";
            user.LastName = "RandomLastName";
            Console.WriteLine($"Hello {user.FirstName} {user.LastName}!");

            Account account = new Account();
            account.deposit(500);
            account.withdraw(250);
            account.withdraw(50);
            account.withdraw(5);
            account.deposit(500);
            account.displayTransactions();
        }
    }
}
