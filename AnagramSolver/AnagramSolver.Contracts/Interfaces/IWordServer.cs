using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordServer
    {
        WordResultDto SaveWord(FullWordDto word, ConfigOptionsDto config);

        Task<WordsPerPageDto> GetWordsByPageAsync(int page, int totalAmount);

        Task<WordsPerPageDto> GetMatchingWordsAsync(string inputWord, int page, int pageSize);

        IEnumerable<string> GetAnagrams(string word);

        Task<IEnumerable<string>> GetAnagramsUsingAnagramicaAsync(string word);

        bool DeleteWord(int wordId);

        WordResultDto UpdateWord(int wordId, string newForm, ConfigOptionsDto config);

        int GetWordId(string word);
    }
}
