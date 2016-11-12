using icModel.Key;
using icModel.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace icModel.Key
{
    public interface ICryptoProvider
    {
        ICryptoKey CryptoKey { get; set; }
        ICryptoMethod CryptoMethod { get; set; }

        string Encrypt(string message);
        string Decrypt(string message);
    }
}
