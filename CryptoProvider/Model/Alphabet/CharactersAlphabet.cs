using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace icModel.Alphabet
{
    public class CharactersAlphabet : IAlphabet
    {
        public int Length { get; private set; }
        private string alphabet;
        public CharactersAlphabet()
        {
            alphabet = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~ ";
            Length = alphabet.Length;
        }

        public int GetIndex(char symbol)
        {
            return alphabet.IndexOf(symbol);
        }
        public char GetSymbol(int index)
        {
            return alphabet.ElementAt(index);
        }
    }
}
