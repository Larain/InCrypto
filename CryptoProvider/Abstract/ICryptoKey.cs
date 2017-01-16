using System.Collections.ObjectModel;

namespace icModel.Abstract {
    public interface ICryptoKey {
        ObservableCollection<ObservableCollection<int>> KeyCodes { get; set; }
        ICryptoKeyValidator Validator { get; }
        string ToString();
    }
}