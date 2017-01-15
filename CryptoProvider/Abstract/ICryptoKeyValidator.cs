namespace icModel.Abstract
{
    public interface ICryptoKeyValidator
    {
        bool IsValid(ICryptoKey key);
        bool IsValid(int[,] key);
    }
}