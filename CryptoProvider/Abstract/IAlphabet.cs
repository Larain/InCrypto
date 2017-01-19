using System.Collections.Generic;
using System.Linq;

namespace icModel.Abstract
{
    public interface IAlphabet
    {
        string Dictionary { get; }
        string DictionaryToShow { get; }
        int Length { get; }
        int GetIndex(char symbol);
        char GetSymbol(int index);
        List<char> SymbolsList { get; }
        List<int> IndexList { get; }
    }
}