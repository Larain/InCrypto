using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using icModel.Model.Keys;

namespace icApplication.Exmaination {
    class ExaminationManager {
        private HashSet<ExaminationVariant> _variants;
        private HashSet<MatrixClass> _marixList;
        private IAlphabet _alphabet;
        private int _matrixSize;
        private int _generatedMaxValue;
        private int _generatedMinValue;
        private int _textLength;
        private int _variantsAmount;

        /// <summary>
        /// Set amount of generated variants
        /// </summary>
        /// <param name="variantsAmount"></param>
        public ExaminationManager() {
            InitalizeDefault();
            GenerateNewVariants(_variantsAmount);
        }

        private void InitalizeDefault() {
            _alphabet = new SimpleAlphabet();
            _variantsAmount = 20;
            MatrixSize = 2;
            GeneratedMaxValue = _alphabet.Length;
            GeneratedMinValue = 0;
            TextLength = 4;
        }

        #region Properties

        public List<ExaminationVariant> VariantsList {
            get { return _variants.ToList(); }
        }

        public int MatrixSize {
            get { return _matrixSize; }
            set { _matrixSize = value; }
        }

        public int GeneratedMaxValue {
            get { return _generatedMaxValue; }
            set { _generatedMaxValue = value; }
        }

        public int GeneratedMinValue {
            get { return _generatedMinValue; }
            set { _generatedMinValue = value; }
        }

        public int TextLength {
            get { return _textLength; }
            set { _textLength = value; }
        }

        public int VariantsAmount {
            get { return _variantsAmount; }
            set { _variantsAmount = value; }
        }

        #endregion


        public void GenerateNewVariants(int variantsAmount) {
            _variantsAmount = variantsAmount;
            _marixList = new HashSet<MatrixClass>();
            _variants = new HashSet<ExaminationVariant>();

            for (int i = 1; i < variantsAmount + 1; i++) {
                ExaminationVariant variant = new ExaminationVariant(i);

                variant.Text = CryptoHelper.RandomString(TextLength, _alphabet);
                variant.Key = new HillKey(GenerateUniqueIvertableMatrix());
                
                _variants.Add(variant);
            }
        }

        public bool Add(ExaminationVariant variant) {
            return _variants.Add(variant);
        }

        public void Remove(ExaminationVariant variant) {
            _variants.Remove(variant);
        }

        private int[,] GenerateUniqueIvertableMatrix() {
            int[,] arrInts = new int[_matrixSize, _matrixSize];
            Random rnd = new Random();

            while (true) {

                for (int i = 0; i < _matrixSize; i++) {
                    for (int j = 0; j < _matrixSize; j++) {
                        arrInts[i, j] = rnd.Next(GeneratedMinValue, GeneratedMaxValue);
                    }
                }

                try {
                    MatrixClass matrix = new MatrixClass(arrInts);
                    if (matrix.IsIvertable) {
                        if (!_marixList.Contains(matrix))
                        {
                            _marixList.Add(matrix);
                            break;
                        }
                        throw new Exception("Is not unique");
                    }
                    throw new Exception("Is not invertable");
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
            }

            return arrInts;
        }
    }
}