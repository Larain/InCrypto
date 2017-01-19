using System;
using System.Linq;
using icModel.Abstract;

namespace icModel.Model.Alphabet {
    [Serializable]
    public class SimpleAlphabet : IAlphabet {
        private string _charactersAlphabet;

        public SimpleAlphabet()
        {
            _charactersAlphabet =
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ ";
        }

        public string Dictionary
        {
            get { return _charactersAlphabet; }
        }

        public int Length
        {
            get { return _charactersAlphabet.Length; }
        }

        public int GetIndex(char symbol)
        {
            return _charactersAlphabet.IndexOf(symbol);
        }

        public char GetSymbol(int index)
        {
            return _charactersAlphabet.ElementAt(index);
        }

        public override string ToString() {
            return "Simple Alphabet";
        }
    }
}