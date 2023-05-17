using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.InputWordActions
{
	public static class InputValidator
	{
		public static string Validate(this string word, int minLength, int maxLength)
		{
			if (word == null)
			{
				return WordRejectionReasons.Empty;
			}

			if (!IsValidChars(word))
			{
				return WordRejectionReasons.InvalidChars;
			}

			if (!(word.Length >= minLength))
			{
				return WordRejectionReasons.TooShort;
			}

			if (!(word.Length <= maxLength))
			{
				return WordRejectionReasons.TooLong;
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
