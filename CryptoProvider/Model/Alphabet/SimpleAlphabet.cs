using System;
using System.Collections.Generic;
using System.Linq;
using icModel.Abstract;

namespace icModel.Model.Alphabet {
    [Serializable]
    public class SimpleAlphabet : Abstract.Alphabet {
        private string _charactersAlphabet;

        public SimpleAlphabet()
        {
            _charactersAlphabet =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
        }

        public override string Dictionary
        {
            get { return _charactersAlphabet; }
        }
        public override string DictionaryToShow
        {
            get { return _charactersAlphabet + "+ ' '"; }
        }

        public override string ToString() {
            return "Eng simple alphabet";
        }
    }
}