namespace icModel.Abstract {
    public interface ICryptoKey {
        int[,] KeyCodes { get; set; }
        ICryptoKeyValidator Validator { get; }
        string ToString();
    }
}