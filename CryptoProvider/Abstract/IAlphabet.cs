namespace icModel.Abstract {
    public interface IAlphabet {
        string Dictionary { get; }
        int Length { get; }
        int GetIndex(char symbol);
        char GetSymbol(int index);
    }
}