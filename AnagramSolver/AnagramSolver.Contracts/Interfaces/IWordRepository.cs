using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Dtos.Obsolete;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        int Add(FullWordDto parameters);
        
        bool Update(FullWordDto parameters);

        bool Delete(string word);

        IEnumerable<string> GetWords();

        IEnumerable<string> GetMatchingWords(string inputWord);

        bool IsWordExists(string inputWord);

        IEnumerable<string> GetAnagrams(string word);
    }
}
