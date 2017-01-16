using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using icApplication.Command;
using icApplication.Exmaination;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;
using icModel.Model.Keys;

namespace icApplication.ViewModel
{
    public partial class MatrixValidationViewModel : ViewModelBase
    {

        private int _matrixSize;
        private int _variantAmount;
        private int[][] _userMatrix;
        private string _message;

        private List<ExaminationVariant> _examVariants;
        private ExaminationVariant _examinationVariant;
        private ExaminationManager _examinationManager;

        public MatrixValidationViewModel()
        {
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

        public ExaminationVariant SelectedExaminationVariant
        {
            get { return _examinationVariant; }
            set
            {
                _examinationVariant = value;
                base.NotifyPropertyChanged("SelectedExaminationVariant");
            }
        }

        public List<ExaminationVariant> ExaminationVariantCollection
        {
            get { return _examVariants ?? (_examVariants = _examinationManager.VariantsList); }
            set
            {
                _examVariants = value;
                base.NotifyPropertyChanged("ExaminationVariantCollection");
            }
        }

        public int MatrixSize
        {
            get { return _matrixSize; }
            set
            {
                _matrixSize = value;
                base.NotifyPropertyChanged("MatrixSize");
                FillMatrixWithZeros();
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                base.NotifyPropertyChanged("Message");
            }
        }

        public int[][] UserMatrix
        {
            get { return _userMatrix; }
            set
            {
                _userMatrix = value;
                base.NotifyPropertyChanged("UserMatrix");
            }
        }

        public int VariantAmount
        {
            get { return _variantAmount; }
            set
            {
                _variantAmount = value;
                base.NotifyPropertyChanged("VariantAmount");
            }
        }

        #endregion

        private void FillMatrixWithZeros()
        {
            int[][] matrix = new int[MatrixSize][];
            for (int i = 0; i < MatrixSize; i++)
            {
                int[] arr = new int[MatrixSize];
                for (int j = 0; j < MatrixSize; j++)
                {
                    arr[j] = 0;
                }
                matrix[i] = arr;
            }
            UserMatrix = matrix;
        }

        #region Commands

        private void CreateVariants(object obj)
        {
            _examinationManager = new ExaminationManager(VariantAmount);
            ExaminationVariantCollection = _examinationManager.VariantsList;
        }

        private bool CanCreateVariants(object obj)
        {
            return VariantAmount > 0 && VariantAmount < 100;
        }

        private bool CanValidateUserMatrix(object obj)
        {
            throw new NotImplementedException();
        }

        private void ValidateUserMatrix(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanFindInvert(object obj)
        {
            throw new NotImplementedException();
        }

        private void FindInvert(object obj)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}