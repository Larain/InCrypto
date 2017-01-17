using System.Collections.Generic;
using icApplication.Command;
using icApplication.Exmaination;
using icApplication.ViewModel.Interface;
using icModel.Model.Entities;

namespace icApplication.ViewModel {
    public class ExaminationViewModel : ViewModelBase {

        private int _matrixSize;
        private int _variantAmount;
        private int[][] _userMatrix;

        private List<ExaminationVariant> _examVariants;
        private ExaminationVariant _examinationVariant;
        private ExaminationManager _examinationManager;

        public ExaminationViewModel() {
            _examinationManager = new ExaminationManager();
            MatrixSize = 3;
            VariantAmount = 20;
            //CreateVariants(null);

            GenerateVariantsCommand = new RelayCommand(CreateVariants, CanCreateVariants);
        }

        public RelayCommand GenerateVariantsCommand { get; set; }

        #region Properties

        public ExaminationVariant SelectedExaminationVariant {
            get { return _examinationVariant; }
            set {
                _examinationVariant = value;
                if (MainView != null) {
                    MainView.SetCryptoKey(value.Key);
                    MainView.SendMessage("Key used from examination variant #" + value.Number);
                    MainView.SetEncryptoText(value.Text);
                }

                base.NotifyPropertyChanged("SelectedExaminationVariant");
            }
        }

        public List<ExaminationVariant> ExaminationVariantCollection {
            get { return _examVariants ?? (_examVariants = _examinationManager.VariantsList); }
            set {
                _examVariants = value;
                base.NotifyPropertyChanged("ExaminationVariantCollection");
            }
        }

        public int MatrixSize {
            get { return _matrixSize; }
            set {
                _matrixSize = value;
                base.NotifyPropertyChanged("MatrixSize");
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

        public ICryptoView MainView { get; set; }

        #endregion

        #region Commands

        private void CreateVariants(object obj) {
            _examinationManager.GenerateNewVariants(VariantAmount);
            ExaminationVariantCollection = _examinationManager.VariantsList;
        }

        private bool CanCreateVariants(object obj) {
            return VariantAmount > 0 && VariantAmount < 100;
        }

        #endregion


    }
}