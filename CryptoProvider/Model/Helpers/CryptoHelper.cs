using System;
using System.Collections.Generic;
using System.Linq;
using icModel.Abstract;

namespace icModel.Model.Helpers {
    public static class CryptoHelper {
        /// <summary>
        /// Integer division operation
        /// </summary>
        /// <param name="x">x parametr</param>
        /// <param name="m">m - maximal value</param>
        /// <returns></returns>
        public static int Mod(int x, int m) {
            int r = x%m;
            return r < 0 ? r + m : r;
        }

        /// <summary>
        /// Indicate is m and n Nods
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsNod(int m, int n)
        {
            int nod = 0;
            for (int i = 1; i < (n * m + 1); i++)
            {
                if (m % i == 0 && n % i == 0)
                {
                    nod = i;
                }
            }
            if (nod == 1)
                return true;
            return false;
        }

        /// <summary>
        /// Search reciprocal number
        /// </summary>
        /// <param name="a">The number to which find reciprocation</param>
        /// <param name="length">Length of alphabet</param>
        /// <returns></returns>
        public static int Reciprocal(int a, int length) {
            int? invA = null;
            for (int i = 0;; i++) {
                if ((i*a)%length == 1) {
                    invA = i;
                    break;
                }
                if (i > 200)
                    break;
            }
            if (invA == null)
                throw new ArgumentException("Reciprocal InvA is unracheable");
            return invA.Value;
        }

        /// <summary>
        /// Convert digits to char Array.
        /// </summary>
        /// <param name="codedDigitMessage">Message represented in digit format</param>
        /// <param name="alphabet">Used Crypto Alphabet</param>
        /// <returns></returns>
        public static string[] ConvertDigitsToChar(List<int[]> codedDigitMessage, Abstract.Alphabet alphabet) {
            List<string> codedMessage = new List<string>();
            for (int i = 0; i < codedDigitMessage.Count; i++) {
                char[] codedLineMessage = new char[codedDigitMessage[i].Length];

                for (int j = 0; j < codedDigitMessage[i].Length; j++)
                    codedLineMessage[j] = alphabet.GetSymbol(codedDigitMessage[i][j]);

                codedMessage.Add(new string(codedLineMessage));
            }
            return codedMessage.ToArray();
        }

        private static readonly Random Random = new Random();

        /// <summary>
        /// Generate string with random characters
        /// </summary>
        /// <param name="length">Legth of generated string</param>
        /// <param name="alphabet">Alphabet of charaters to use for generating</param>
        /// <returns></returns>
        public static string RandomString(int length, Abstract.Alphabet alphabet) {
            return new string(Enumerable.Repeat(alphabet.Dictionary, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static string ArrayToString(this double[,] arr)
        {
            string output = "";
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    output += "[" + arr[i, j] + "] ";
                }
                output += "\n";
            }
            return output;
        }

        public static int ModInv(int a, int b)
        {
            int b0 = b, t, q;
            int x0 = 0, x1 = 1;
            if (b == 1)
                return 1;
            while (a > 1)
            {
                q = a / b;
                t = b;
                b = Mod(a, b);
                a = t;
                t = x0;
                x0 = x1 - q*x0;
                x1 = t;
            }
            if (x1 < 0) x1 += b0;
            return x1;
        }
    }
}