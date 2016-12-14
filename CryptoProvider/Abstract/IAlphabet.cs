namespace icModel.Abstract {
    public interface IAlphabet {
        int Length { get; }
        int GetIndex(char symbol);
        char GetSymbol(int index);
    }
}