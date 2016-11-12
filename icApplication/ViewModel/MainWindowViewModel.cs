using icModel.Alphabet;
using icModel.Key;
using icModel.Model;
using icModel.Method;
using icModel.Provider;
using System.Collections.Generic;
using icApplication.Helper;
using System;
using System.Collections.ObjectModel;

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
        ICryptoMethod _method;
        ICryptoProvider _provider;

        int selectedKey;
        string encryptoText;
        string decryptoText;
        ObservableCollection<int> avaibleKeys;
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
            _method = new AffineCipher(_alphabet);
            avaibleKeys = new ObservableCollection<int>();

            GetAvaibleKeys();
            if (AvaibleKeys != null && AvaibleKeys.Count > 0)
                SelectedKey = AvaibleKeys[0];
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
        public string EcnryptoText
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
        public int SelectedKey
        {
            get
            {
                return this.selectedKey;
            }
            set
            {
                this.selectedKey = value;
                _key = new AffineKey(value, _alphabet.Length);
                base.NotifyPropertyChanged("SelectedKey");
            }
        }
        #endregion

        #region Private Methods
        private void GetAvaibleKeys()
        {
            for (int i = 0; i < 200; i++)
                if (NodHelper.IsNod(i, _alphabet.Length))
                    AvaibleKeys.Add(i);
        }
        #endregion
    }
}