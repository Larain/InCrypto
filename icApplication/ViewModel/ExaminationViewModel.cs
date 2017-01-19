using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using icApplication.Command;
using icApplication.Exmaination;
using icApplication.Helper;
using icApplication.ViewModel.Interface;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;
using icModel.Model.Keys;
using icModel.Model.Providers;

namespace icApplication.ViewModel
{
    public class ExaminationViewModel : ViewModelBase, IExaminationView
    {

        private int _generatedMatrixSize;
        private int _generatedTextLength;
        private int _variantAmount;
        private Alphabet _alphabet;

        public List<Alphabet> _alphavetList;
        private ObservableCollection<ExaminationVariant> _examVariants;
        private ExaminationVariant _examinationVariant;
        private readonly ExaminationManager _examinationManager = new ExaminationManager();

        public ExaminationViewModel()
        {
            GeneratedMatrixSize = 2;
            GeneratedTextLength = GeneratedMatrixSize * 2;
            VariantAmount = 20;
            AlphavetList = new List<Alphabet> {new SimpleAlphabet(), new CharactersAlphabet(), new RusExtendedAlphabet()};
            Alphabet = AlphavetList.FirstOrDefault();

            CreateVariants(null);
            SelectedExaminationVariant = ExaminationVariantCollection.First();

            GenerateVariantsCommand = new RelayCommand(CreateVariants, CanCreateVariants);
            LoadVariantsCommand = new RelayCommand(LoadVariants, CanLoadVariants);
            SaveVariantsCommand = new RelayCommand(SaveVariants, CanSaveVariants);
        }

        public RelayCommand GenerateVariantsCommand { get; set; }
        public RelayCommand LoadVariantsCommand { get; set; }
        public RelayCommand SaveVariantsCommand { get; set; }

        #region Properties

        public ExaminationVariant SelectedExaminationVariant
        {
            get { return _examinationVariant; }
            set
            {
                if (value == null)
                    return;
                _examinationVariant = value;
                if (MainView != null)
                {
                    MatrixView.SetKey((HillKey) value.Key);
                    MainView.SetCryptoKey(value.Key);
                    MainView.SendMessage("Key used from examination variant #" + value.Number);
                    MainView.SetEncryptoText(value.Text);
                }

                base.NotifyPropertyChanged("SelectedExaminationVariant");
            }
        }

        public ObservableCollection<ExaminationVariant> ExaminationVariantCollection
        {
            get { return _examVariants; }
            set
            {
                _examVariants = value;
                base.NotifyPropertyChanged("ExaminationVariantCollection");
            }
        }

        public int GeneratedMatrixSize
        {
            get { return _generatedMatrixSize; }
            set
            {
                _generatedMatrixSize = value;
                _examinationManager.MatrixSize = value;
                base.NotifyPropertyChanged("GeneratedMatrixSize");
            }
        }

        public int VariantAmount
        {
            get { return _variantAmount; }
            set
            {
                _variantAmount = value;
                _examinationManager.VariantsAmount = value;
                base.NotifyPropertyChanged("VariantAmount");
            }
        }

        public ICryptoView MainView { get; set; }
        public IMatrixValidationView MatrixView { get; set; }

        public int MatrixMaxGeneratedValue
        {
            get { return _examinationManager.GeneratedMaxValue; }
        }

        public int MatrixMinGeneratedValue
        {
            get { return _examinationManager.GeneratedMinValue; }
        }

        public int GeneratedTextLength
        {
            get { return _generatedTextLength; }
            set
            {
                _generatedTextLength = value;
                _examinationManager.TextLength = value;
                base.NotifyPropertyChanged("GeneratedTextLength");
            }
        }

        public Alphabet Alphabet
        {
            get { return _alphabet; }
            set
            {
                _alphabet = value;
                _examinationManager.Alphabet = value;
                base.NotifyPropertyChanged("Alphabet");
            }
        }

        public List<Alphabet> AlphavetList
        {
            get
            {
                return _alphavetList;
            }
            set
            {
                _alphavetList = value;
                base.NotifyPropertyChanged("AlphavetList");
            }
        }

        #endregion

        #region Commands

        private void SaveVariants(object obj)
        {
            string title = "";
            string message = "";
            try
            {
                string path = Serializer.OpenDirectoryDialog();
                Serializer.XmlSerialization(ExaminationVariantCollection.ToList(), path);
                message = "Variants loaded in amount: " + ExaminationVariantCollection.Count + "loaded successfully";
                title = "Success";
            }
            catch (Exception e)
            {
                message = e.Message;
                title = "Fail";
            }

            MessageBox.Show(message, title);
        }
        private bool CanSaveVariants(object obj)
        {
            return ExaminationVariantCollection?.Count > 0;
        }

        private void LoadVariants(object obj)
        {
            string title = "";
            string message = "";
            try
            {
                string path = Serializer.OpenFileDialog();
                ExaminationVariantCollection =
                new ObservableCollection<ExaminationVariant>(Serializer.XmlDeserialization(path));
                message = "Variants loaded in amount: " + ExaminationVariantCollection.Count + "loaded successfully";
                title = "Success";
            }
            catch (Exception e)
            {
                message = e.Message;
                title = "Fail";
            }
            
            MessageBox.Show(message, title);
        }
        private bool CanLoadVariants(object obj)
        {
            return true;
        }

        private void CreateVariants(object obj)
        {
            _examinationManager.GenerateNewVariants(VariantAmount);
            ExaminationVariantCollection = _examinationManager.VariantsList;
        }
        private bool CanCreateVariants(object obj)
        {
            bool amount = VariantAmount > 0 && VariantAmount < 100;
            bool size = GeneratedMatrixSize > 1 && GeneratedMatrixSize < 10;
            bool len = GeneratedTextLength > 0 && GeneratedTextLength < 100;
            return amount && size && len;
        }

        #endregion

        public void SetAlphabet(Alphabet alphabet)
        {
            Alphabet = alphabet;
        }
    }
}