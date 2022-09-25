using Love.Discussion.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Services.Util
{
    public class Encryption : IEncryption
    {
        public Encryption(string encryptionKey)
        {
            _key = Convert.FromBase64String(encryptionKey);
        }
        private byte[] _key;

        public string Decrypt(string encryptedText)
        {
            ArgumentNullException.ThrowIfNull(encryptedText);

            var ivAndCipherText = Convert.FromBase64String(encryptedText);
            using var aes = Aes.Create();
            var ivLength = aes.BlockSize / 8;
            aes.IV = ivAndCipherText.Take(ivLength).ToArray();
            aes.Key = _key;
            using var cipher = aes.CreateDecryptor();
            var cipherText = ivAndCipherText.Skip(ivLength).ToArray();
            var text = cipher.TransformFinalBlock(cipherText, 0, cipherText.Length);

            return Encoding.UTF8.GetString(text);
        }

        public string Encrypt(string plainText)
        {
            ArgumentNullException.ThrowIfNull(plainText);

            using var aes = Aes.Create();
            aes.Key = _key;
            aes.GenerateIV();
            using var cipher = aes.CreateEncryptor();
            var text = Encoding.UTF8.GetBytes(plainText);
            var cipherText = cipher.TransformFinalBlock(text, 0, text.Length);

            return Convert.ToBase64String(aes.IV.Concat(cipherText).ToArray());
        }
    }
}
