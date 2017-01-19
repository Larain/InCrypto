using System.Collections.ObjectModel;

namespace icModel.Abstract {
    public interface ICryptoKeyValidator {
        bool IsValid(ICryptoKey key);
    }
}