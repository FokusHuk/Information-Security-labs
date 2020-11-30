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
            var caesarManager = new CaesarManager();

            //rsaManager.Execute();
            //dhManager.Execute();
            caesarManager.Execute();
            //ExecuteAll();
        }

        static void ExecuteAll()
        {
            var algorithms = new IEncryptionAlgorithmManager[]
            {
                new RSAManager(),
                new DHManager(),
                new CaesarManager()
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