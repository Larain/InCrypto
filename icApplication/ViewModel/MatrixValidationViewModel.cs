using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using icApplication.Command;
using icApplication.Exmaination;
using icApplication.ViewModel.Interface;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using icModel.Model.Keys;
using icModel.Model.Providers;

namespace icApplication.ViewModel {
    public class MatrixValidationViewModel : ViewModelBase, IMatrixValidationView {

        private int _matrixSize;
        private ObservableCollection<ObservableCollection<double>> _userMatrix;
        private string _message;
        private HillCipher _provider;
        private IAlphabet _alphabet;
        private ICryptoKey _key;
        private string _determinant;
        private string _determinantModule;
        private string _reciprocalValue;
        private ObservableCollection<ObservableCollection<double>> _adjugateMatrix;
        private ObservableCollection<ObservableCollection<double>> _decryptoMatrix;
        private ObservableCollection<ObservableCollection<double>> _observableMatrix;

        public MatrixValidationViewModel() {
            MatrixViewInitializtion();
        }

        private void MatrixViewInitializtion() {
            MatrixSize = 3;
            UserMatrix = FillMatrixWithZeros();

            SetMatrixCommand = new RelayCommand(SetMatrixAsKey, CanSetMatrixAsKey);
            FindInvertCommand = new RelayCommand(FindInvert, CanFindInvert);
            ValidateCommand = new RelayCommand(ValidateUserMatrix, CanValidateUserMatrix);
        }

        public RelayCommand SetMatrixCommand { get; set; }

        public RelayCommand FindInvertCommand { get; set; }

        public RelayCommand ValidateCommand { get; set; }

        #region Properties

        public ICryptoView MainView { get; set; }

        public int MatrixSize {
            get { return _matrixSize; }
            set {
                _matrixSize = value;
                base.NotifyPropertyChanged("MatrixSize");
                UserMatrix = FillMatrixWithZeros();
            }
        }

        public string Message {
            get { return _message; }
            set {
                _message = value;
                base.NotifyPropertyChanged("Message");
            }
        }

        public ObservableCollection<ObservableCollection<double>> UserMatrix {
            get { return _userMatrix; }
            set {
                _userMatrix = value;
                base.NotifyPropertyChanged("UserMatrix");
            }
        }

        #region Matrix Detail

        public HillCipher Provider {
            get { return _provider; }
            set
            {
                _provider = value;
                base.NotifyPropertyChanged("Provider");
            }
        }

        public string Determinant
        {
            get { return _determinant; }
            set
            {
                _determinant = value;
                base.NotifyPropertyChanged("Determinant");
            }
        }

        public string DeterminantModule
        {
            get { return _determinantModule; }
            set
            {
                _determinantModule = value;
                base.NotifyPropertyChanged("DeterminantModule");
            }
        }

        public string ReciprocalValue
        {
            get { return _reciprocalValue; }
            set
            {
                _reciprocalValue = value;
                base.NotifyPropertyChanged("ReciprocalValue");
            }
        }

        public ObservableCollection<ObservableCollection<double>> AdjugateMatrix
        {
            get { return _adjugateMatrix; }
            set
            {
                _adjugateMatrix = value;
                base.NotifyPropertyChanged("AdjugateMatrix");
            }
        }

        public ObservableCollection<ObservableCollection<double>> DecryptoMatrix
        {
            get { return _decryptoMatrix; }
            set
            {
                _decryptoMatrix = value;
                base.NotifyPropertyChanged("DecryptoMatrix");
            }
        }

        public ObservableCollection<ObservableCollection<double>> ObservableMatrix
        {
            get { return _observableMatrix; }
            set
            {
                _observableMatrix = value;
                base.NotifyPropertyChanged("ObservableMatrix");
            }
        }

        #endregion

        public ICryptoKey Key {
            get { return _key; }
            set {
                _key = value;
                Provider.Key = value;
            }
        }

        #endregion

        private ObservableCollection<ObservableCollection<double>> FillMatrixWithZeros() {
            ObservableCollection<ObservableCollection<double>> matrix =
                new ObservableCollection<ObservableCollection<double>>();

            for (int i = 0; i < MatrixSize; i++) {
                ObservableCollection<double> arr = new ObservableCollection<double>();
                for (int j = 0; j < MatrixSize; j++) {
                    arr.Add(0);
                }
                matrix.Add(arr);
            }
            return matrix;
        }

        #region Commands

        private void SetMatrixAsKey(object obj) {
            try {
                Key = new HillKey(UserMatrix, Provider.Key.Alphabet);
                MainView.SetCryptoKey(Key);
            }
            catch (ValidationException ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanSetMatrixAsKey(object obj) {
            bool same = true;
            for (int i = 0; i < MatrixSize; i++) {
                for (int j = 0; j < MatrixSize; j++) {
                    if (UserMatrix[i][j] != FillMatrixWithZeros()[i][j])
                        same = false;
                }
            }
            return !same;
        }

        private void ValidateUserMatrix(object obj) {
            try
            {
                Key = new HillKey(UserMatrix, Provider.Key.Alphabet);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private bool CanValidateUserMatrix(object obj)
        {
            return UserMatrix != null;
        }

        private void FindInvert(object obj) {
            try
            {
                Provider = new HillCipher();
                IAlphabet alphabet = new SimpleAlphabet();

                Key = new HillKey(UserMatrix, alphabet);
                Provider.Key = Key;
            }
            catch (ValidationException ex)
            {
                Provider = null;
                MessageBox.Show(ex.Message);
            }

        }

        private bool CanFindInvert(object obj)
        {
            return UserMatrix != null;
        }

        public void SetKey(HillKey hillKey)
        {
            Provider = new HillCipher();
            Provider.Key = hillKey;
            Key = hillKey;

            Determinant = Provider.Determinant.ToString();
            DeterminantModule = Provider.DeterminantModule.ToString();
            ReciprocalValue = Provider.ReciprocalValue.ToString();
            AdjugateMatrix = MatrixConverters.ConvertMatrixToObservableCollection(Provider.AdjugateMatrix);
            DecryptoMatrix = MatrixConverters.ConvertMatrixToObservableCollection(Provider.DecryptoMatrix);
            ObservableMatrix = Provider.Key.ObservableMatrix;
        }

        #endregion
    }
}