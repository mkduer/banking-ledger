using System;
using System.Collections.Generic;

namespace BankingLedger
{
    public class UsersCollection
    {
        private Dictionary<string, User> _users;

        public Dictionary<string, User> Users
        {
            get { return _users; }
        }

        public UsersCollection()
        {
            _users = new Dictionary<string, User>();
        }

        // add a user to collection of users
        public bool add(User user)
        {
            return _add(user);
        }

        // displays all users
        public void displayUsers()
        {
            this._displayUsers();
        }

        // checks if a user exists
        public bool hasUser(string userID)
        {
            return _hasUser(userID);
        }

        // retrieve a user if they exist
        public User retrieveUser(string userID)
        {
            return _users[userID];
        }

        // checks if a user exists
        private bool _hasUser(string userID)
        {
            return this._users.ContainsKey(userID);
        }


        // add a user to collection of users
        // note: duplicate keys are not allowed
        private bool _add(User user)
        {
            try {
                this._users.Add(user.UserID, user);
            } catch (ArgumentNullException) {
                Console.WriteLine("A valid username must be provided to create a user.");
                return false;
            } catch (ArgumentException) {
                Console.WriteLine("This username is already taken. Please choose a different one.");
                return false;
            }
            return true;
        }

        private void _displayUsers()
        {
            Console.WriteLine("All Users:");
            foreach (KeyValuePair<string, User> kvp in this._users)
            {
                Console.WriteLine(kvp.Key);
            }
        }
    }
}