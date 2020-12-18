using System;
using InfSec.Infrastructure;
using InfSec.Settings;

namespace InfSec.Caesar
{
    public class CaesarManager: EncryptionAlgorithmManager
    {
        public CaesarManager(InfSecSettings settings) : base(settings)
        {
            Name = "Caesar";
        }

        public override string Name { get; }

        public override void Execute()
        {
            var caesar = new CaesarAlgorithm();
            var sourceText = _settings.Caesar.SourceText;
            var bias = _settings.Caesar.Bias;

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
            Console.WriteLine("Decryption by frequency (see the source file and base in the \"Test data\" folder).\n");
            
            var caesar = new CaesarAlgorithm();
            var sourceText = FileManager.GetTextFromFile(_settings.Caesar.FilePathWithSourceTextForFrequencyDecryption);
            var baseText = FileManager.GetTextFromFile(_settings.Caesar.FilePathWithBaseTextForFrequencyDecryption);
            var bias = _settings.Caesar.Bias;
            
            var encryptedText = caesar.Encryption(sourceText, bias);
            Console.WriteLine($"Encrypted text:\n{encryptedText}\n");
            
            var decryptedText = caesar.DecryptionByFrequency(baseText, encryptedText);
            Console.WriteLine($"Decrypted text:\n{decryptedText}\n");
        }
    }
}