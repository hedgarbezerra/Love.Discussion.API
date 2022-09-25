using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Interfaces
{
    public interface IEncryption
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
