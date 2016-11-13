using icModel.Key;
using icModel.Method;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace icModel.Provider
{
    public class CryptoProvider : ICryptoProvider
    {
        public ICryptoKey CryptoKey { get; set; }
        public ICryptoMethod CryptoMethod { get; set; }
        public CryptoProvider(ICryptoKey cryptoKey, ICryptoMethod cryptoMethod)
        {
            CryptoKey = cryptoKey;
            CryptoMethod = cryptoMethod;
        }
        public string[] Encrypt(string[] message)
        {
            return CryptoMethod.Encrypt(message, CryptoKey);
        }

        public string[] Decrypt(string[] message)
        {
            return CryptoMethod.Decrypt(message, CryptoKey);
        }
    }
}
