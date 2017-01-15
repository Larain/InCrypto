using System.Collections.Generic;

namespace icModel.Abstract {
    public interface ICryptoKey {
        List<List<int>> KeyCodes { get; set; }
        ICryptoKeyValidator Validator { get; }
        string ToString();
    }
}