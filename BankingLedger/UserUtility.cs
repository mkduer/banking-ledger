using System;
using System.Security.Cryptography;
using System.Text;

namespace BankingLedger
{
    public static class UserUtility
    {
        private const int _BYTESIZE = 16;
        private const int _MAXBYTESIZE = 23;
        private const int _ITERATIONS = 3000;

        // check that id parameter is valid
        public static bool validateUserID(string id, ref string userID)
        {
            if (string.IsNullOrEmpty(id)) {
                return false;
            }
            userID = id;
            return true;
        }


        // check that name parameters are valid
        public static bool validateRealName(string first, string last, ref string userFirstName, ref string userLastName)
        {
            if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(last)) {
                return false;
            }

            userFirstName = first;
            userLastName = last;

            return true;
        }

        public static bool createPassword(ref string temp)
        {
            return _createPassword(ref temp);
        }

        public static bool createHashSalt(ref string temp)
        {
            return _createHashSalt(ref temp);
        }

        // verify password
        public static bool verifyPassword(ref User user, ref string temp)
        {
            return _verifyPassword(ref user, ref temp);
        }

        public static bool verifyUser(ref User user, ref string id)
        {
            return _verifyUser(ref user, ref id);
        }

        // create a temp password and check that it is valid
        private static bool _createPassword(ref string temp)
        {
            ConsoleKeyInfo key;

            // Create a valid password allowing for a maximum of characters, symbols, numbers
            // this could be modified to not include invalid ascii values
            do {
                key = Console.ReadKey(true);

                if ((int) key.Key > 31 && (int) key.Key < 127) {
                    temp += key.KeyChar;
                    Console.Write("*");
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();

            if (temp.Length < 8 || string.IsNullOrEmpty(temp)) {
                temp = "";
                return false;
            }
            return true;
        }

        // create hash and salt
        private static bool _createHashSalt(ref string temp)
        {
            // modified from source: https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
            // this version incorporates SHA256 explicitly, while many versions online use SHA1 behind the PBKF2 function
            byte[] randSalt = new byte[_BYTESIZE];
            byte[] hashBytes = new byte[_BYTESIZE + _MAXBYTESIZE];

            if (string.IsNullOrEmpty(temp)) {
                return false;
            }

            // Create hash and salt
            RNGCryptoServiceProvider randCSP = new RNGCryptoServiceProvider();
            randCSP.GetBytes(randSalt);
            var derived = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(temp), randSalt, _ITERATIONS, HashAlgorithmName.SHA256);
            byte[] hash = derived.GetBytes(_MAXBYTESIZE);

            Array.Copy(randSalt, 0, hashBytes, 0, _BYTESIZE);
            Array.Copy(hash, 0, hashBytes, _BYTESIZE, _MAXBYTESIZE);
            temp = Convert.ToBase64String(hashBytes);

            return true;
        }

        // verify that username matches
        private static bool _verifyUser(ref User user, ref string id)
        {
            return id == user.UserID;
        }

        // verify password
        private static bool _verifyPassword(ref User user, ref string temp)
        {
            // modified from source: https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129
            // this version incorporates SHA256 explicitly, while many versions online use SHA1 behind the PBKF2 function
            if (string.IsNullOrEmpty(temp)) {
                return false;
            }

            byte[] hashBytes = Convert.FromBase64String(user.Hash);
            byte[] salt = new byte[_BYTESIZE];
            Array.Copy(hashBytes, 0, salt, 0, _BYTESIZE);

            // Hash provided password
            var derived = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(temp), salt, _ITERATIONS, HashAlgorithmName.SHA256);
            byte[] hash = derived.GetBytes(_MAXBYTESIZE);

            for (int i = 0; i < _BYTESIZE; i++) {
                if (hashBytes[i + _BYTESIZE] != hash[i]) {
                    return false;
                }
            }
            return true;
        }


    }
}