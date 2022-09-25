using Love.Discussion.Core.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Love.Discussion.Services.Util
{
    public class Hashing : IHashing
    {
        public string ComputeHash(string plainText, byte[] saltBytes = null)
        {
            ArgumentNullException.ThrowIfNull(plainText);

            if (saltBytes == null || saltBytes.Length <= 0)
            {
                saltBytes = GenerateSaltBytes();
            }

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash = SHA256.Create();

            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            return hashValue;
        }

        public bool VerifyHash(string plainText, string hashValue)
        {
            ArgumentNullException.ThrowIfNull(plainText);
            ArgumentNullException.ThrowIfNull(hashValue);

            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            int hashSizeInBits = 256;
            int hashSizeInBytes;

            hashSizeInBytes = hashSizeInBits / 8;

            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];

            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            string expectedHashString = ComputeHash(plainText, saltBytes);

            return hashValue == expectedHashString;
        }
        private byte[] GenerateSaltBytes()
        {
            var rngProvider = RandomNumberGenerator.Create();
            int minSaltSize = 4;
            int maxSaltSize = 8;

            Random random = new Random();
            int saltSize = random.Next(minSaltSize, maxSaltSize);

            var saltBytes = new byte[saltSize];


            rngProvider.GetNonZeroBytes(saltBytes);

            return saltBytes;
        }
    }

}
