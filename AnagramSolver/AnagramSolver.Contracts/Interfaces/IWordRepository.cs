using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Dtos.Obsolete;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        bool InsertWord(FullWordDto parameters);

        IEnumerable<string> GetWords();

        IEnumerable<string> GetMatchingWords(string inputWord);

        bool IsWordExists(string inputWord);

        IEnumerable<string> GetAnagrams(string word);
    }
}
