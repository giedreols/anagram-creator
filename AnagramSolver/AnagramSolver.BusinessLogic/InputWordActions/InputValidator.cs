using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.InputWordActions
{
	public static class InputValidator
	{
		public static void Validate(this InputWordModel word, int minLength, int maxLength = 20)
		{
			if (word.Word == null)
			{
				word.InvalidityReason = WordRejectionReasons.Empty;
				return;
			}

			if (!IsValidChars(word.Word))
			{
				word.InvalidityReason = WordRejectionReasons.InvalidChars;
				return;
			}

			if (!(word.Word.Length >= minLength))
			{
				word.InvalidityReason = WordRejectionReasons.TooShort;
				return;
			}

			if (!(word.Word.Length <= maxLength))
			{
				word.InvalidityReason = WordRejectionReasons.TooLong;
				return;
			}
		}

		public static string Validate(this string? word, int minLength, int maxLength = 20)
		{
			if (word == null)
			{
				return WordRejectionReasons.Empty;
			}

			var tempWord = new InputWordModel(word);
			tempWord.Validate(minLength, maxLength);

			return tempWord.InvalidityReason;
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
