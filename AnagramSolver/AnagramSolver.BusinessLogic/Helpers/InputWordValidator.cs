using AnagramSolver.Contracts.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.BusinessLogic.Helpers
{
    public static class InputWordValidator
    {
        public static ErrorMessageEnumDto Validate(this string word, int minLength, int maxLength)
        {
            if (word.IsNullOrEmpty())
            {
                return ErrorMessageEnumDto.Empty;
            }

            if (!IsValidChars(word))
            {
                return ErrorMessageEnumDto.InvalidChars;
            }

            if (!(word.Length >= minLength))
            {
                return ErrorMessageEnumDto.TooShort;
            }

            if (!(word.Length <= maxLength))
            {
                return ErrorMessageEnumDto.TooLong;
            }

            return ErrorMessageEnumDto.Ok;
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
