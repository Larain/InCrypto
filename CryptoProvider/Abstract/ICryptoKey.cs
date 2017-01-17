using System.Collections.ObjectModel;

namespace icModel.Abstract {
    public interface ICryptoKey {
        ObservableCollection<ObservableCollection<int>> KeyCodes { get; set; }
        int[,] KeyArray { get; }
        ICryptoKeyValidator Validator { get; }
        string ToString();
    }
}