using System;
using System.Collections.ObjectModel;
using icModel.Abstract;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using icModel.Model.Providers;

namespace icModel.Model.Keys
{
    public class HillKey : ICryptoKey {
        private int[,] _keyArray;
        private ObservableCollection<ObservableCollection<int>> _keyCodes;
        private ICryptoKeyValidator _validator;

        public HillKey(int[,] keyInts)
        {
            GenerateNewKey(keyInts);
        }
        public HillKey(int[][] keyInts)
        {
            GenerateNewKey(keyInts);
        }

        #region Properties

        public ObservableCollection<ObservableCollection<int>> KeyCodes
        {
            get { return _keyCodes; }
            set
            {
                if (Validator.IsValid(value))
                    _keyCodes = value;
                else
                    throw new ArgumentException($"HillKey is not valid.");
            }

        }

        public int[,] KeyArray
        {
            get { return _keyArray ?? (_keyArray = _keyCodes != null ? this.ToIntArray() : null); }
            private set { _keyArray = value; }
        }

        public ICryptoKeyValidator Validator
        {
            get { return _validator ?? (_validator = new HillKeyValidator()); }
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            string output = "";
            for (int i = 0; i < KeyCodes.Count; i++)
            {
                for (int j = 0; j < KeyCodes[0].Count; j++)
                {
                    output += "[" + KeyCodes[i][j] + "] ";
                }
                output += "\n";
            }
            return output;
        }

        private void GenerateNewKey(int[][] keyInts)
        {
            if (!Validator.IsValid(keyInts))
                throw new CipherException("Invalid matrix");

            ObservableCollection<ObservableCollection<int>> outer =
                new ObservableCollection<ObservableCollection<int>>();

            for (int i = 0; i < keyInts.Length; i++)
            {
                ObservableCollection<int> inner = new ObservableCollection<int>();

                for (int j = 0; j < keyInts.Length; j++)
                {
                    inner.Add(keyInts[i][j]);
                }

                outer.Add(inner);
            }

            _keyCodes = outer;
        }

        private void GenerateNewKey(int[,] keyInts) {

            if (!Validator.IsValid(keyInts))
                throw new CipherException("Invalid matrix");

            ObservableCollection<ObservableCollection<int>> outer =
                new ObservableCollection<ObservableCollection<int>>();

            for (int i = 0; i < keyInts.GetLength(0); i++) {

                ObservableCollection<int> inner = new ObservableCollection<int>();

                for (int j = 0; j < keyInts.GetLength(1); j++) {
                    inner.Add(keyInts[i,j]);
                }

                outer.Add(inner);
            }

            _keyCodes = outer;
        }

        #endregion
    }
}