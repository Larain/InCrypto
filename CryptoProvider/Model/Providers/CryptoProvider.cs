using icModel.Abstract;

namespace icModel.Model.Providers
{
    public class CryptoProvider : ICryptoProvider
    {
        public ICryptoKey CryptoKey { get; set; }
        public CryptoMethod CryptoMethod { get; set; }
        public CryptoProvider(ICryptoKey cryptoKey, CryptoMethod cryptoMethod)
        {
            CryptoKey = cryptoKey;
            CryptoMethod = cryptoMethod;
        }
        public string[] Encrypt(string[] message)
        {
            return CryptoMethod.Encrypt(message);
        }

        public string[] Decrypt(string[] message)
        {
            return CryptoMethod.Decrypt(message);
        }
    }
}
