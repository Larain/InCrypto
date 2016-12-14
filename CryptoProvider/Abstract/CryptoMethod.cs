using System.Collections.Generic;

namespace icModel.Abstract {
    public abstract class CryptoMethod {
        public ICryptoKey Key { get; set; }
        public IAlphabet Alphabet { get; set; }
        public abstract string[] Encrypt(string[] message);
        public abstract string[] Decrypt(string[] message);

        /// <summary>
        /// Convert digits to char Array.
        /// </summary>
        /// <param name="codedDigitMessage"></param>
        /// <returns></returns>
        protected string[] ConvertDigitsToChar(List<int[]> codedDigitMessage) {
            List<string> codedMessage = new List<string>();
            for (int i = 0; i < codedDigitMessage.Count; i++) {
                char[] codedLineMessage = new char[codedDigitMessage[i].Length];
                for (int j = 0; j < codedDigitMessage[i].Length; j++) {
                    codedLineMessage[j] = Alphabet.GetSymbol(codedDigitMessage[i][j]);
                }
                codedMessage.Add(new string(codedLineMessage));
            }
            return codedMessage.ToArray();
        }
    }
}