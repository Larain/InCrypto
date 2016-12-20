using icModel.Abstract;

namespace icModel.Model.KeyGenerators {
    public class AffineKey : ICryptoKey {
        public int[,] KeyCodes { get; private set; }

        public string Print() {
            string output = "";
            for (int i = 0; i < KeyCodes.Length; i++) {
                for (int j = 0; j < KeyCodes.GetLength(0); j++) {
                    output += "[" + KeyCodes[i, j] + "] ";
                }
                output += "\n";
            }
            return output;
        }

        public AffineKey(int a, int b) {
            KeyCodes = GenerateNewKey(a, b);

        }

        private int[,] GenerateNewKey(int a, int b) {
            return new int[2, 1] { { a }, { b } };
        }
    }
}