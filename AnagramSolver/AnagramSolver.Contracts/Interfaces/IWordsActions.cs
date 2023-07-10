using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordsActions
    {
        bool InsertWord(FullWordDto parameters);
        IEnumerable<FullWordDto> GetWords();
        IEnumerable<FullWordDto> GetMatchingWords(string inputWord);
        bool IsWordExists(string inputWord);

        int InsertAnagrams(WordWithAnagramsDto anagrams);
        IEnumerable<string> GetCachedAnagrams(string word);
    }
}
