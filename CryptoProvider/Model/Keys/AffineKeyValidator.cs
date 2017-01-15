using icModel.Abstract;

namespace icModel.Model.Keys
{
    public class AffineKeyValidator : ICryptoKeyValidator
    {
        public bool IsValid(int[,] digits)
        {
            if (digits != null)
                if (digits.GetLength(0) == 1)
                    if (digits.GetLength(1) == 2)
                        return true;
            return false;
        }

        public bool IsValid(ICryptoKey key)
        {
            return IsValid(key.KeyCodes);
        }
    }
}