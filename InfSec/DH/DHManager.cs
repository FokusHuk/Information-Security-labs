using System;

namespace InfSec
{
    public class DHManager: EncryptionAlgorithmManager
    {
        public DHManager(InfSecSettings settings) : base(settings)
        {
            Name = "DH";
        }

        public override string Name { get; }

        public override void Execute()
        {
            var paramG = _settings.DH.G;
            var paramP = _settings.DH.P;

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