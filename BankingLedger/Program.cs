﻿using System;

namespace BankingLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            bool logout = false;
            bool accountMenu = false;
            User user = null;

            // Welcome user
            Interface.welcomeMessage();

            // Provide welcome menu options and check for valid selection, 
            // until program is exited or next menu is entered
            do {
                Interface.resetPrompt();
                ConsoleKey[] validOptions = Interface.welcomeMenu();
                ConsoleKey selection = Console.ReadKey(true).Key;

                // Re-prompting up to the maximum number of prompt allowances
                while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && Interface.promptCount < Interface.LIMIT) {
                    Interface.increasePromptCount();;
                    Console.WriteLine($"Invalid option selected. Please try again (Attempt {Interface.promptCount})\n");
                    _ = Interface.welcomeMenu();
                    selection = Console.ReadKey(true).Key;
                }

                // Program exits if too many unsuccessful selections were attempted
                if (Interface.promptCount >= Interface.LIMIT) {
                    Interface.Exit_TooManyInvalidKeyPresses();
                }

                // Handle user's selection
                Console.Clear();
                switch (selection) {
                    case ConsoleKey.D1:
                        // User wants to login
                        Console.WriteLine("User Login");
                        if (!Interface.login(ref user)) {
                            Console.WriteLine($"\nYou may try logging in again, or if you continue to have difficulties");
                            Console.WriteLine("please contact support at {contact point} for further help.");
                        } else {
                            Console.WriteLine($"Credentials Verified.\n");
                            accountMenu = true;
                        }
                        break;
                    case ConsoleKey.D2:
                        // User wants to create account
                        Console.WriteLine("Create Account");
                        if (!Interface.createUser(ref user)) {
                            Console.WriteLine($"\nYou may try creating an account again, or if you continue to have difficulties");
                            Console.WriteLine("please contact support at {contact point} for further help.");
                        } else {
                            Console.WriteLine($"Your account was created successfully\n");
                            Console.WriteLine("ACCOUNT DETAILS:");
                            Console.WriteLine($"Username: {user.UserID}");
                            Console.WriteLine($"Name: {user.FirstName} {user.LastName}");
                        }
                        break;
                    default:
                        // User wants to exit the program
                        Console.WriteLine("Exit Program");
                        exit = true;
                        Interface.Exit();
                        break;
                }
            } while (!exit && !accountMenu);

            // Welcome validated user
            Console.WriteLine($"Welcome {user.FirstName} {user.LastName}\n");
            exit = false;

            // Provide user banking menu options 
            // check for valid selection until program is exited
            do {
                Interface.resetPrompt();
                ConsoleKey[] validOptions = Interface.mainMenu();
                ConsoleKey selection = Console.ReadKey(true).Key;

                // Re-prompting up to the maximum number of prompt allowances
                while (Array.Exists<ConsoleKey>(validOptions, option => option == selection) == false && Interface.promptCount < Interface.LIMIT) {
                    Interface.increasePromptCount();;
                    Console.WriteLine($"Invalid option selected. Please try again (Attempt {Interface.promptCount})\n");
                    _ = Interface.mainMenu();
                    selection = Console.ReadKey(true).Key;
                }

                // Program exits if too many unsuccessful selections were attempted
                if (Interface.promptCount >= Interface.LIMIT) {
                    Interface.Exit_TooManyInvalidKeyPresses();
                }

                // Handle user's selection
                Console.Clear();
                switch (selection) {
                    case ConsoleKey.D1:
                        // Make a deposit
                        Console.WriteLine("Deposit");
                        if (!Interface.makeDeposit(ref user)) {
                            Console.WriteLine("The amount was not deposited. Please try again.");
                        } else {
                            Console.WriteLine("Your transaction was successful.");
                        }
                        break;
                    case ConsoleKey.D2:
                        // Make a withdrawal
                        Console.WriteLine("Make a Withdrawal");
                        if (!Interface.makeWithdrawal(ref user)) {
                            Console.WriteLine("The amount was not withdrawn.");
                        } else {
                            Console.WriteLine("Your transaction was successful.");
                        }
                        break;
                    case ConsoleKey.D3:
                        // Check balance
                        Console.WriteLine("Check Balance");
                        Interface.checkBalance(ref user);
                        break;
                    case ConsoleKey.D4:
                        // View transactions
                        Console.WriteLine("View transactions");
                        Interface.viewTransactions(ref user);
                        break;
                    default:
                        // Logout
                        Console.WriteLine("Logout");
                        logout = true;
                        break;
                }
            } while (!logout);
        }
    }
}
