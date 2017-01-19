using System.Collections.ObjectModel;
using System.Xml.Serialization;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace icModel.Abstract {
    public interface ICryptoKey {
        [SoapIgnore]
        ObservableCollection<ObservableCollection<double>> ObservableMatrix { get; }
        Matrix<double> Matrix { get; }
        Alphabet Alphabet { get; set; }
        ICryptoKeyValidator Validator { get; }
        string ToString();
    }
}