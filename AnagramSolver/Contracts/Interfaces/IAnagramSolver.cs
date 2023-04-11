using Contracts.Models;

namespace Contracts.Interfaces
{
    public interface IAnagramSolver
    {
        List<string> GetAnagrams(InputWord inputWord);
    }
}
