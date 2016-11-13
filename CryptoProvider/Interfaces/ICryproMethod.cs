using icModel.Key;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace icModel.Method
{
    public interface ICryptoMethod
    {
        string[] Encrypt(string[] message, ICryptoKey key);
        string[] Decrypt(string[] message, ICryptoKey key);
    }
}
