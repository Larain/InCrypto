using System;
using System.Linq;
using System.Text;
using icModel.Abstract;
using icModel.Model.Entities;
using System.Windows.Input;
using icModel.Model.Keys;

namespace icModel.Model.Providers
{
    public class HillCipher : ICryptoProvider
    {
        private ICryptoKey _key;

        public HillCipher(IAlphabet characterTable)
        {
            Alphabet = characterTable;
        }

        #region Public Methods

        public IAlphabet Alphabet { set; get; }

        public ICryptoKey Key
        {
            get { return _key; }
            set
            {
                if (value != null)
                    _key = (HillKey)value;
            }
        }

        public string[] Encrypt(string[] plainText)
        {
            return Process(plainText, Mode.Encrypt);
        }

        public string[] Decrypt(string[] cipher)
        {
            return Process(cipher, Mode.Decrypt);
        }

        #endregion

        #region Private Methods

        private string[] Process(string[] message, Mode mode)
        {
            MatrixClass matrix = new MatrixClass(_key.KeyArray);

            if (mode == Mode.Decrypt)
            {
                matrix = matrix.Inverse();
            }

            int index = 0;
            string[] proccessedString = new string[message.Length];

            foreach (string line in message) {

                int pos = 0;

                int matrixSize = _key.KeyArray.GetLength(0);

                string newLine = "";
                while (pos < line.Length)
                {
                    string result = "";
                    string substring = "";
                    try {
                        substring = line.Substring(pos, matrixSize);
                    }
                    catch (ArgumentOutOfRangeException) {
                        throw new CipherException(string.Format("Invalid length of string {0} in line {1}", line.Length, index));
                        return null;
                    }
                    
                    pos += matrixSize;

                    for (int i = 0; i < matrixSize; i++)
                    {
                        string portion = "";
                        var charPosition = 0;

                        for (int j = 0; j < matrixSize; j++)
                        {
                            charPosition += (int)matrix[j, i].Numerator * Alphabet.GetIndex(substring[j]);
                        }

                        result += Alphabet.GetSymbol(charPosition % 26);
                    }

                    newLine += result;
                }
                proccessedString[index++] = newLine;
            }

            return proccessedString;
        }

        #endregion
    }
}
