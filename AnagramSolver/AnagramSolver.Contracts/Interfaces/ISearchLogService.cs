using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLogService
    {
        Task<SearchLogDto> GetLastSearchInfoAsync();
        Task<int> LogSearchAsync(string inputWord);

        Task<bool> HasSpareSearchAsync();
    }
}