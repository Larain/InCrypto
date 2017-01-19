using icApplication.Helper;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using icApplication.Command;
using System.Windows;
using icApplication.ViewModel.Interface;
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
    public class MainWindowViewModel : ViewModelBase, ICryptoView
    {
        #region fields

        private IAlphabet _alphabet;
        private ICryptoKey _key;
        private ExaminationVariant _examinationVariant;
        private ICryptoProvider _provider;
        private string _message;
        private string _encryptoText;
        private string _decryptoText;

        IExaminationView view;
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class.
        /// </summary>
        public MainWindowViewModel() {
            InitializeCryptoComponents();
            Message = "Select CryptoKey from matrix tab or examination";
        }

        private void InitializeCryptoComponents() {
            EncryptCommand = new RelayCommand(EncryptMessage, CanEncrypt);
            DecryptCommand = new RelayCommand(DecryptMessage, CanDecrypt);

            _provider = new HillCipher();
        }

        #region Properties

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

        public ICryptoKey SelectedKey {
            get { return this._key; }
            set {
                _key = value;
                _provider.Key = _key;
                base.NotifyPropertyChanged("SelectedKey");
            }
        }

        public string Message
        {
            get { return _message; }
            set {
                _message = value;
                base.NotifyPropertyChanged("Message");
            }
        }

        public ExaminationVariant ExaminationVariant
        {
            get { return _examinationVariant; }
            set {
                _examinationVariant = value;
                base.NotifyPropertyChanged("ExaminationVariant");
            }
        }

        public ICommand EncryptCommand { get; set; }

        public ICommand DecryptCommand { get; set; }

        public IExaminationView ExaminationView {
            get { return view; }
            set { view = value; }
        }

        #endregion

        #region Command Methods

        public void EncryptMessage(object obj) {
            try {
                DecryptoText = ConvertToString(_provider.Encrypt(ConvertToStringArray(EncryptoText)));
            }
            catch (CipherException e) {
                MessageBox.Show(e.Message + "\nKey: " + e.Key, "Crypto Key error", MessageBoxButton.OK,
                    MessageBoxImage.Stop);
            }
            
            EncryptoText = null;
        }

        public void DecryptMessage(object obj) {
            try {
                EncryptoText = ConvertToString(_provider.Decrypt(ConvertToStringArray(DecryptoText)));
            }
            catch (CipherException e) {
                MessageBox.Show(e.Message + "\nKey: " + e.Key, "Crypto Key error", MessageBoxButton.OK,
                    MessageBoxImage.Stop);
            }
            DecryptoText = null;
        }

        public bool CanEncrypt(object obj) {
            return EncryptoText != null && (EncryptoText.Length > 0 && SelectedKey != null && _key != null);
        }

        public bool CanDecrypt(object obj) {
            return DecryptoText != null && (DecryptoText.Length > 0 && SelectedKey != null && _key != null);
        }

        #endregion

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

        public void SetCryptoKey(ICryptoKey key) {
            SelectedKey = key;
        }

        public void SetEncryptoText(string text) {
            EncryptoText = text;
        }

        public void SendMessage(string text) {
            Message = text;
        }
    }
}