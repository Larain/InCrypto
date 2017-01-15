using System.Collections.Generic;

namespace icModel.Abstract
{
    public interface ICryptoKeyValidator
    {
        bool IsValid(ICryptoKey key);
        bool IsValid(List<List<int>> key);
    }
}