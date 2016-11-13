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
        public string[] Encrypt(string[] message, ICryptoKey key)
        {
            int[] parameters = key.Key;

            // Decrypting message to digit code.
            List<int[]> codedDigitMessage = new List<int[]>();
            for (int i = 0; i < message.Length; i++)
            {
                int[] lineDigitMessage = new int[message[i].Length];
                for (int j = 0; j < message[i].Length; j++)
                {
                    lineDigitMessage[j] = EncryptoFunc(alphabet.GetIndex(Convert.ToChar(message[i][j])), parameters[0], parameters[1]);
                }
                codedDigitMessage.Add(lineDigitMessage);
            }

            // Convert digits to char Array.
            List<string> codedMessage = new List<string>();
            for (int i = 0; i < message.Length; i++)
            {
                char[] codedLineMessage = new char[message[i].Length];
                for (int j = 0; j < message[i].Length; j++)
                {
                    codedLineMessage[j] = alphabet.GetSymbol(codedDigitMessage[i][j]);
                }
                codedMessage.Add(new string(codedLineMessage));
            }

            return codedMessage.ToArray();
        }

        public string[] Decrypt(string[] message, ICryptoKey key)
        {
            int[] parameters = key.Key;
            int? InvA = GetInvA(parameters[0]);
            if (InvA == null)
                throw new ArgumentException("Can not find InvA :(");

            int a = InvA ?? InvA.Value;

            // Decrypting message to digit code.
            List<int[]> codedDigitMessage = new List<int[]>();
            for (int i = 0; i < message.Length; i++)
            {
                int[] lineDigitMessage = new int[message[i].Length];
                for (int j = 0; j < message[i].Length; j++)
                {
                    lineDigitMessage[j] = DecryptoFunc(alphabet.GetIndex(Convert.ToChar(message[i][j])), a, parameters[1]);
                }
                codedDigitMessage.Add(lineDigitMessage);
            }

            // Convert digits to char Array.
            List<string> codedMessage = new List<string>();
            for (int i = 0; i < message.Length; i++)
            {
                char[] codedLineMessage = new char[message[i].Length];
                for (int j = 0; j < message[i].Length; j++)
                {
                    codedLineMessage[j] = alphabet.GetSymbol(codedDigitMessage[i][j]);
                }
                codedMessage.Add(new string(codedLineMessage));
            }

            return (codedMessage.ToArray());
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
