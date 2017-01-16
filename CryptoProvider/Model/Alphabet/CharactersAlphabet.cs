using System.Linq;
using icModel.Abstract;

namespace icModel.Model.Alphabet {
    public class CharactersAlphabet : IAlphabet {
        private readonly string _charactersAlphabet;

        public CharactersAlphabet() {
            _charactersAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
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
            return _charactersAlphabet.IndexOf(symbol);
        }

        public char GetSymbol(int index) {
            return _charactersAlphabet.ElementAt(index);
        }
    }
}