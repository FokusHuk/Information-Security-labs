using System;
using InfSec.Caesar;

namespace InfSec
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = InfSecSettings.FromJsonFile("./appsettings.json");
            
            var rsaManager = new RSAManager(settings);
            var dhManager = new DHManager(settings);
            var caesarManager = new CaesarManager(settings);

            rsaManager.Execute();
            //dhManager.Execute();
            //caesarManager.Execute();
            //ExecuteAll(settings);
        }

        static void ExecuteAll(InfSecSettings settings)
        {
            var algorithms = new EncryptionAlgorithmManager[]
            {
                new RSAManager(settings),
                new DHManager(settings),
                new CaesarManager(settings)
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