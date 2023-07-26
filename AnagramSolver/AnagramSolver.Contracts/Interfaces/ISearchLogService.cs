using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLogService
    {
        SearchLogDto GetLastSearchInfo();
        void LogSearch(string inputWord, string ipAddress);
        bool HasSpareSearch(string ipAddress, int searchCount);
    }
}