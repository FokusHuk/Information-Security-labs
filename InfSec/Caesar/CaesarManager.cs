using System;
using System.IO;

namespace InfSec.Caesar
{
    public class CaesarManager: IEncryptionAlgorithmManager
    {
        public CaesarManager()
        {
            Name = "Caesar";
        }

        public string Name { get; }
        public void Execute()
        {
            var caesar = new CaesarAlgorithm();
            var sourceText = "Sample text to encrypt";
            var bias = 7;

            Console.WriteLine($"Source text: {sourceText}\nBias: {bias}\n");
            
            var encryptedText = caesar.Encryption(sourceText, bias);
            Console.WriteLine($"Encrypted text: {encryptedText}\n");
            
            var decryptedText = caesar.Decryption(encryptedText, bias);
            Console.WriteLine($"Decrypted text: {decryptedText}\n");
            
            executeFrequncyDecryption();
        }

        private void executeFrequncyDecryption()
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Decryption by frequency (see the source file and base in the \"Test data\" folder).");
            
            var caesar = new CaesarAlgorithm();
            var sourceText = FileManager.GetTextFromFile("./Test data/Caesar source text.txt");
            var baseText = FileManager.GetTextFromFile("./Test data/Caesar base.txt");
            var bias = 7;
            
            var encryptedText = caesar.Encryption(sourceText, bias);
            Console.WriteLine($"Encrypted text:\n{encryptedText}\n");
            
            var decryptedText = caesar.DecryptionByFrequency(baseText, encryptedText);
            Console.WriteLine($"Decrypted text:\n{decryptedText}\n");
        }
    }
}