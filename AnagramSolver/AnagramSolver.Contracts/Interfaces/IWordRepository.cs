using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces
{
	public interface IWordRepository
	{
		IEnumerable<WordWithFormsModel> GetWords();

		bool SaveWord(string word);

		WordsPerPageModel GetWordsByPage(int page, int totalAmount);

		byte[] GetFileWithWords();

		WordsPerPageModel GetMatchingWords(string inputWord, int page, int pageSize);

		CachedAnagramModel GetCachedAnagrams(string word);

		void CacheAnagrams(WordWithAnagramsModel anagrams);

	}
}
