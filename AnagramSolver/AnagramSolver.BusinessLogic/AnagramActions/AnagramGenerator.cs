using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.AnagramActions
{
	public class AnagramGenerator : IAnagramGenerator
	{
		private List<AnagramWord> _wordList { get; set; }

		public AnagramGenerator(IWordRepository wordRepository)
		{
			_wordList = wordRepository.GetWords();
		}

		public List<string> GetAnagrams(string inputWord)
		{
			List<string> anagrams = new();
			var loweredInputWord = inputWord.ToLower().Replace(" ", "");
			var inputSequence = loweredInputWord.OrderByDescending(c => c);

			foreach (var word in _wordList)
			{
				if (inputSequence.SequenceEqual(word.OrderedForm) && word.LowerCaseForm != loweredInputWord)
				{
					anagrams.Add(word.MainForm);
				}
			}

			return anagrams;
		}
	}
}


