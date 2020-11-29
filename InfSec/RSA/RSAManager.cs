using System;

namespace InfSec
{
    public class RSAManager: IEncryptionAlgorithmManager
    {
        public RSAManager()
        {
            Name = "RSA";
        }

        public string Name { get; }

        public void Execute()
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