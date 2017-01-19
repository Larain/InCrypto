using System;
using System.Collections.ObjectModel;
using System.Linq;
using icModel.Abstract;
using icModel.Model.Helpers;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;
using DenseMatrix = MathNet.Numerics.LinearAlgebra.Double.DenseMatrix;

//namespace icModel.Model.Keys {
//    //public class AffineKey : ICryptoKey {
//    //    private int[,] _keyArray;
//    //    private ObservableCollection<ObservableCollection<int>> _keyCodes;
//    //    private ICryptoKeyValidator _validator;

//    //    public AffineKey(int a, int b) {
//    //        KeyCodes = GenerateNewKey(a, b);
//    //    }

//    //    #region Properties

//    //    public Matrix<double> KeyCodes {
//    //        get { return _keyCodes; }
//    //        private set {
//    //            if (Validator.IsValid(value.ToArray(), Alphabet.Length))
//    //                _keyCodes = value;
//    //            else
//    //                throw new ArgumentException(
//    //                    $"AffineKey is not valid. Size of argument {value.Count}x{value[0].Count}"
//    //                    );
//    //        }

//    //    }

//    //    public int[,] KeyArray {
//    //        get { return _keyArray ?? (_keyArray = (_keyCodes != null ? this.ToIntArray() : null)); }
//    //        private set { _keyArray = value; }
//    //    }

//    //    public IAlphabet Alphabet { get; }

//    //    public ICryptoKeyValidator Validator {
//    //        get { return _validator ?? (_validator = new AffineKeyValidator()); }
//    //    }

//    //    #endregion

//    //    #region Methods

//    //    public override string ToString() {
//    //        string output = "";
//    //        for (int i = 0; i < KeyCodes.Count; i++) {
//    //            for (int j = 0; j < KeyCodes[0].Count; j++) {
//    //                output += "[" + KeyCodes[i][j] + "] ";
//    //            }
//    //            output += "\n";
//    //        }
//    //        return output;
//    //    }

//    //    private Matrix<double> GenerateNewKey(int a, int b) {
//    //        Matrix<double> matrix = DenseMatrix.OfArray(new double[1,2] { {a, b} });
//    //        return matrix;
//    //    }

//        #endregion
//    }
//}