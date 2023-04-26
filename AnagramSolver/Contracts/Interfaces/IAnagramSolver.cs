using Contracts.Models;
using System.Collections.Immutable;

namespace Contracts.Interfaces
{
    public interface IAnagramSolver
    {
        List<string> GetAnagrams(string inputWord);

        List<List<string>> GetAnagramsMultiWord(string inputWords);

    }
}
