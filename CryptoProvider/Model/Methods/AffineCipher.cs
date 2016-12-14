using System;
using System.Collections.Generic;
using icModel.Abstract;
using icModel.Model.Entities;
using icModel.Model.Helpers;

namespace icModel.Model.Methods {
    public class AffineCipher : CryptoMethod {

        public AffineCipher(IAlphabet characterTable, ICryptoKey key) {
            Alphabet = characterTable;
            Key = key;
        }

        public override string[] Encrypt(string[] message) {
            return ConvertDigitsToChar(Proccess(message, Mode.Encrypt));
        }

        public override string[] Decrypt(string[] message) {
            return ConvertDigitsToChar(Proccess(message, Mode.Decrypt));
        }

        private List<int[]> Proccess(string[] message, Mode mode) {
            int[] parameters = Key.KeyCodes;

            int a = 0;
            if (mode == Mode.Decrypt)
                a = CryptoHelper.GetInvA(parameters[0], Alphabet.Length);

            // Decrypting message to digit code.
            List<int[]> codedDigitMessage = new List<int[]>();
            for (int i = 0; i < message.Length; i++) {
                int[] lineDigitMessage = new int[message[i].Length];
                for (int j = 0; j < message[i].Length; j++) {
                    if (mode == Mode.Encrypt)
                        lineDigitMessage[j] = EncryptoFunc(Alphabet.GetIndex(Convert.ToChar(message[i][j])),
                            parameters[0], parameters[1]);
                    if (mode == Mode.Decrypt)
                        lineDigitMessage[j] = DecryptoFunc(Alphabet.GetIndex(Convert.ToChar(message[i][j])), a,
                            parameters[1]);
                }
                codedDigitMessage.Add(lineDigitMessage);
            }
            return codedDigitMessage;
        }

        private int EncryptoFunc(int x, int a, int b) {
            return CryptoHelper.Mod((a*x + b), Alphabet.Length);
        }

        private int DecryptoFunc(int x, int a, int b) {
            return CryptoHelper.Mod((a*(x - b)), Alphabet.Length);
        }

    }
}