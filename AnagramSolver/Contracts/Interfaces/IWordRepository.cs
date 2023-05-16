using Contracts.Models;
using System.Collections.Immutable;

namespace Contracts.Interfaces
{
    public interface IWordRepository
    {
        List<AnagramWord> GetWords();
    }
}
