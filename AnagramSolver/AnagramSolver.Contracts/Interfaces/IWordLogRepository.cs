using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordLogRepository
    {
        Task<int> AddAsync(WordLogDto word);
        Task<int> GetEntriesCountAsync(string ipAddress, WordOpEnum operation);
    }
}