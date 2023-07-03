using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
	public interface IWordRepository
	{
		IEnumerable<WordWithFormsDto> GetWords();

		bool SaveWord(string word);

		WordsPerPageDto GetWordsByPage(int page, int totalAmount);

		byte[] GetFileWithWords();

		WordsPerPageDto GetMatchingWords(string inputWord, int page, int pageSize);

		CachedAnagramDto GetCachedAnagrams(string word);

		void CacheAnagrams(WordWithAnagramsDto anagrams);

		void LogSearchInfo(SearchLogDto model);

		SearchLogDto GetLastSearchInfo();
	}
}
