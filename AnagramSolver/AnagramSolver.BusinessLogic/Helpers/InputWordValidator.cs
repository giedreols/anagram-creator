﻿using AnagramSolver.Contracts.Dtos;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.BusinessLogic.Helpers
{
    public static class InputWordValidator
    {
        public static string Validate(this string word, int minLength, int maxLength)
        {
            if (word.IsNullOrEmpty())
            {
                return ErrorMessages.Empty;
            }

            if (!IsValidChars(word))
            {
                return ErrorMessages.InvalidChars;
            }

            if (!(word.Length >= minLength))
            {
                return ErrorMessages.TooShort;
            }

            if (!(word.Length <= maxLength))
            {
                return ErrorMessages.TooLong;
            }

            return null;
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
