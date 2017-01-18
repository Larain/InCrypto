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
using icModel.Model.Keys;
using icModel.Model.Providers;

namespace icApplication.ViewModel {
    public class MatrixValidationViewModel : ViewModelBase {

        private int _matrixSize;
        private ObservableCollection<ObservableCollection<double>> _userMatrix;
        private string _message;
        private ICryptoProvider _provider;
        private IAlphabet _alphabet;
        private ICryptoKey _key;

        public MatrixValidationViewModel() {
            MatrixViewInitializtion();
        }

        private void MatrixViewInitializtion() {
            MatrixSize = 3;
            UserMatrix = FillMatrixWithZeros();

            Provider = new HillCipher();
            Alphabet = new SimpleAlphabet();
            Provider.Alphabet = Alphabet;

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

        public ICryptoProvider Provider {
            get { return _provider; }
            set { _provider = value; }
        }

        public IAlphabet Alphabet {
            get { return _alphabet; }
            set {
                _alphabet = value;
                Provider.Alphabet = value;
            }
        }

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
                Key = new HillKey(UserMatrix, Provider.Alphabet);
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
                Key = new HillKey(UserMatrix, Provider.Alphabet);
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
                Key = new HillKey(UserMatrix, Provider.Alphabet);
                Provider.Key = Key;
                Provider.Alphabet = Alphabet;
            }
            catch (ValidationException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private bool CanFindInvert(object obj)
        {
            return UserMatrix != null;
        }

        #endregion
    }
}