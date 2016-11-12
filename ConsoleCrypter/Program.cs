using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using icModel.Provider;
using icModel.Method;
using icModel.Key;
using icModel.Alphabet;

namespace ConsoleCrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            B test = new B();
            Console.WriteLine(test.smallb);
            //Console.WriteLine(test.av.a);
            Console.Read();
            
            int a = new int();
            Console.WriteLine(a);

            CharactersAlphabet alphabet = new CharactersAlphabet();

            List<int> nods = new List<int>();
            for (int i = 0; i < 100; i++)
                if (alphabet.IsNod(i))
                    nods.Add(i);

            foreach (var item in nods)
                Console.Write(item.ToString() + ", ");

            AffineKey key = new AffineKey(5, 9);
            Console.WriteLine("\nkey = {0}, {1}", key.Key[0], key.Key[1]);
            AffineCipher method = new AffineCipher(alphabet);
            CryptoProvider cp = new CryptoProvider(key, method);

            //string msg = "Open:-> message!";
            string msg = "Roma privet eto text";
            Console.WriteLine(msg);
            string enmsg = cp.Encrypt(msg).ToString();
            Console.WriteLine(enmsg);
            cp.CryptoKey = new AffineKey(5, 9);
            string demsg = cp.Decrypt(enmsg).ToString();
            Console.WriteLine(demsg);

            Console.ReadKey();
        }
    }
}
