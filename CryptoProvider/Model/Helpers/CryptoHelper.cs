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

        private static Tuple<int, Tuple<int, int>> ExtendedEuclid(int a, int b)
        {
            int x = 1, y = 0;
            int xLast = 0, yLast = 1;
            int q, r, m, n;
            while (a != 0)
            {
                q = b / a;
                r = b % a;
                m = xLast - q * x;
                n = yLast - q * y;
                xLast = x; yLast = y;
                x = m; y = n;
                b = a; a = r;
            }
            return new Tuple<int, Tuple<int, int>>(b, new Tuple<int, int>(xLast, yLast));
        }

        public static int ModInverse(int a, int m)
        {
            return CryptoHelper.Mod((ExtendedEuclid(a, m).Item2.Item1 + m), m);
        }
    }
}