using System;
using System.Collections.Generic;
using System.Linq;
using icModel.Abstract;

namespace icModel.Model.Alphabet
{
    [Serializable]
    public class RusExtendedAlphabet : Abstract.Alphabet
    {
        private string _charactersAlphabet;

        public RusExtendedAlphabet()
        {
            _charactersAlphabet =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?@#$%^&*()_+ ./';\\][`~=-";
            _charactersAlphabet += "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();
            _charactersAlphabet += "ЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ";
            _charactersAlphabet += "йцукенгшщзхъфывапролджэячсмитьбю";

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
            return "Rus and Eng full alphabet";
        }
    }
}