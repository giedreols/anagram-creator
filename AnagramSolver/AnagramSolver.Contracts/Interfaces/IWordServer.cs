using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordServer
    {
        NewWordDto SaveWord(FullWordDto word, ConfigOptionsDto config);

        WordsPerPageDto GetWordsByPage(int page, int totalAmount);

        WordsPerPageDto GetMatchingWords(string inputWord, int page, int pageSize);

        IEnumerable<string> GetAnagrams(string word);

        bool DeleteWord(int wordId);

        bool UpdateWord(int wordId, string newForm);
    }
}
