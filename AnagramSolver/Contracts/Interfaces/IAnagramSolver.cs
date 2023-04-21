namespace Contracts.Interfaces
{
    public interface IAnagramSolver
    {
        List<string> GetAnagrams(string inputWord);

        List<List<string>> GetAnagramsMultiWord(string inputWords);

    }
}
