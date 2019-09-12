using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            user.FirstName = "Constantina";
            user.LastName = "Random";
            Console.WriteLine($"Hello {user.FirstName} {user.LastName}!");
        }
    }
}
