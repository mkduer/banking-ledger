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
        }
    }
}
