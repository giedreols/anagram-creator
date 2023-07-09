using AnagramSolver.Contracts.Dtos;
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

		public IList<string> GetAnagrams(string inputWord)
		{
			CachedAnagramDto cachedAnagrams = _wordRepository.GetCachedAnagrams(inputWord.ToLower().Replace(" ", ""));

			IList<string> anagrams;

			if (cachedAnagrams.IsCached)
			{
				anagrams = cachedAnagrams.Anagrams.OrderBy(a => a).ToList();
			}
			else
			{
				anagrams = GenerateAnagrams(inputWord);
				_wordRepository.CacheAnagrams(new WordWithAnagramsDto(inputWord, anagrams));
			}

			return cachedAnagrams.Anagrams.OrderBy(a => a).ToList();
		}

		private IList<string> GenerateAnagrams(string inputWord)
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


