using System.Collections.Generic;
using icModel.Abstract;

namespace icModel.Model.Keys
{
    public class AffineKeyValidator : ICryptoKeyValidator
    {
        public bool IsValid(List<List<int>> digits)
        {
            if (digits != null)
                if (digits.Count == 1)
                    if (digits[0].Count == 2)
                        return true;
            return false;
        }

        public bool IsValid(ICryptoKey key)
        {
            return IsValid(key.KeyCodes);
        }
    }
}