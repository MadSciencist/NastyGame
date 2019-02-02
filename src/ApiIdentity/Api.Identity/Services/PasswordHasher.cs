using System;
using System.Security.Cryptography;
using Api.Identity.Extensions;

namespace Api.Identity.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public int SaltLength { get; } = 16;
        public int HashLength { get; } = 30;
        public int RfcAlgorithmIterations { get; } = 2000;

        public string CreateHashString(string passwordToHash)
        {
            byte[] salt = CreateHashingSalt();

            var pbkdf2 = new Rfc2898DeriveBytes(passwordToHash, salt, RfcAlgorithmIterations);

            byte[] hashedPasswordBytes = pbkdf2.GetBytes(HashLength);

            byte[] hashedPasswordWSaltBytes = new byte[SaltLength + HashLength];
            Array.Copy(salt, 0, hashedPasswordWSaltBytes, 0, SaltLength);
            Array.Copy(hashedPasswordBytes, 0, hashedPasswordWSaltBytes, SaltLength, HashLength);

            return Convert.ToBase64String(hashedPasswordWSaltBytes);
        }

        private byte[] CreateHashingSalt()
        {
            byte[] salt = new byte[SaltLength];
            new RNGCryptoServiceProvider().GetBytes(salt);
            return salt;
        }

        public bool IsPasswordMatching(string savedPasswordHash, string plainPasswordToCompare)
        {
            byte[] salt = new byte[SaltLength];

            // TODO validate if savedPasswordHash is valid base64 string
            byte[] savedPasswordHashBytes = Convert.FromBase64String(savedPasswordHash);
            Array.Copy(savedPasswordHashBytes, 0, salt, 0, SaltLength);

            byte[] hash = GetPlainPasswordToCompareHashBytes(plainPasswordToCompare, salt);

            return hash.Compare(savedPasswordHashBytes, hash, SaltLength, 0, HashLength);
        }

        private byte[] GetPlainPasswordToCompareHashBytes(string plainPasswordToCompare, byte[] salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(plainPasswordToCompare, salt, RfcAlgorithmIterations);
            return pbkdf2.GetBytes(HashLength);
        }
    }
}
