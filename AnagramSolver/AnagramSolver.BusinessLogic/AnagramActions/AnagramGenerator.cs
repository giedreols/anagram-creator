using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

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
			CachedAnagramModel cachedAnagrams = _wordRepository.GetCachedAnagrams(inputWord);
			IList<string> anagrams;

			if (cachedAnagrams.IsCached)
			{
				return cachedAnagrams.Anagrams.OrderBy(a => a).ToList();
			}

			anagrams = GenerateAnagrams(inputWord);

			IQueryable<string> tempAnagrams = anagrams.AsQueryable().Append(inputWord);
			_wordRepository.CacheAnagrams(tempAnagrams);

			return anagrams.OrderBy(a => a).ToList();
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


