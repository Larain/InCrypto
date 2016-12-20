﻿using icModel.Model;
using System.Collections.Generic;
using icApplication.Helper;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using icApplication.Command;
using System.Windows;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.KeyGenerators;
using icModel.Model.Methods;
using icModel.Model.Providers;

namespace icApplication.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// </para>
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region fields
        IAlphabet _alphabet;
        ICryptoKey _key;
        CryptoMethod _method;
        ICryptoProvider _provider;

        int? selectedKey;
        string encryptoText;
        string decryptoText;
        ObservableCollection<int> avaibleKeys;

        private ICommand decryptCommand;
        private ICommand ecnryptCommand;

        private bool canExecute = true;
        #endregion

        /// <summary>
        /// Initializes a new instance of the MainWindowViewModel class.
        /// </summary>
        public MainWindowViewModel()
        {
            InitializeCryptoComponents();
            EncryptCommand = new RelayCommand(EncryptMessage, CanEncrypt);
            DecryptCommand = new RelayCommand(DecryptMessage, CanDecrypt);
            if (AvaibleKeys != null && AvaibleKeys.Count > 0)
                SelectedKey = AvaibleKeys[(int)(avaibleKeys.Count / 2)];
        }

        private void InitializeCryptoComponents()
        {
            _alphabet = new CharactersAlphabet();
            avaibleKeys = new ObservableCollection<int>();

            GetAvaibleKeys();
            _method = new AffineCipher(_alphabet, _key);
        }

        #region Properties
        public ObservableCollection<int> AvaibleKeys
        {
            get
            {
                return this.avaibleKeys;
            }
            set
            {
                this.avaibleKeys = value;
                base.NotifyPropertyChanged("AvaibleKeys");
            }
        }
        public string EncryptoText
        {
            get
            {
                return this.encryptoText;
            }
            set
            {
                this.encryptoText = value;
                base.NotifyPropertyChanged("EncryptoText");
            }
        }
        public string DecryptoText
        {
            get
            {
                return this.decryptoText;
            }
            set
            {
                this.decryptoText = value;
                base.NotifyPropertyChanged("DecryptoText");
            }
        }
        public int? SelectedKey
        {
            get
            {
                return this.selectedKey;
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
                    _method = new AffineCipher(_alphabet, _key);
                    _provider = new CryptoProvider(_key, _method);
                    base.NotifyPropertyChanged("SelectedKey");
                }
                selectedKey = value;
            }
        }
        #endregion

        #region Commands
        public bool CanExecute
        {
            get
            {
                return this.canExecute;
            }

            set
            {
                if (this.canExecute == value)
                {
                    return;
                }

                this.canExecute = value;
            }
        }
        public ICommand EncryptCommand
        {
            get
            {
                return ecnryptCommand;
            }
            set
            {
                ecnryptCommand = value;
            }
        }
        public ICommand DecryptCommand
        {
            get
            {
                return decryptCommand;
            }
            set
            {
                decryptCommand = value;
            }
        }
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
            return EncryptoText == null ? false : (EncryptoText.Length > 0 && SelectedKey != null && _key != null);
        }
        public bool CanDecrypt(object obj)
        {
            return DecryptoText == null ? false : (DecryptoText.Length > 0 && SelectedKey != null && _key != null);
        }
        private void GetAvaibleKeys()
        {
            for (int i = 0; i < 140; i++)
                if (NodHelper.IsNod(i, _alphabet.Length))
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
    }
    #endregion
}