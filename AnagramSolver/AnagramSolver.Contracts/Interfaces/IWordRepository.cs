using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        Task<int> AddAsync(FullWordDto parameters);

        Task<bool> AddListAsync(IList<FullWordDto> list);

        Task<bool> UpdateAsync(FullWordDto parameters);

        Task<bool> DeleteAsync(int wordId);

        Task<Dictionary<int, string>> GetWordsAsync();

        Task<Dictionary<int, string>> GetMatchingWordsAsync(string inputWord);

        Task<int> GetWordIdAsync(string inputWord);

        Task<IEnumerable<string>> GetAnagramsAsync(string word);
    }
}
