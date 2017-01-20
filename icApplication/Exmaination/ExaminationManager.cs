using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Documents;
using icApplication.ViewModel;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using icModel.Model.Keys;
using icModel.Model.Providers;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace icApplication.Exmaination {
    class ExaminationManager : ViewModelBase {
        private const int GENERATED_TEXT_MIN_VALUE = 1;

        private ObservableCollection<ExaminationVariant> _variants;
        private HashSet<HillKey> _marixList;
        private Alphabet _alphabet;
        private int _matrixSize;
        private int _textLength;
        private int _variantsAmount;
        private static Random _random = new Random();

        public ExaminationManager() {
            InitalizeDefault();
        }

        private void InitalizeDefault() {
            _marixList = new HashSet<HillKey>();
            VariantsList = new ObservableCollection<ExaminationVariant>();
        }

        #region Properties

        public ObservableCollection<ExaminationVariant> VariantsList {
            get { return _variants; }
            private set {
                _variants = value;
                base.NotifyPropertyChanged("VariantsList");
            }
        }

        public Alphabet Alphabet
        {
            get { return _alphabet; }
            set { _alphabet = value; }
        }

        public int MatrixSize {
            get { return _matrixSize; }
            set { _matrixSize = value; }
        }

        public int GeneratedMaxValue {
            get { return Alphabet.Length; }
        }

        public int GeneratedMinValue
        {
            get { return GENERATED_TEXT_MIN_VALUE; }
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
            _marixList.Clear();
            VariantsList.Clear();

            for (int i = 1; i < variantsAmount + 1; i++) {

                ExaminationVariant variant = new ExaminationVariant
                {
                    Number = i,
                    Text = CryptoHelper.RandomString(TextLength, _alphabet),
                    Key = GenerateUniqueIvertableMatrix()
                };

                VariantsList.Add(variant);
            }
        }

        private HillKey GenerateUniqueIvertableMatrix() {
            int operationCounter = 0;
            double[,] arrDoubles = new double[MatrixSize, MatrixSize];

            while (true) {
                operationCounter++;
                for (int i = 0; i < MatrixSize; i++)
                    for (int j = 0; j < MatrixSize; j++)
                        arrDoubles[i, j] = _random.Next(GENERATED_TEXT_MIN_VALUE, GeneratedMaxValue);

                try {
                    HillKey key = new HillKey(arrDoubles, _alphabet);
                    HillCipher p = new HillCipher {Key = key};
                    string[] originalText = new string[1] { CryptoHelper.RandomString(MatrixSize * 2, _alphabet)};
                    string[] encryptedText = p.Encrypt(originalText);
                    string[] decryptedText = p.Decrypt(encryptedText);

                    if (_marixList.Add(key) && originalText[0] == decryptedText[0])
                        return key;
                }
                catch (ValidationException ex) {
                    MessageBox.Show(ex.Message);
                }
                catch (ArgumentException ex) {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}