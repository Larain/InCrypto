using System.Collections.ObjectModel;
using icModel.Abstract;

namespace icModel.Model.Keys
{
    public class HillKeyValidator : ICryptoKeyValidator
    {
        public bool IsValid(ObservableCollection<ObservableCollection<int>> key)
        {
            if (key != null)
                if (key.Count == key[0].Count)
                        return true;
            return false;
        }

        public bool IsValid(int[][] key)
        {
            if (key != null)
                if (key.Length == key[0].Length)
                        return true;
            return false;
        }

        public bool IsValid(int[,] key)
        {
            if (key != null)
                if (key.GetLength(1) == key.GetLength(0))
                    return true;
            return false;
        }

        public bool IsValid(ICryptoKey key)
        {
            return IsValid(key.KeyCodes);
        }
    }
}