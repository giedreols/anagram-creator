using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordServer
    {
        Task<WordResultDto> SaveWordAsync(FullWordDto word);

        Task<WordsPerPageDto> GetWordsByPageAsync(int page);

        Task<WordsPerPageDto> GetMatchingWordsAsync(string inputWord, int page);

        Task<IEnumerable<string>> GetAnagramsAsync(string word);

        Task<GetAnagramsResultDto> GetAnagramsNewAsync(string word);

        Task<IEnumerable<string>> GetAnagramsUsingAnagramicaAsync(string word);

        Task<bool> DeleteWordAsync(int wordId);

        Task<WordResultDto> UpdateWordAsync(int wordId, string newForm);

        Task<int> GetWordIdAsync(string word);
    }
}
