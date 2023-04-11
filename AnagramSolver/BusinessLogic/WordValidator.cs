using Cli;
using Contracts.Data;

namespace BusinessLogic
{
    public static class WordValidator
    {
        public static bool IsValid(this string word)
        {
            var minLength = new Configuration().MinLength;
            var renderer = new Renderer();
            bool isValid = true;

            if (!IsValidChars(word))
            {
                renderer.RejectWord(WordRejectionReasons.InvalidCharacters);
                isValid = false;
            }

            if (!IsValidLength(word, minLength))
            {
                renderer.RejectWord(WordRejectionReasons.TooShort);
                isValid = false;
            }
            return isValid;
        }


        private static bool IsValidChars(string word)
        {
            bool isValid = false;

            foreach (char letter in word)
            {
                if (char.IsDigit(letter) || char.IsLetter(letter) || letter.Equals('-'))
                {
                    isValid = true;
                }

                else
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool IsValidLength(string word, int minLength)
        {
            return word.Length >= minLength;
        }
    }
}
