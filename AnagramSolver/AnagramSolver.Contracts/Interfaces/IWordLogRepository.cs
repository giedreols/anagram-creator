using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordLogRepository
    {
        void Add(WordLogDto word);
        int GetEntriesCount(string ipAddress, WordOpEnum operation);
    }
}