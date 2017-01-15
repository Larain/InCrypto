using System;
using System.Collections.Generic;
using System.Data;
using icModel.Abstract;
using icModel.Model.Entities;
using icModel.Model.Helpers;
using icModel.Model.Keys;

namespace icModel.Model.Providers {
    public class AffineCipher : ICryptoProvider
    {
        private AffineKey _key;
        public AffineCipher(IAlphabet characterTable, AffineKey key) {
            Alphabet = characterTable;
            Key = key;
        }

        public IAlphabet Alphabet { set; get; }

        public ICryptoKey Key {
            get { return _key; }
            set
            {
                if (value == null)
                    throw new NoNullAllowedException();
                    _key = (AffineKey)value;
            } }


        #region Methods

        public string[] Encrypt(string[] message) {
            return CryptoHelper.ConvertDigitsToChar(Proccess(message, Mode.Encrypt), Alphabet);
        }

        public string[] Decrypt(string[] message) {
            return CryptoHelper.ConvertDigitsToChar(Proccess(message, Mode.Decrypt), Alphabet);
        }

        private List<int[]> Proccess(string[] message, Mode mode) {
            int[,] parameters = Key.KeyCodes;

            int a = 0;
            if (mode == Mode.Decrypt)
                a = CryptoHelper.GetInvA(parameters[0, 0], Alphabet.Length);

            // Decrypting message to digit code.
            List<int[]> codedDigitMessage = new List<int[]>();
            for (int i = 0; i < message.Length; i++) {
                int[] lineDigitMessage = new int[message[i].Length];
                for (int j = 0; j < message[i].Length; j++) {
                    if (mode == Mode.Encrypt)
                        lineDigitMessage[j] = EncryptoFunc(Alphabet.GetIndex(Convert.ToChar(message[i][j])),
                            parameters[0, 0], parameters[0, 1]);
                    if (mode == Mode.Decrypt)
                        lineDigitMessage[j] = DecryptoFunc(Alphabet.GetIndex(Convert.ToChar(message[i][j])), a,
                            parameters[0, 1]);
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

        #endregion
    }
}