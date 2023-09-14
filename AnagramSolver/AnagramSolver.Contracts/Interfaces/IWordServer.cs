using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordServer
    {
        NewWordDto SaveWord(FullWordDto word, ConfigOptionsDto config);

        WordsPerPageDto GetWordsByPage(int page, int totalAmount);

        WordsPerPageDto GetMatchingWords(string inputWord, int page, int pageSize);

        IEnumerable<string> GetAnagrams(string word);

        Task<IEnumerable<string>> GetAnagramsUsingAnagramicaAsync(string word);

        bool DeleteWord(int wordId);

        NewWordDto UpdateWord(int wordId, string newForm, ConfigOptionsDto config);

        int GetWordId(string word);
    }
}
