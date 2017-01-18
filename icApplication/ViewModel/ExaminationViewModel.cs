using System.Collections.Generic;
using System.Collections.ObjectModel;
using icApplication.Command;
using icApplication.Exmaination;
using icApplication.ViewModel.Interface;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;

namespace icApplication.ViewModel {
    public class ExaminationViewModel : ViewModelBase, IExaminationView {

        private int _generatedMatrixSize;
        private int _generatedTextLength;
        private int _variantAmount;
        private IAlphabet _alphabet;

        private ObservableCollection<ExaminationVariant> _examVariants;
        private ExaminationVariant _examinationVariant;
        private readonly ExaminationManager _examinationManager = new ExaminationManager();

        public ExaminationViewModel() {
            GeneratedMatrixSize = 2;
            GeneratedTextLength = GeneratedMatrixSize*2;
            VariantAmount = 20;
            Alphabet = new SimpleAlphabet();

            _examinationManager.GenerateNewVariants(VariantAmount);

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

        public ObservableCollection<ExaminationVariant> ExaminationVariantCollection {
            get { return _examVariants; }
            set {
                _examVariants = value;
                base.NotifyPropertyChanged("ExaminationVariantCollection");
            }
        }

        public int GeneratedMatrixSize {
            get { return _generatedMatrixSize; }
            set {
                _generatedMatrixSize = value;
                _examinationManager.MatrixSize = value;
                base.NotifyPropertyChanged("GeneratedMatrixSize");
            }
        }

        public int VariantAmount {
            get { return _variantAmount; }
            set {
                _variantAmount = value;
                _examinationManager.VariantsAmount = value;
                base.NotifyPropertyChanged("VariantAmount");
            }
        }

        public ICryptoView MainView { get; set; }

        public int MatrixMaxGeneratedValue {
            get { return _examinationManager.GeneratedMaxValue; }
        }

        public int MatrixMinGeneratedValue
        {
            get { return _examinationManager.GeneratedMinValue; }
        }

        public int GeneratedTextLength {
            get { return _generatedTextLength; }
            set {
                _generatedTextLength = value;
                _examinationManager.TextLength = value;
                base.NotifyPropertyChanged("GeneratedTextLength");
            }
        }

        public IAlphabet Alphabet {
            get { return _alphabet; }
            set {
                _alphabet = value;
                _examinationManager.Alphabet = value;
                base.NotifyPropertyChanged("Alphabet");
            }
        }

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

        public void SetAlphabet(IAlphabet alphabet) {
            throw new System.NotImplementedException();
        }
    }
}