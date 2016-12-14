namespace icModel.Abstract {
    public interface ICryptoProvider {
        ICryptoKey CryptoKey { get; set; }
        CryptoMethod CryptoMethod { get; set; }

        string[] Encrypt(string[] message);
        string[] Decrypt(string[] message);
    }
}