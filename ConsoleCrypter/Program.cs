using System;
using icModel.Abstract;
using icModel.Model.Keys;

namespace ConsoleCrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            ICryptoKey key = new AffineKey(2, 5);
            
            Console.WriteLine(key.Validator.IsValid(key));
            Console.Read();
        }
    }
}
