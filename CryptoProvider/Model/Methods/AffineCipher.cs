using icModel.Alphabet;
using icModel.Key;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace icModel.Method
{
    public class AffineCipher : ICryptoMethod
    {
        IAlphabet alphabet;
        int length;
        public AffineCipher(IAlphabet characterTable)
        {
            alphabet = characterTable;
            length = alphabet.Length;
        }
        public string Encrypt(string message, ICryptoKey key)
        {
            int[] parameters = key.Key;

            // Encrypting message to digit code.
            int[] codedDigitMessage = new int[message.Length];
            for (int i = 0; i < message.Length; i++)
                codedDigitMessage[i] = EncryptoFunc(alphabet.GetIndex
                    (Convert.ToChar(message[i])), parameters[0], parameters[1]);

            // Convert digits to char Array.
            char[] codedMessage = new char[message.Length];
            for (int i = 0; i < message.Length; i++)
                codedMessage[i] = alphabet.GetSymbol(codedDigitMessage[i]);

            return new string(codedMessage);
        }

        public string Decrypt(string message, ICryptoKey key)
        {
            int[] parameters = key.Key;
            int? InvA = GetInvA(parameters[0]);
            if (InvA == null)
                throw new ArgumentException("Can not find InvA :(");

            int a = InvA ?? InvA.Value;

            // Decrypting message to digit code.
            int[] codedDigitMessage = new int[message.Length];
            for (int i = 0; i < message.Length; i++)
                codedDigitMessage[i] = DecryptoFunc(alphabet.GetIndex
                    (Convert.ToChar(message[i])), a, parameters[1]);

            // Convert digits to char Array.
            char[] codedMessage = new char[message.Length];
            for (int i = 0; i < message.Length; i++)
                codedMessage[i] = alphabet.GetSymbol(codedDigitMessage[i]);

            return new string(codedMessage);
        }
        private int EncryptoFunc(int x, int a, int b)
        {
            return Mod((a * x + b), length);
        }
        private int DecryptoFunc(int x, int a, int b)
        {
            return Mod((a * (x - b)), length);
        }
        int Mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }
        private int? GetInvA(int a)
        {
            int? invA = null;
            for (int i = 0; ; i++)
            {
                if ((i * a) % length == 1)
                {
                    invA = i;
                    break;
                }
                if (i > 200)
                    break;
            }
            if (invA != null)
                return invA.Value;
            else
                return null;
        }
    }
}
