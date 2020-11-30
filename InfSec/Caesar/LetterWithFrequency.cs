namespace InfSec.Caesar
{
    public class LetterWithFrequency
    {
        public LetterWithFrequency(char letter, int frequency)
        {
            Letter = letter;
            Frequency = frequency;
        }
        
        public char Letter { get; }
        public int Frequency { get; }
    }
}