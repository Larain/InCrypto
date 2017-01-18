using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using icModel.Abstract;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace icModel.Model.Helpers {
    public static class MatrixConverters {
        public static Matrix<double> ConvertIntArrayToMatrix(int[,] arr)
        {
            if (arr == null)
                throw new NullReferenceException();

            int m = arr.GetLength(0);
            int n = arr.GetLength(1);

            if (m != n)
                throw new ArgumentException("Wrong matrix size: " + m + "x" + n);

            double[,] doublesArr = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    doublesArr[i, j] = arr[i, j];

            return DenseMatrix.OfArray(doublesArr);
        }

        public static Matrix<double> ConvertIntListToMatrix(int[][] arr)
        {
            if (arr == null)
                throw new NullReferenceException();

            int m = arr.GetLength(0);
            int n = arr[0].GetLength(0);

            if (m != n)
                throw new ArgumentException("Wrong matrix size: " + m + "x" + n);

            double[,] doublesArr = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    doublesArr[i, j] = arr[i][j];

            return DenseMatrix.OfArray(doublesArr);
        }

        public static Matrix<double> ConverDoubleListToMatrix(List<List<double>> arr)
        {
            if (arr == null)
                throw new NullReferenceException();

            int m = arr.Count;
            int n = arr[0].Count;

            if (m != n)
                throw new ArgumentException("Wrong matrix size: " + m + "x" + n);

            double[,] doublesArr = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    doublesArr[i, j] = arr[i][j];

            return DenseMatrix.OfArray(doublesArr);
        }

        public static Matrix<double> ConvertIntObservableCollectionToMatrix(ObservableCollection<ObservableCollection<double>> arr)
        {
            if (arr == null)
                throw new NullReferenceException();

            int m = arr.Count;
            int n = arr[0].Count;

            if (m != n)
                throw new ArgumentException("Wrong matrix size: " + m + "x" + n);

            double[,] doublesArr = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    doublesArr[i, j] = arr[i][j];

            return DenseMatrix.OfArray(doublesArr);
        }

        public static ObservableCollection<ObservableCollection<double>> ConvertMatrixToObservableCollection(Matrix<double> arr)
        {
            if (arr == null)
                throw new NullReferenceException();

            int m = arr.ColumnCount;
            int n = arr.RowCount;

            if (m != n)
                throw new ArgumentException("Wrong matrix size: " + m + "x" + n);

            ObservableCollection<ObservableCollection<double>> outer = 
                new ObservableCollection<ObservableCollection<double>>();

            for (int i = 0; i < n; i++) {
                ObservableCollection<double> inner = new ObservableCollection<double>();
                for (int j = 0; j < n; j++)
                    inner.Add(arr[i, j]);
                outer.Add(inner);

            }
            return outer;
        }
    }
}