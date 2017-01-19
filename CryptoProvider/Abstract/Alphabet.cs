using System.Collections.Generic;
using System.Linq;

namespace icModel.Abstract
{
    public abstract class Alphabet
    {
        public abstract string Dictionary { get; }
        public abstract string DictionaryToShow { get; }

        public int Length {
            get { return Dictionary.Length; }
        }
        public int GetIndex(char symbol)
        {
            return Dictionary.IndexOf(symbol) + 1;
        }
        public char GetSymbol(int index) {
            if (index == 0)
                index = Dictionary.Length;
            if (index == Length)
                index = Length;
            return Dictionary.ElementAt(index - 1);
        }
        public List<char> SymbolsList
        {
            get { return Dictionary.ToCharArray().ToList(); }
        }
        public List<int> IndexList
        {
            get {
                return Dictionary.Select(c => Dictionary.IndexOf(c)).ToList();
            }
        }
    }
}