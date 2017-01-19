using System.Collections.ObjectModel;
using icModel.Abstract;

//namespace icModel.Model.Keys {
//    public class AffineKeyValidator : ICryptoKeyValidator {
//        public bool IsValid(ObservableCollection<ObservableCollection<int>> key) {
//            if (key != null)
//                if (key.Count == 1)
//                    if (key[0].Count == 2)
//                        return true;
//            return false;
//        }

//        public bool IsValid(int[][] key) {
//            if (key != null)
//                if (key.Length == 1)
//                    if (key[0].Length == 2)
//                        return true;
//            return false;
//        }

//        public bool IsValid(int[][] key, int module) {
//            throw new System.NotImplementedException();
//        }

//        public bool IsValid(int[,] key, int module)
//        {
//            if (key != null)
//                if (key.Length == key.GetLength(0))
//                        return true;
//            return false;
//        }

//        public bool IsValid(ICryptoKey key) {
//            return IsValid(key.KeyCodes);
//        }
//    }
//}