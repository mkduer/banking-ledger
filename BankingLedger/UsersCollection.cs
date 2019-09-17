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
        public void add(User user)
        {
            this._add(user);
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


        // add a user to collection of users, an exception is thrown if the fuction fails
        // note: duplicate keys are not allowed
        private void _add(User user)
        {
            this._users.Add(user.UserID, user);
        }

        // displays users
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