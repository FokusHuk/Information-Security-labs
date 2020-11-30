using System;
using System.Reflection.Metadata.Ecma335;
using InfSec.Caesar;

namespace InfSec
{
    class Program
    {
        static void Main(string[] args)
        {
            var rsaManager = new RSAManager();
            var dhManager = new DHManager();
            
            var caesar = new CaesarAlgorithm();
            
            caesar.DecryptionByFrequency("aabcddd", "");
            
            var encr = caesar.Encryption("hello", 2);
            var decr = caesar.Decryption(encr, 2);
            Console.WriteLine($"{encr}\n{decr}");
            
            
            var source = FileManager.GetTextFromFile("./Test data/Caesar source text.txt");
            var baseText = FileManager.GetTextFromFile("./Test data/Caesar base.txt");

            var enc = caesar.Encryption(source, 5);
            var dec = caesar.DecryptionByFrequency(baseText, enc);

            Console.WriteLine(dec);

            //rsaManager.Execute();
            //dhManager.Execute();
            //ExecuteAll();
        }

        static void ExecuteAll()
        {
            var algorithms = new IEncryptionAlgorithmManager[]
            {
                new RSAManager(),
                new DHManager()
            };

            foreach (var algorithm in algorithms)
            {
                Console.WriteLine($"ALGORITHM: {algorithm.Name}");
                algorithm.Execute();
                Console.WriteLine("=======================================================");
            }
        }
    }
}