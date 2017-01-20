using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace icModel.Model.Helpers {
    public static class MatrixOperation {
        public static double CalculateMinor(this Matrix<double> src, int row, int col)
        {
            var minorSubmatrix = GetSubmatrix(src, row, col);
            return minorSubmatrix.Determinant();
        }

        public static double[,] Cofactor(this Matrix<double> m)
        {
            double[,] result = new double[m.RowCount, m.RowCount];

            for (int j = 0; j < m.RowCount; j++)
                for (int i = 0; i < m.RowCount; i++)
                {
                    // Get minor of element (j, i) - not (i, j) because
                    // this is where the transpose happens.
                    double cofactor = 0;
                    double minor = CalculateMinor(m, j, i);

                    // Multiply by (−1)^{i+j}
                    double factor = (CryptoHelper.Mod((i + j), 2) == 1) ? -1.0f : 1.0f;
                    cofactor = minor * factor;

                    result[i, j] = cofactor;
                }

            return result;
        }
        public static Matrix<double> GetSubmatrix(this Matrix<double> src, int row, int col)
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
    }
}