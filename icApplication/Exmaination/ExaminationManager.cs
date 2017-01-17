using System;
using System.Collections.Generic;
using System.Linq;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using icModel.Model.Keys;

namespace icApplication.Exmaination {
    class ExaminationManager {
        private Dictionary<int, ExaminationVariant> _variants;
        private HashSet<int[,]> _marixList;
        private IAlphabet _alphabet;
        private int _matrixSize;
        private int _generatedMaxValue;
        private int _generatedMinValue;
        private int _textLength;

        /// <summary>
        /// Set amount of generated variants
        /// </summary>
        /// <param name="variantsAmount"></param>
        public ExaminationManager(int variantsAmount) {
            InitalizeDefault();
            GenerateNewVariants(variantsAmount);
        }

        private void InitalizeDefault() {
            _marixList = new HashSet<int[,]>();
            _alphabet = new SimpleAlphabet();
            MatrixSize = 2;
            GeneratedMaxValue = _alphabet.Length;
            GeneratedMinValue = 0;
            TextLength = 4;
        }

        #region Properties

        public List<ExaminationVariant> VariantsList {
            get { return _variants.Values.ToList(); }
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

        #endregion


        private void GenerateNewVariants(int variantsAmount) {
            _marixList.Clear();
            Random rnd = new Random();
            Dictionary<int, ExaminationVariant> varList = new Dictionary<int, ExaminationVariant>();

            for (int i = 1; i < variantsAmount + 1; i++) {
                ExaminationVariant var = new ExaminationVariant(i) {
                    Text = CryptoHelper.RandomString(TextLength, _alphabet),
                    Key = new HillKey(GenerateUniqueIvertableMatrix())
                };
                varList.Add(i, var);
            }

            _variants = varList;
        }

        public void Add(ExaminationVariant variant) {
            _variants.Add(_variants.Count, variant);
        }

        public void Update(int number, ExaminationVariant variant) {
            _variants[number] = variant;
        }

        public void Remove(int number) {
            _variants.Remove(number);
        }

        public int[,] GenerateUniqueIvertableMatrix() {
            int[,] arrInts = new int[_matrixSize, _matrixSize];
            Random rnd = new Random();

            bool success = false;
            while (!success) {
                for (int i = 0; i < _matrixSize; i++) {
                    for (int j = 0; j < _matrixSize; j++) {
                        arrInts[i, j] = rnd.Next(GeneratedMinValue, GeneratedMaxValue);
                    }
                }
                MatrixClass matrix = new MatrixClass(arrInts);
                if (matrix.IsIvertable)
                    if (!_marixList.Contains(arrInts))
                        success = _marixList.Add(arrInts);
            }

            return arrInts;
        }
    }
}