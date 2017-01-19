using System;
using System.Linq;
using icModel.Abstract;

namespace icModel.Model.Alphabet {
    [Serializable]
    public class CharactersAlphabet : IAlphabet {
        private readonly string _charactersAlphabet;

        public CharactersAlphabet() {
            _charactersAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?@#$%^&*()_+ ./';\\][`~=-";
        }

        public string Dictionary
        {
            get { return _charactersAlphabet; }
        }

        public int Length
        {
            get { return _charactersAlphabet.Length; }
        }

        public int GetIndex(char symbol) {
            return _charactersAlphabet.IndexOf(symbol) + 1;
        }

        public char GetSymbol(int index) {
            return _charactersAlphabet.ElementAt(index - 1);
        }

        public override string ToString()
        {
            return "Full Alphabet";
        }
    }
}