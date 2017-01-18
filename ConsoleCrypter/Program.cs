using System;
using System.Drawing;
using icModel.Abstract;
using icModel.Model.Alphabet;
using icModel.Model.Keys;
using icModel.Model.Providers;

namespace ConsoleCrypter
{
    //public class SuperKey : ICryptoKey
    //{
    //    public SuperKey()
    //    {
    //        SomeNewShit = new List<string>();
    //    }
    //    public List<string> SomeNewShit { get; set; }
    //    public List<List<int>> KeyCodes { get; set; }
    //    public ICryptoKeyValidator Validator { get; }
    //}
    class Program
    {
        static void Main(string[] args)
        {
            //ICryptoKey key = new AffineKey(2, 5);
            //Console.WriteLine(key.Validator.IsValid(key));
            int [,] a = new int[1,1] { {1} };
            //SuperKey k = new SuperKey();
            //k.SomeNewShit.Add("some string");

            //ICryptoKey key2 = k;
            //Console.WriteLine(key.Validator.IsValid(key2));

            ////int[,] newKyes = new int[2,2] { {1,2}, {3,4} };
            ////key.KeyCodes = newKyes;

            //AffineCipher cipher = new AffineCipher(new CharactersAlphabet(), (AffineKey)key);
            //cipher.Key = key2;

            Console.Read();
        }
    }
}
