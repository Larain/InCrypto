using System;
using System.Collections.Generic;
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

        public int GetIndex(char symbol)
        {
            return _charactersAlphabet.IndexOf(symbol);
        }

        public char GetSymbol(int index)
        {
            return _charactersAlphabet.ElementAt(index);
        }

        public override string ToString() {
            return "Eng simple alphabet";
        }
    }
}