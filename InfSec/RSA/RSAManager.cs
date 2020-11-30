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
            var rsa = new AlgorithmRSA(_settings.RSA.P, _settings.RSA.Q);
            var message = _settings.RSA.Message;
            
            rsa.GenerateKeys();

            Console.WriteLine($"Generated keys:\n" +
                              $"Public: {rsa.PublicKey.EXP} - {rsa.PublicKey.VALUE}\n" +
                              $"Private: {rsa.PrivateKey.EXP} - {rsa.PrivateKey.VALUE}\n");
            
            Console.WriteLine($"Message to encrypt: {message}");

            var encryptedMessage = rsa.Encryption(message);
            
            Console.WriteLine($"Encrypted message: {encryptedMessage}");
            
            var decryptedMessage = rsa.Decryption(encryptedMessage);
            
            Console.WriteLine($"Decrypted message: {decryptedMessage}");

            Console.WriteLine($"Are messages equals: {message == decryptedMessage}");
        }
    }
}