using System;
using System.Collections.ObjectModel;
using icModel.Abstract;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace icModel.Model.Keys
{
    [Serializable]
    public class HillKeyValidator : ICryptoKeyValidator
    {
        public bool IsValid(ICryptoKey key) {
            if (key == null)
                throw new ValidationException("Key is null");
            if (key.Matrix == null)
                throw new ValidationException("Key Matrix is null");
            if (key.Matrix.ColumnCount != key.Matrix.RowCount)
                throw new ValidationException("Wrong matrix size");

            int det = (int)key.Matrix.Determinant();
            if(det == 0)
                throw new ValidationException("Matrix Determinant = 0");
            if (!CryptoHelper.IsNod(CryptoHelper.Mod(det, key.Alphabet.Length), key.Alphabet.Length))
                throw new ValidationException("Module and determinant are not Nod");

            return true;
        }

        public bool IsValid(int[][] key, int module)
        {
            if (key == null)
                throw new ValidationException("Null matrix");
            if (key.GetLength(1) != key.GetLength(0))
                throw new ValidationException("Wrong matrix size");

            double[,] doubleArray = new double[key.GetLength(0), key.GetLength(0)];
            for (int i = 0; i < key.GetLength(0); i++)
                for (int j = 0; j < key.GetLength(0); j++)
                    doubleArray[i, j] = key[i][j];

            Matrix<double> matrix = DenseMatrix.OfArray(doubleArray);
            int det = (int)matrix.Determinant();

            if (det == 0)
                throw new ValidationException("Matrix Determinant = 0");
            if (!CryptoHelper.IsNod(det, module))
                throw new ValidationException("Module and determinant are not Nod");

            return true;
        }

        public bool IsValid(int[,] key, int module) {
            if (key == null)
                return false;
            if (key.GetLength(1) != key.GetLength(0))
                return false;

            double[,] doubleArray = new double[key.GetLength(0), key.GetLength(0)];
            for (int i = 0; i < key.GetLength(0); i++)
                for (int j = 0; j < key.GetLength(0); j++)
                    doubleArray[i, j] = key[i,j];
            
            Matrix<double> matrix = DenseMatrix.OfArray(doubleArray);
            int det = (int)matrix.Determinant();
  
            if (det == 0)
                throw new ArithmeticException("Matrix Determinant = 0");
            if (!CryptoHelper.IsNod(det, module))
                throw new ArithmeticException("Module and determinant are not Nod");

            return true;
        }
    }
}