using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        int Add(FullWordDto parameters);

        bool AddList(IList<FullWordDto> list);

        bool Update(FullWordDto parameters);

        bool Delete(int wordId);

        Task<Dictionary<int, string>> GetWordsAsync();

        Task<Dictionary<int, string>> GetMatchingWordsAsync(string inputWord);

        int IsWordExists(string inputWord);

        IEnumerable<string> GetAnagrams(string word);
    }
}
