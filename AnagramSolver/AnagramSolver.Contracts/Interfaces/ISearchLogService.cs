using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLogService
    {
        Task<SearchLogDto> GetLastSearchInfoAsync();
        Task<int> LogSearchAsync(string inputWord, string ipAddress);
        Task<bool> HasSpareSearchAsync(string ipAddress, int searchCount);
    }
}