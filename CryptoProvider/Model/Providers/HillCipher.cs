using System;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using icModel.Abstract;
using icModel.Model.Entities;
using System.Windows.Input;
using icModel.Model.Helpers;
using icModel.Model.Keys;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace icModel.Model.Providers {
    [Serializable]
    public class HillCipher : ICryptoProvider {
        private ICryptoKey _key;

        #region Formulas

        public int Determinant {
            get { return (int) Key.Matrix.Determinant(); }
        }

        public int DeterminantModule {
            get { return CryptoHelper.Mod(Determinant, Key.Alphabet.Length); }
        }

        public int ReciprocalValue {
            get { return ModInverse(DeterminantModule, Key.Alphabet.Length); }
        }

        public double[,] AdjugateMatrix {
            get { return Invert(Key.Matrix); }
        }

        public double[,] DecryptoMatrix {
            get {
                double[,] decryptoMatrix = new double[Key.Matrix.RowCount, Key.Matrix.ColumnCount];

                for (int i = 0; i < decryptoMatrix.GetLength(0); i++) {
                    for (int j = 0; j < decryptoMatrix.GetLength(1); j++) {
                        decryptoMatrix[i, j] = CryptoHelper.Mod((int) AdjugateMatrix[i, j]*ReciprocalValue,
                            Key.Alphabet.Length);
                    }
                }
                return decryptoMatrix;
            }
        }

        #endregion

        public ICryptoKey Key {
            get { return _key; }
            set {
                if (value != null)
                    _key = (HillKey) value;
            }
        }

        public string[] Encrypt(string[] plainText) {
            return Process(plainText, Mode.Encrypt);
        }

        public string[] Decrypt(string[] cipher) {
            return Process(cipher, Mode.Decrypt);
        }

        #region Private Methods

        private string[] Process(string[] message, Mode mode) {
            int index = 0;
            string[] proccessedString = new string[message.Length];

            double[,] matrix = Key.Matrix.ToArray();
            if (mode == Mode.Decrypt) {
                matrix = DecryptoMatrix;
                var s = Invert(Key.Matrix);
            }

            foreach (string line in message) {

                int pos = 0;
                int matrixSize = _key.Matrix.ColumnCount;

                string newLine = "";
                while (pos < line.Length) {
                    string result = "";
                    string substring = "";
                    try {
                        substring = line.Substring(pos, matrixSize);
                    }
                    catch (ArgumentOutOfRangeException) {
                        throw new CipherException(string.Format("Invalid length of string {0} in line {1}", line.Length,
                            index));
                    }

                    pos += matrixSize;

                    for (int i = 0; i < matrixSize; i++) {
                        string portion = "";
                        var charPosition = 0;

                        for (int j = 0; j < matrixSize; j++) {
                            charPosition += (int)matrix[j,i] * Key.Alphabet.GetIndex(substring[j]);
                        }

                        result += Key.Alphabet.GetSymbol(CryptoHelper.Mod(charPosition, Key.Alphabet.Length));
                    }

                    newLine += result;
                }
                proccessedString[index++] = newLine;
            }

            return proccessedString;
        }

        private double[,] GetDecryptMatrix() {

            double det = Key.Matrix.Determinant();
            double moduleDet = CryptoHelper.Mod((int)det, Key.Alphabet.Length);
            int rec = CryptoHelper.Reciprocal((int)moduleDet, Key.Alphabet.Length);

            double[,] adjugateMatrix = new double[Key.Matrix.RowCount, Key.Matrix.ColumnCount];
            double[,] decryptoMatrix = new double[Key.Matrix.RowCount, Key.Matrix.ColumnCount];

            Matrix<double> matrix = Key.Matrix.Inverse();
            
            for (int i = 0; i < adjugateMatrix.GetLength(0); i++) {
                for (int j = 0; j < adjugateMatrix.GetLength(1); j++) {
                    adjugateMatrix[i, j] = Math.Round((matrix[i, j] * det));
                    decryptoMatrix[i, j] = CryptoHelper.Mod((int) adjugateMatrix[i, j] * rec, Key.Alphabet.Length);
                }
            }

            return decryptoMatrix;
        }

        double CalculateMinor(Matrix<double> src, int row, int col)
        {
            var minorSubmatrix = GetSubmatrix(src, row, col);
            return minorSubmatrix.Determinant();
        }

        double[,] Invert(Matrix<double> m) {
            // Calculate the inverse of the determinant of m.
            double det = m.Determinant();
            double inverseDet = ModInverse((int)det, Key.Alphabet.Length);
            double[,] result = new double[m.RowCount, m.RowCount];

            for (int j = 0; j < m.RowCount; j++)
                for (int i = 0; i < m.RowCount; i++) {
                    // Get minor of element (j, i) - not (i, j) because
                    // this is where the transpose happens.
                    double cofactor = 0;
                    double minor = CalculateMinor(m, j, i);

                    // Multiply by (−1)^{i+j}
                    double factor = (CryptoHelper.Mod((i + j),2) == 1) ? -1.0f : 1.0f;
                    cofactor = minor*factor;

                    result[i, j] = cofactor;
                }

            return result;
        }
        Matrix<double> GetSubmatrix(Matrix<double> src, int row, int col)
        {
            int rowCount = 0;

            double[,] arr = new double[src.RowCount - 1, src.RowCount - 1];
            for (int i = 0; i < src.RowCount; i++)
            {
                if (i != row)
                {
                    var colCount = 0;
                    for (int j = 0; j < src.RowCount; j++)
                    {
                        if (j != col)
                        {
                            arr[rowCount, colCount] = src[i, j];
                            colCount++;
                        }
                    }
                    rowCount++;
                }
            }
            return DenseMatrix.OfArray(arr);
        }

        Tuple<int, Tuple<int, int>> ExtendedEuclid(int a, int b)
        {
            int x = 1, y = 0;
            int xLast = 0, yLast = 1;
            int q, r, m, n;
            while (a != 0)
            {
                q = b / a;
                r = b % a;
                m = xLast - q * x;
                n = yLast - q * y;
                xLast = x; yLast = y;
                x = m; y = n;
                b = a; a = r;
            }
            return new Tuple<int, Tuple<int, int>>(b, new Tuple<int, int>(xLast, yLast));
        }

        int ModInverse(int a, int m)
        {
            return CryptoHelper.Mod((ExtendedEuclid(a, m).Item2.Item1 + m), m);
        }

        #endregion

    }
}
