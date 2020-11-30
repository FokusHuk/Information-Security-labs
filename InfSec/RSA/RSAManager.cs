using System;

namespace InfSec
{
    public class RSAManager: EncryptionAlgorithmManager
    {
        public RSAManager(InfSecSettings settings) : base(settings)
        {
            Name = "RSA";
        }

        public override string Name { get; }

        public override void Execute()
        {
            var rsa = new AlgorithmRSA();
            var message = _settings.RSA.Message;
            
            rsa.GenerateKeys();

            Console.WriteLine($"Generated keys:\n" +
                              $"Public: {rsa.PK.EXP} - {rsa.PK.VALUE}\n" +
                              $"Private: {rsa.SK.EXP} - {rsa.SK.VALUE}\n");
            
            Console.WriteLine($"Message to encrypt: {message}");

            var encryptedMessage = rsa.Encryption(message);
            
            Console.WriteLine($"Encrypted message: {encryptedMessage}");
            
            var decryptedMessage = rsa.Decryption(encryptedMessage);
            
            Console.WriteLine($"Decrypted message: {decryptedMessage}");

            Console.WriteLine($"Are messages equals: {message == decryptedMessage}");
        }
    }
}