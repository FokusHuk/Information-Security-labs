using System;
using InfSec.Caesar;
using InfSec.DH;
using InfSec.RSA;
using InfSec.Settings;
using InfSec.SRP;

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
            var srpManager = new SRPManager(settings);

            //rsaManager.Execute();
            //dhManager.Execute();
            //caesarManager.Execute();
            srpManager.Execute();
            //ExecuteAll(settings);
        }

        static void ExecuteAll(InfSecSettings settings)
        {
            var algorithms = new EncryptionAlgorithmManager[]
            {
                new RSAManager(settings),
                new DHManager(settings),
                new CaesarManager(settings),
                new SRPManager(settings)
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