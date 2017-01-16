using icApplication.Helper;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using icApplication.Command;
using System.Windows;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using icModel.Model.Keys;
using icModel.Model.Providers;

namespace icApplication.ViewModel {
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// </para>
    /// </summary>
    public class MainWindowViewModel : ViewModelBase {
        #region fields

        private IAlphabet _alphabet;
        private ICryptoKey _key;
        private ExaminationVariant _examinationVariant;
        private ICryptoProvider _provider;
        private int _selectedKey;
        private int _examVariantsAmount;
        private string _encryptoText;
        private string _decryptoText;
        private ObservableCollection<int> _avaibleKeys;

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class.
        /// </summary>
        public MainWindowViewModel() {
            InitializeCryptoComponents();
        }

        private void InitializeCryptoComponents() {
            _alphabet = new CharactersAlphabet();
            _avaibleKeys = new ObservableCollection<int>();

            EncryptCommand = new RelayCommand(EncryptMessage, CanEncrypt);
            DecryptCommand = new RelayCommand(DecryptMessage, CanDecrypt);

            GetAvaibleKeys();
            if (AvaibleExamVariants != null && AvaibleExamVariants.Count > 0)
                SelectedKey = AvaibleExamVariants[(int) (_avaibleKeys.Count/2)];

            _provider = new AffineCipher(_alphabet, _key as AffineKey);
        }

        #region Properties

        public ObservableCollection<int> AvaibleExamVariants
        {
            get { return this._avaibleKeys; }
            set {
                this._avaibleKeys = value;
                base.NotifyPropertyChanged("AvaibleExamVariants");
            }
        }

        public string EncryptoText {
            get { return this._encryptoText; }
            set {
                this._encryptoText = value;
                base.NotifyPropertyChanged("EncryptoText");
            }
        }

        public string DecryptoText {
            get { return this._decryptoText; }
            set {
                this._decryptoText = value;
                base.NotifyPropertyChanged("DecryptoText");
            }
        }

        public int SelectedKey {
            get { return this._selectedKey; }
            set {
                _key = new AffineKey(value, _alphabet.Length);
                _provider = new AffineCipher(_alphabet, (AffineKey) _key);
                base.NotifyPropertyChanged("SelectedKey");

                _selectedKey = value;
            }
        }

        public int ExamVariantsAmount
        {
            get { return _examVariantsAmount; }
            set {
                _examVariantsAmount = value;
                GetAvaibleKeys();
                base.NotifyPropertyChanged("ExamVariantsAmount");
            }
        }

        public ExaminationVariant ExaminationVariant1
        {
            get { return _examinationVariant; }
            set { _examinationVariant = value; }
        }

        public ICommand EncryptCommand { get; set; }

        public ICommand DecryptCommand { get; set; }

        #endregion

        #region Command Methods

        public void EncryptMessage(object obj) {
            DecryptoText = ConvertToString(_provider.Encrypt(ConvertToStringArray(EncryptoText)));
            EncryptoText = null;
        }

        public void DecryptMessage(object obj) {
            EncryptoText = ConvertToString(_provider.Decrypt(ConvertToStringArray(DecryptoText)));
            DecryptoText = null;
        }

        public bool CanEncrypt(object obj) {
            return EncryptoText != null && (EncryptoText.Length > 0 && SelectedKey != null && _key != null);
        }

        public bool CanDecrypt(object obj) {
            return DecryptoText != null && (DecryptoText.Length > 0 && SelectedKey != null && _key != null);
        }

        #endregion

        private void GetAvaibleKeys()
        {
            AvaibleExamVariants.Clear();
            for (var i = 0; i < ExamVariantsAmount; i++)
                AvaibleExamVariants.Add(i);
        }

        #region Converters

        public string ConvertToString(string[] text) {
            if (text.Length < 2)
                return text[0];
            else return string.Join(Environment.NewLine, text);
        }

        public string[] ConvertToStringArray(string text) {
            return text.Split(new[] {'\r', '\n'}, StringSplitOptions.RemoveEmptyEntries);
        }

        #endregion
    }
}