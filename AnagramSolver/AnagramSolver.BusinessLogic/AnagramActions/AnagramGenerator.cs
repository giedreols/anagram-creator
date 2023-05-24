using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.AnagramActions
{
	public class AnagramGenerator : IAnagramGenerator
	{
		private readonly IWordRepository _wordRepository;

		public AnagramGenerator(IWordRepository wordRepository)
		{
			_wordRepository = wordRepository;
		}

		public List<string> GetAnagrams(string inputWord)
		{
			var wordList = _wordRepository.GetWords();
			List<string> anagrams = new();
			var loweredInputWord = inputWord.ToLower().Replace(" ", "");
			var inputSequence = loweredInputWord.OrderByDescending(c => c);

			foreach (var word in wordList)
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


