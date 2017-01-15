using System.Linq;
using icModel.Abstract;

namespace icModel.Model.Alphabet {
    public class CharactersAlphabet : IAlphabet {
        public int Length {
            get { return _charactersAlphabet.Length; }
        }

        private string _charactersAlphabet;

        public CharactersAlphabet() {
            _charactersAlphabet =
                "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~ ";

        }

        public int GetIndex(char symbol) {
            return _charactersAlphabet.IndexOf(symbol);
        }

        public char GetSymbol(int index) {
            return _charactersAlphabet.ElementAt(index);
        }
    }
}