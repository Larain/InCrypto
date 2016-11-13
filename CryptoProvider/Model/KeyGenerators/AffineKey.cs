using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace icModel.Key
{
    public class AffineKey : ICryptoKey
    {
        public int[] Key { get; private set; }

        public AffineKey(int a, int b)
        {
            Key = GenerateNewKey(a, b);

        }
        private int[] GenerateNewKey(int a, int b)
        {
            return new int[2] { a, b };
        }
    }
}
