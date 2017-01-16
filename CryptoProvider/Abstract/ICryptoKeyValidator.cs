using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace icModel.Abstract
{
    public interface ICryptoKeyValidator
    {
        bool IsValid(ICryptoKey key);
        bool IsValid(ObservableCollection<ObservableCollection<int>> key);
    }
}