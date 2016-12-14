using System.Linq;
using icModel.Abstract;

namespace icModel.Model.Alphabet {
    public class CharactersAlphabet : IAlphabet {
        public int Length {
            get { return charactersAlphabet.Length; }
        }

        private string charactersAlphabet;

        public CharactersAlphabet() {
            charactersAlphabet =
                "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~ ";

        }

        public int GetIndex(char symbol) {
            return charactersAlphabet.IndexOf(symbol);
        }

        public char GetSymbol(int index) {
            return charactersAlphabet.ElementAt(index);
        }
    }
}