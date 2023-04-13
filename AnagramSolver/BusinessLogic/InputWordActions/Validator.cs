using Contracts.Models;

namespace BusinessLogic.InputWordActions
{
    public static class Validator
    {
        public static void Validate(this InputWord word, int minLength)
        {
            if (!IsValidChars(word.MainForm))
            {
                word.InvalidityReason = WordRejectionReasons.InvalidCharacters;
            }

            if (!IsValidLength(word.MainForm, minLength))
            {
                word.InvalidityReason = WordRejectionReasons.TooShort;
            }
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
