using Contracts.Models;

namespace Contracts.Interfaces
{
    public interface IPermutator
    {
        InputWord GeneratePermutations(InputWord inputWord);
    }
}
