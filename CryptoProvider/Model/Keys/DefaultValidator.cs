using icModel.Abstract;

namespace icModel.Model.Keys
{
    public class DefaultValidator : ICryptoKeyValidator
    {
        public bool IsValid(ICryptoKey key)
        {
            return IsValid(key.KeyCodes);
        }

        public bool IsValid(int[,] key)
        {
            return key == null;
        }
    }
}