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

        private IEnumerable<char> getAlphabet(string language)
        {
            if (language == "ru")
            {
                return Enumerable
                    .Range('а', 'я' - 'а' + 1)
                    .Select(c => (char) c)
                    .ToArray();
            }

            return Enumerable
                .Range('a', 'z' - 'a' + 1)
                .Select(c => (char) c)
                .ToArray();
        }

        private IEnumerable<char> _currentAlphabet;
    }
}