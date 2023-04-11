using Cli;
using Contracts.Data;

namespace BusinessLogic.InputWordActions
{
    public static class Validator
    {
        public static bool IsValid(this InputWord word)
        {
            var minLength = new Configuration().MinLength;
            var renderer = new Renderer();
            bool isValid = true;

            if (!IsValidChars(word.MainForm))
            {
                word.InvalidReason = WordRejectionReasons.InvalidCharacters;
                isValid = false;
            }

            if (!IsValidLength(word.MainForm, minLength))
            {
                word.InvalidReason = WordRejectionReasons.TooShort;
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
