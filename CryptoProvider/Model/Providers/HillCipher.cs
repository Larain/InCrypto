﻿using System;
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
            get {
                return CryptoHelper.ModInverse(DeterminantModule, Key.Alphabet.Length);
            }
        }

        public double[,] CofactorMatrix
        {
            get { return Key.Matrix.Cofactor(); }
        }

        public double[,] DecryptoMatrix {
            get {
                double[,] decryptoMatrix = new double[Key.Matrix.RowCount, Key.Matrix.ColumnCount];

                for (int i = 0; i < decryptoMatrix.GetLength(0); i++) {
                    for (int j = 0; j < decryptoMatrix.GetLength(1); j++) {
                        decryptoMatrix[i, j] = CryptoHelper.Mod((int) Math.Round(CofactorMatrix[i, j])*ReciprocalValue,
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
                var s = Key.Matrix.Cofactor();
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
            int rec = CryptoHelper.ModInverse((int)moduleDet, Key.Alphabet.Length);

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

        #endregion

    }
}
