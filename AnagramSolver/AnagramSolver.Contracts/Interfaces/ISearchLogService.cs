using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLogService
    {
        SearchLogDto GetLastSearchInfo();
        void LogSearch(string inputWord, string ipAddress);
        Task<bool> HasSpareSearchAsync(string ipAddress, int searchCount);
    }
}