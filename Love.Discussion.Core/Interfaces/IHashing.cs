using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Love.Discussion.Core.Interfaces
{
    public interface IHashing
    {
        string ComputeHash(string plainText, byte[] saltBytes = null);
        bool VerifyHash(string plainText, string hashValue);
    }
}
