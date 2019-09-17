using Xunit;
using System;
using System.Collections.Generic;

namespace BankingLedger.UnitTests
{

    public class BankingLedger_TestUsersCollection
    {
        [Theory]
        [InlineData("bobTheHobbit")]
        [InlineData("ralphTheDestroyer")]
        public void TestAddUser_UserExistsTrue(string id)
        {
            UsersCollection users = new UsersCollection();
            User user = new User(id, "firstname", "lastname", "hash");
            users.add(user);
            Assert.True(users.hasUser(id));
        }

        [Theory]
        [InlineData("jello", "mellow", "yellow", "hello")]
        public void TestAddMultipleUsers_MultipleUsersExistTrue(params string[] multiID)
        {
            UsersCollection users = new UsersCollection();
            foreach (string id in multiID) {
                User user = new User(id, "firstname", "lastname", "hash");
                users.add(user);
            }

            Array.ForEach(multiID, id => 
                Assert.True(users.hasUser(id))
            );
        }

        [Theory]
        [InlineData("Jane")]
        [InlineData("John")]
        public void TestRetreiveUser_UserSuccessfullyRetrieved(string id)
        {
            UsersCollection users = new UsersCollection();
            User user = new User(id, "firstname", "lastname", "hash");
            users.add(user);
            Assert.Equal(user, users.retrieveUser(id));
        }

        [Theory]
        [InlineData("eleanorRoosevelt", "ghosty")]
        [InlineData("usainBolt", "lochnessMonster")]
        public void TestInvalidRetreiveUser_UserNotRetrievedThrows(string actualUser, string nonexistentUser)
        {
            UsersCollection users = new UsersCollection();
            User user = new User(actualUser, "firstname", "lastname", "hash");
            users.add(user);
            Assert.Throws<KeyNotFoundException>(() => users.retrieveUser(nonexistentUser));
        }

        [Theory]
        [InlineData("glindaTheGood", "misunderstoodWitch")]
        public void TestInvalidAddUser_UserDoesNotExistThrows(string collectionID, string differentID)
        {
            UsersCollection users = new UsersCollection();
            User user = new User(collectionID, "firstname", "lastname", "hash");
            users.add(user);
            Assert.False(users.hasUser(differentID));
        }


        [Theory]
        [InlineData(null)]
        public void TestInvalidAddUser_NullUserThrows(string id)
        {
            UsersCollection users = new UsersCollection();
            User user = new User(id, "firstname", "lastname", "hash");
            Assert.Throws<ArgumentNullException>(() => users.add(user));
        }

        [Theory]
        [InlineData("same", "different", "same")]
        public void TestInvalidAddUser_SameUserIdThrows(params string[] multiID)
        {
            UsersCollection users = new UsersCollection();

            // add first user
            User userInitial = new User(multiID[0], "firstname", "lastname", "hash");
            users.add(userInitial);

            // add different user
            User userDifferent = new User(multiID[1], "firstname", "lastname", "hash");
            users.add(userDifferent);

            // add same name user
            User userSame = new User(multiID[2], "firstname", "lastname", "hash");
            
            // Same user ids are not allowed
            Assert.Throws<ArgumentException>(() => users.add(userSame));
            
        }
    }
}