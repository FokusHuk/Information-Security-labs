using System;

namespace InfSec
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestDH();
            TestRSA();
        }

        static void TestDH()
        {
            Console.WriteLine("Diffie-Hellman Algorithm.");
            
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

        static void TestRSA()
        {
            var rsa = new AlgorithmRSA(3, 11);
            
            rsa.GenerateKeys();

            Console.WriteLine($"Generated keys:\n" +
                              $"Public: {rsa.PublicKey.EXP} - {rsa.PublicKey.VALUE}\n" +
                              $"Private: {rsa.PrivateKey.EXP} - {rsa.PrivateKey.VALUE}\n");

            var message = 24;

            Console.WriteLine($"Message to encrypt: {message}");

            var encryptedMessage = rsa.Encryption(message);
            
            Console.WriteLine($"Encrypted message: {encryptedMessage}");
            
            var decryptedMessage = rsa.Decryption(encryptedMessage);
            
            Console.WriteLine($"Decrypted message: {decryptedMessage}");

            Console.WriteLine($"Are messages equals: {message == decryptedMessage}");
        }
    }
}