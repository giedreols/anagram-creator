using AnagramSolver.Contracts.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.BusinessLogic.Helpers
{
    public static class InputWordValidator
    {
        public static ErrorMessageEnum Validate(this string word, int minLength, int maxLength)
        {
            if (word.IsNullOrEmpty())
            {
                return ErrorMessageEnum.Empty;
            }

            if (!IsValidChars(word))
            {
                return ErrorMessageEnum.InvalidChars;
            }

            if (!(word.Length >= minLength))
            {
                return ErrorMessageEnum.TooShort;
            }

            if (!(word.Length <= maxLength))
            {
                return ErrorMessageEnum.TooLong;
            }

            return ErrorMessageEnum.Ok;
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
    }
}
