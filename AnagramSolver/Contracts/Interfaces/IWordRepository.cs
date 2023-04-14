using Contracts.Models;

namespace Contracts.Interfaces
{
    public interface IWordRepository
    {
        IList<DictWord> GetWords();
    }
}
