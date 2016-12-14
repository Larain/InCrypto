using icModel.Abstract;

namespace icModel.Model.KeyGenerators {
    public class AffineKey : ICryptoKey {
        public int[] KeyCodes { get; private set; }

        public AffineKey(int a, int b) {
            KeyCodes = GenerateNewKey(a, b);

        }

        private int[] GenerateNewKey(int a, int b) {
            return new int[2] {a, b};
        }
    }
}