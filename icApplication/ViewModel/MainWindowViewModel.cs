using icApplication.Helper;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using icApplication.Command;
using System.Windows;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Keys;
using icModel.Model.Providers;

namespace icApplication.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// </para>
    /// </summary>
    public partial class MainWindowViewModel : ViewModelBase
    {
        #region fields
        IAlphabet _alphabet;
        ICryptoKey _key;
        ICryptoProvider _provider;

        int? _selectedKey;
        string _encryptoText;
        string _decryptoText;
        ObservableCollection<int> _avaibleKeys;

        private bool _canExecute = true;
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class.
        /// </summary>
        public MainWindowViewModel()
        {
            InitializeCryptoComponents();
        }

        private void InitializeCryptoComponents()
        {
            _alphabet = new CharactersAlphabet();
            _avaibleKeys = new ObservableCollection<int>();

            EncryptCommand = new RelayCommand(EncryptMessage, CanEncrypt);
            DecryptCommand = new RelayCommand(DecryptMessage, CanDecrypt);

            GetAvaibleKeys();
            if (AvaibleKeys != null && AvaibleKeys.Count > 0)
                SelectedKey = AvaibleKeys[(int)(_avaibleKeys.Count / 2)];

            _provider = new AffineCipher(_alphabet, _key as AffineKey);
        }

        #region Properties
        public ObservableCollection<int> AvaibleKeys
        {
            get
            {
                return this._avaibleKeys;
            }
            set
            {
                this._avaibleKeys = value;
                base.NotifyPropertyChanged("AvaibleKeys");
            }
        }
        public string EncryptoText
        {
            get
            {
                return this._encryptoText;
            }
            set
            {
                this._encryptoText = value;
                base.NotifyPropertyChanged("EncryptoText");
            }
        }
        public string DecryptoText
        {
            get
            {
                return this._decryptoText;
            }
            set
            {
                this._decryptoText = value;
                base.NotifyPropertyChanged("DecryptoText");
            }
        }
        public int? SelectedKey
        {
            get
            {
                return this._selectedKey;
            }
            set
            {
                if (value == null)
                {
                    _key = null;
                    MessageBox.Show("Key can not be null!");
                }
                else
                {
                    _key = new AffineKey((int)value, _alphabet.Length);
                    _provider = new AffineCipher(_alphabet, (AffineKey) _key);
                    base.NotifyPropertyChanged("SelectedKey");
                }
                _selectedKey = value;
            }
        }
        #endregion

        #region Commands
        public bool CanExecute
        {
            get
            {
                return _canExecute;
            }

            set
            {
                if (_canExecute == value)
                {
                    return;
                }

                _canExecute = value;
            }
        }
        public ICommand EncryptCommand { get; set; }

        public ICommand DecryptCommand { get; set; }

        #endregion

        #region Private Methods
        public void EncryptMessage(object obj)
        {
            DecryptoText = ConvertToString(_provider.Encrypt(ConvertToStringArray(EncryptoText)));
            EncryptoText = null;
        }
        public void DecryptMessage(object obj)
        {
            EncryptoText = ConvertToString(_provider.Decrypt(ConvertToStringArray(DecryptoText)));
            DecryptoText = null;
        }
        public bool CanEncrypt(object obj)
        {
            return EncryptoText != null && (EncryptoText.Length > 0 && SelectedKey != null && _key != null);
        }
        public bool CanDecrypt(object obj)
        {
            return DecryptoText != null && (DecryptoText.Length > 0 && SelectedKey != null && _key != null);
        }
        private void GetAvaibleKeys()
        {
            for (var i = 0; i < 140; i++)
                if (OperationHelper.IsNod(i, _alphabet.Length))
                    AvaibleKeys.Add(i);
        }
        #endregion

        #region Converters
        public string ConvertToString(string[] text)
        {
            if (text.Length < 2)
                return text[0];
            else return string.Join(Environment.NewLine, text);
        }

        public string[] ConvertToStringArray(string text)
        {
            return text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }
        #endregion
    }

}
