using System;
using System.Collections.Generic;
using System.Linq;
using icModel.Abstract;

namespace icModel.Model.Alphabet {
    [Serializable]
    public class CharactersAlphabet : IAlphabet {
        private readonly string _charactersAlphabet;

        public CharactersAlphabet() {
            _charactersAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!?@#$%^&*()_+ ./';\\][`~=-";
            _charactersAlphabet += "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToLower();
        }

        public string Dictionary
        {
            get { return _charactersAlphabet; }
        }

        public string DictionaryToShow
        {
            get { return _charactersAlphabet + "+ ' '"; }
        }

        public List<char> SymbolsList
        {
            get { return Dictionary.ToCharArray().ToList(); }
        }
        public List<int> IndexList
        {
            get
            {
                List<int> list = new List<int>();
                foreach (char c in Dictionary)
                {
                    list.Add(Dictionary.IndexOf(c) + 1);
                }
                return list;
            }
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
            return "Eng extended alphabet";
        }
    }
}