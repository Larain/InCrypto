using System;

namespace icModel.Model.Helpers {
    public static class CryptoHelper {
        public static int Mod(int x, int m) {
            int r = x%m;
            return r < 0 ? r + m : r;
        }

        public static int GetInvA(int a, int length) {
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
                throw new ArgumentException("InvA is unracheable");
            return invA.Value;

        }
    }
}