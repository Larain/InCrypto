using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using icModel.Abstract;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using icModel.Model.Providers;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace icModel.Model.Keys {
    [Serializable]
    public class HillKey : ICryptoKey, IEquatable<HillKey> {
        private ICryptoKeyValidator _validator;
        private Matrix<double> _matrix;

        public HillKey(Matrix<double> matrix, Abstract.Alphabet alphabet) {
            Alphabet = alphabet;
            Matrix = matrix;
        }

        public HillKey(double[,] matrix, Abstract.Alphabet alphabet) {
            Alphabet = alphabet;
            Matrix = DenseMatrix.OfArray(matrix);
        }

        public HillKey(int[][] matrix, Abstract.Alphabet alphabet)
        {
            Alphabet = alphabet;
            Matrix = MatrixConverters.ConvertIntListToMatrix(matrix);
        }

        public HillKey(ObservableCollection<ObservableCollection<double>> matrix, Abstract.Alphabet alphabet)
        {
            Alphabet = alphabet;
            Matrix = MatrixConverters.ConvertIntObservableCollectionToMatrix(matrix);
        }

        #region Properties

        public ICryptoKeyValidator Validator {
            get { return _validator ?? (_validator = new HillKeyValidator()); }
        }

        public Abstract.Alphabet Alphabet { get; set; }
        [SoapIgnore]
        public ObservableCollection<ObservableCollection<double>> ObservableMatrix { get; private set; }

        public Matrix<double> Matrix {
            get { return _matrix; }
            set {
                _matrix = value;
                if (!Validator.IsValid(this))
                    throw new ValidationException("Matrix is not Valid");
                ObservableMatrix = MatrixConverters.ConvertMatrixToObservableCollection(value);
            }
        }

        #endregion

        #region Methods

        public override string ToString() {
            string output = "";
            for (int i = 0; i < Matrix.RowCount; i++) {
                for (int j = 0; j < Matrix.ColumnCount; j++) {
                    output += "[" + Matrix[i, j] + "] ";
                }
                output += "\n";
            }
            return output;
        }

        #endregion

        #region Overrides 

        public override bool Equals(object obj) {
            if (obj == null)
                throw new NullReferenceException();
            HillKey comarable = (HillKey) obj;

            if (comarable.Matrix.ColumnCount != Matrix.ColumnCount || comarable.Matrix.RowCount != Matrix.RowCount)
                return false;

            for (int i = 0; i < Matrix.RowCount; i++) {
                for (int j = 0; j < Matrix.ColumnCount; j++) {
                    if (Math.Abs(Matrix[i, j] - comarable.Matrix[i, j]) > 0.001)
                        return false;
                }
            }
            return true;
        }

        public bool Equals(HillKey obj) {
            if (obj == null)
                throw new NullReferenceException();
            if (obj.Matrix.ColumnCount != Matrix.ColumnCount || obj.Matrix.RowCount != Matrix.RowCount)
                return false;

            for (int i = 0; i < Matrix.RowCount; i++) {
                for (int j = 0; j < Matrix.ColumnCount; j++) {
                    if (Math.Abs(Matrix[i, j] - obj.Matrix[i, j]) > 0.001)
                        return false;
                }
            }
            return true;
        }

        public override int GetHashCode() {
            int hc = Matrix.RowCount;
            for (int i = 0; i < Matrix.RowCount; ++i) {
                for (int j = 0; j < Matrix.ColumnCount; j++) {
                    hc = unchecked(hc*314159 + Convert.ToInt32(Matrix[i, j]));
                }
            }
            return hc;
        }

        #endregion
    }
}