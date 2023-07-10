using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        IEnumerable<WordWithFormsDto> GetWords();

        NewWordDto SaveWord(FullWordDto word, ConfigOptionsDto config);

        WordsPerPageDto GetWordsByPage(int page, int totalAmount);

        byte[] GetFileWithWords();

        WordsPerPageDto GetMatchingWords(string inputWord, int page, int pageSize);

        IEnumerable<string> GetAnagrams(string word);

        void CacheAnagrams(WordWithAnagramsDto anagrams);

        void LogSearchInfo(SearchLogDto model);

        SearchLogDto GetLastSearchInfo();
    }
}
