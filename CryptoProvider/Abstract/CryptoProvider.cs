namespace icModel.Abstract {
    public interface ICryptoProvider {
        ICryptoKey Key { get; set; }
        string[] Encrypt(string[] message);
        string[] Decrypt(string[] message);
    }
}