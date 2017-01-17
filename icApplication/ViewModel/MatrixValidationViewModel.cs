using System;
using System.Collections.Generic;
using System.Windows;
using icApplication.Command;
using icApplication.Exmaination;
using icApplication.ViewModel.Interface;
using icModel.Abstract;
using icModel.Model.Entities;

namespace icApplication.ViewModel {
    public class MatrixValidationViewModel : ViewModelBase {

        private int _matrixSize;
        private int _variantAmount;
        private int[][] _userMatrix;
        private string _message;
        ICryptoKeyValidator _validator;

        private List<ExaminationVariant> _examVariants;
        private ExaminationVariant _examinationVariant;

        public MatrixValidationViewModel() {
            MatrixSize = 3;
            VariantAmount = 20;
            CreateVariants(null);

            GenerateVariantsCommand = new RelayCommand(CreateVariants, CanCreateVariants);
            FindInvertCommand = new RelayCommand(FindInvert, CanFindInvert);
            ValidateCommand = new RelayCommand(ValidateUserMatrix, CanValidateUserMatrix);
        }


        public RelayCommand ValidateCommand { get; set; }

        public RelayCommand FindInvertCommand { get; set; }

        public RelayCommand GenerateVariantsCommand { get; set; }

        #region Properties

        public ICryptoView MainView { get; set; }

        public ExaminationVariant SelectedExaminationVariant {
            get { return _examinationVariant; }
            set {
                _examinationVariant = value;
                base.NotifyPropertyChanged("SelectedExaminationVariant");
            }
        }

        public int MatrixSize {
            get { return _matrixSize; }
            set {
                _matrixSize = value;
                base.NotifyPropertyChanged("MatrixSize");
                FillMatrixWithZeros();
            }
        }

        public string Message {
            get { return _message; }
            set {
                _message = value;
                base.NotifyPropertyChanged("Message");
            }
        }

        public int[][] UserMatrix {
            get { return _userMatrix; }
            set {
                _userMatrix = value;
                base.NotifyPropertyChanged("UserMatrix");
            }
        }

        public int VariantAmount {
            get { return _variantAmount; }
            set {
                _variantAmount = value;
                base.NotifyPropertyChanged("VariantAmount");
            }
        }

        public ICryptoKeyValidator Validator {
            get { return _validator; }

            set {
                _validator = value;
                base.NotifyPropertyChanged("Validator");
            }
        }

        #endregion

        private void FillMatrixWithZeros() {
            int[][] matrix = new int[MatrixSize][];
            for (int i = 0; i < MatrixSize; i++) {
                int[] arr = new int[MatrixSize];
                for (int j = 0; j < MatrixSize; j++) {
                    arr[j] = 0;
                }
                matrix[i] = arr;
            }
            UserMatrix = matrix;
        }

        #region Commands

        private void CreateVariants(object obj) {
        }

        private bool CanCreateVariants(object obj) {
            return VariantAmount > 0 && VariantAmount < 100;
        }

        private bool CanValidateUserMatrix(object obj) {
            return UserMatrix != null;
        }

        private void ValidateUserMatrix(object obj) {
            if (_validator.IsValid(UserMatrix))
                MessageBox.Show("Matrix is valid.", "Success", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            else {
                MessageBox.Show("Invalid matrix.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanFindInvert(object obj) {
            throw new NotImplementedException();
        }

        private void FindInvert(object obj) {
            throw new NotImplementedException();
        }

        #endregion
    }
}