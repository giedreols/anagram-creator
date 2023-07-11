using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordsActions
    {
        bool InsertWord(FullWordDto parameters);
        IEnumerable<string> GetWords();
        IEnumerable<string> GetMatchingWords(string inputWord);
        bool IsWordExists(string inputWord);

        int InsertAnagrams(WordWithAnagramsDto anagrams);
        IEnumerable<string> GetAnagrams(string word);
    }
}
