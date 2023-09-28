using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLogRepository
    {
        Task<int> AddAsync(SearchLogDto item);
        Task<SearchLogDto> GetLastSearchAsync();
        Task<int> GetSearchCountAsync(string ipAddress);
    }
}