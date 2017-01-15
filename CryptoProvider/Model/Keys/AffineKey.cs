using System;
using System.Collections.Generic;
using icModel.Abstract;

namespace icModel.Model.Keys {
    public class AffineKey : ICryptoKey
    {
        private List<List<int>> _keyCodes;
        private ICryptoKeyValidator _validator;

        public AffineKey(int a, int b)
        {
            KeyCodes = GenerateNewKey(a, b);
        }

        #region Properties

        public List<List<int>> KeyCodes
        {
            get { return _keyCodes; }
            set
            {
                if (Validator.IsValid(value))
                    _keyCodes = value;
                else 
                    throw new ArgumentException(
                        $"AffineKey is not valid. Size of argument {value.Count}x{value[0].Count}"
                    );
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
            for (int i = 0; i < KeyCodes.Count; i++) {
                for (int j = 0; j < KeyCodes[0].Count; j++) {
                    output += "[" + KeyCodes[i][j] + "] ";
                }
                output += "\n";
            }
            return output;
        }

        private List<List<int>> GenerateNewKey(int a, int b)
        {
            List<int> inner = new List<int>() {1, 2};
            List<List<int>> outer = new List<List<int>>();
            outer.Add(inner);
            return outer;
        }

        #endregion
    }
}