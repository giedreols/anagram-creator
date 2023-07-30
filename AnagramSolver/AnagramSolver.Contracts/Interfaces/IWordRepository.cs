using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        int Add(FullWordDto parameters);

        bool Update(FullWordDto parameters);

        bool Delete(int wordId);

        Dictionary<int, string> GetWords();

        Dictionary<int, string> GetMatchingWords(string inputWord);

        bool IsWordExists(string inputWord);

        IEnumerable<string> GetAnagrams(string word);
    }
}
