using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using icModel.Model.Keys;

namespace icApplication.Exmaination
{
    class ExaminationManager
    {
        private HashSet<ExaminationVariant> _variants;
        private HashSet<MatrixClass> _marixList;
        private IAlphabet _alphabet;
        private int _matrixSize;
        private int _generatedMaxValue;
        private int _generatedMinValue;
        private int _textLength;
        private int _variantsAmount;
        private static Random _random = new Random();

        public ExaminationManager()
        {
            InitalizeDefault();
            GenerateNewVariants(_variantsAmount);
        }

        private void InitalizeDefault()
        {
            _marixList = new HashSet<MatrixClass>();
            _variants = new HashSet<ExaminationVariant>();
            _alphabet = new SimpleAlphabet();

            VariantsAmount = 20;
            MatrixSize = 2;
            GeneratedMaxValue = _alphabet.Length;
            GeneratedMinValue = 0;
            TextLength = 4;
        }

        #region Properties

        public List<ExaminationVariant> VariantsList
        {
            get { return _variants?.ToList(); }
        }

        public int MatrixSize
        {
            get { return _matrixSize; }
            set { _matrixSize = value; }
        }

        public int GeneratedMaxValue
        {
            get { return _generatedMaxValue; }
            set { _generatedMaxValue = value; }
        }

        public int GeneratedMinValue
        {
            get { return _generatedMinValue; }
            set { _generatedMinValue = value; }
        }

        public int TextLength
        {
            get { return _textLength; }
            set { _textLength = value; }
        }

        public int VariantsAmount
        {
            get { return _variantsAmount; }
            set { _variantsAmount = value; }
        }

        #endregion


        public void GenerateNewVariants(int variantsAmount)
        {
            _variantsAmount = variantsAmount;
            _marixList = new HashSet<MatrixClass>();
            _variants = new HashSet<ExaminationVariant>();

            for (int i = 1; i < variantsAmount + 1; i++)
            {
                ExaminationVariant variant = new ExaminationVariant(i);

                variant.Text = CryptoHelper.RandomString(TextLength, _alphabet);
                variant.Key = new HillKey(GenerateUniqueIvertableMatrix());

                _variants.Add(variant);
            }
        }

        public bool Add(ExaminationVariant variant)
        {
            return _variants.Add(variant);
        }

        public void Remove(ExaminationVariant variant)
        {
            _variants.Remove(variant);
        }

        private int[,] GenerateUniqueIvertableMatrix()
        {
            int operationCounter = 0;
            Random rnd = new Random();
            int[,] arrInts = new int[_matrixSize, _matrixSize];

            while (true)
            {
                operationCounter++;
                for (int i = 0; i < _matrixSize; i++)
                {
                    for (int j = 0; j < _matrixSize; j++)
                    {
                        while (true)
                        {
                            int randomValue = rnd.Next(GeneratedMinValue, GeneratedMaxValue);
                            if (CryptoHelper.IsNod(randomValue, GeneratedMaxValue))
                            {
                                arrInts[i, j] = randomValue;
                                break;
                            }
                        }
                    }
                }

                MatrixClass newMatrix = new MatrixClass(arrInts);
                if (newMatrix.IsIvertable)
                {
                    if (_marixList.Add(newMatrix))
                        return arrInts;
                }
            }
        }
    }
}