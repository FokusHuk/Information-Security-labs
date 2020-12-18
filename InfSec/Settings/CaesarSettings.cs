namespace InfSec.Settings
{
    public class CaesarSettings
    {
        public CaesarSettings(string sourceText, int bias, string filePathWithSourceTextForFrequencyDecryption, string filePathWithBaseTextForFrequencyDecryption)
        {
            SourceText = sourceText;
            Bias = bias;
            FilePathWithSourceTextForFrequencyDecryption = filePathWithSourceTextForFrequencyDecryption;
            FilePathWithBaseTextForFrequencyDecryption = filePathWithBaseTextForFrequencyDecryption;
        }
        
        public string SourceText { get; }
        public int Bias { get; }
        public string FilePathWithSourceTextForFrequencyDecryption { get; }
        public string FilePathWithBaseTextForFrequencyDecryption { get; }
    }
}