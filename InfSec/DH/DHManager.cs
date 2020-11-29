using System;

namespace InfSec
{
    public class DHManager: IEncryptionAlgorithmManager
    {
        public DHManager()
        {
            Name = "DH";
        }

        public string Name { get; }

        public void Execute()
        {
            var paramG = 32.0;
            var paramP = 40.0;

            Console.WriteLine($"Parameter G: {paramG}\nParamter P: {paramP}\n");
            
            var Alice = new DiffieHellmanAlgorithm(paramG, paramP);
            var Bob = new DiffieHellmanAlgorithm(paramG, paramP);
            
            Alice.GenerateNumber();
            Bob.GenerateNumber();

            Console.WriteLine($"Generated numbers:\n" +
                              $"Alice: {Alice.LocalGeneratedParameter}\n" +
                              $"Bob: {Bob.LocalGeneratedParameter}\n");

            var AliceParameter = Alice.CalculateParameterToSend();
            var BobParameter = Bob.CalculateParameterToSend();
            
            Console.WriteLine($"Calculated parameters:\n" +
                              $"Alice: {AliceParameter}\n" +
                              $"Bob: {BobParameter}\n");
            
            Alice.CalculateKey(BobParameter);
            Bob.CalculateKey(AliceParameter);
            
            Console.WriteLine($"Calculated keys:\n" +
                              $"Alice: {Alice.Key}\n" +
                              $"Bob: {Bob.Key}\n");

            Console.WriteLine($"Are keys equals: {Alice.Key == Bob.Key}");
        }
    }
}