using System.Collections.ObjectModel;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace icModel.Abstract {
    public interface ICryptoKey {
        ObservableCollection<ObservableCollection<double>> ObservableMatrix { get; }
        Matrix<double> Matrix { get; }
        IAlphabet Alphabet { get; }
        ICryptoKeyValidator Validator { get; }
        string ToString();
    }
}