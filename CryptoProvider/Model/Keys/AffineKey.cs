using System;
using icModel.Abstract;

namespace icModel.Model.Keys {
    public class AffineKey : ICryptoKey
    {
        private int[,] _keyCodes;
        private ICryptoKeyValidator _validator;

        public AffineKey(int a, int b)
        {
            KeyCodes = GenerateNewKey(a, b);
        }

        #region Properties

        public int[,] KeyCodes
        {
            get { return _keyCodes; }
            set
            {
                if (Validator.IsValid(value))
                    _keyCodes = value;
                else 
                    throw new ArgumentException("Key is not valid");
            }
            
        }

        public ICryptoKeyValidator Validator
        {
            get { return _validator ?? (_validator = new AffineKeyValidator()); }
        }

        #endregion

        #region Methods

        public override string ToString() {
            string output = "";
            for (int i = 0; i < KeyCodes.Length; i++) {
                for (int j = 0; j < KeyCodes.GetLength(0); j++) {
                    output += "[" + KeyCodes[i, j] + "] ";
                }
                output += "\n";
            }
            return output;
        }

        private int[,] GenerateNewKey(int a, int b) {
            return new int[1, 2] { { a, b } };
        }

        #endregion
    }
}