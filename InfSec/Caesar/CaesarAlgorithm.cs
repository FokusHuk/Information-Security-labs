using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace InfSec.Caesar
{
    public class CaesarAlgorithm
    {
        public string Encryption(string text, int bias, string language="en")
        {
            _currentAlphabet = getAlphabet(language);

            return new string(text
                .Select(letter => displaceLetter(letter, bias))
                .ToArray());
        }

        public string Decryption(string text, int bias, string language = "en")
        {
            _currentAlphabet = getAlphabet(language);
            
            return new string(text
                .Select(letter => displaceLetterDesc(letter, bias))
                .ToArray());
        }

        public string DecryptionByFrequency(string frequencyBase, string text, string language = "en")
        {
            _currentAlphabet = getAlphabet(language);
            var baseFrequencies = getFrequencies(frequencyBase).ToArray();
            var textFrequencies = getFrequencies(text).ToArray();
            var frequencyDistribution = new Dictionary<int, int>();

            for (int i = 0; i < textFrequencies.Count(); i++)
            {
                var currentBias = Math.Abs(_currentAlphabet.IndexOf(textFrequencies[i].Letter) -
                                    _currentAlphabet.IndexOf(baseFrequencies[i].Letter));
                if (frequencyDistribution.ContainsKey(currentBias))
                    frequencyDistribution[currentBias]++;
                else
                    frequencyDistribution[currentBias] = 1;
            }

            var bias = frequencyDistribution
                .OrderByDescending(e => e.Value)
                .First()
                .Key;

            return Decryption(text, bias, language);
        }
        
        private IEnumerable<LetterWithFrequency> getFrequencies(string text)
        {
            return text
                .ToLower()
                .ToCharArray()
                .Where(c => Char.IsLetter(c))
                .GroupBy(c => c)
                .Select(g => new LetterWithFrequency(g.Key, g.Count()))
                .OrderByDescending(s => s.Frequency)
                .ToList();
        }
        
        private char displaceLetterDesc(char letter, int bias)
        {
            if (!Char.IsLetter(letter)) return letter;
            var isUpper = Char.IsUpper(letter);
            letter = Char.ToLower(letter);

            var lettersToStart = letter - _currentAlphabet.First();

            if (bias > lettersToStart)
            {
                return (char)(_currentAlphabet.Last() - (bias - lettersToStart - 1));
            }

            var resultLetter = (char) (letter - bias);
            
            return isUpper ? 
                Char.ToUpper(resultLetter)
                : resultLetter;
        }

        private char displaceLetter(char letter, int bias)
        {
            if (!Char.IsLetter(letter)) return letter;
            var isUpper = Char.IsUpper(letter);
            letter = Char.ToLower(letter);
            
            var lettersToEnd = _currentAlphabet.Last() - letter;

            if (bias > lettersToEnd)
            {
                return (char)(_currentAlphabet.First() + (bias - lettersToEnd - 1));
            }
            
            var resultLetter = (char)(letter + bias);
            
            return isUpper ? 
                Char.ToUpper(resultLetter)
                : resultLetter;
        }

        private List<char> getAlphabet(string language)
        {
            if (language == "ru")
            {
                return Enumerable
                    .Range('а', 'я' - 'а' + 1)
                    .Select(c => (char) c)
                    .ToList();
            }

            return Enumerable
                .Range('a', 'z' - 'a' + 1)
                .Select(c => (char) c)
                .ToList();
        }

        private List<char> _currentAlphabet;
    }
}