using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace icModel.Alphabet
{
    public interface IAlphabet
    {
        int Length { get; }
        int GetIndex(char symbol);
        char GetSymbol(int index);
    }
}
