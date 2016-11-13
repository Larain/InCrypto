using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace icApplication.Helper
{
    public static class NodHelper
    {
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
    }
}
