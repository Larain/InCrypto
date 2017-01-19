using System;
using System.Collections.Generic;
using System.Linq;
using icModel.Abstract;

namespace icModel.Model.Alphabet {
    [Serializable]
    public class CharactersAlphabet : Abstract.Alphabet {
        private readonly string _charactersAlphabet;

        public CharactersAlphabet() {
            _charactersAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?@#$%^&*()_+ ./';\\][`~=-";
            _charactersAlphabet += "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();
        }

        public override string Dictionary
        {
            get { return _charactersAlphabet; }
        }

        public override string DictionaryToShow
        {
            get { return _charactersAlphabet + "+ ' '"; }
        }

        public override string ToString()
        {
            return "Eng extended alphabet";
        }
    }
}