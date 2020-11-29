using System;
using System.Reflection.Metadata.Ecma335;

namespace InfSec
{
    class Program
    {
        static void Main(string[] args)
        {
            var rsaManager = new RSAManager();
            var dhManager = new DHManager();
            
            //rsaManager.Execute();
            //dhManager.Execute();
            ExecuteAll();
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