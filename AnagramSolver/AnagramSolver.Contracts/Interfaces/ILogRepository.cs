using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ILogRepository
    {
        void Add(SearchLogDto item);
        SearchLogDto GetLastSearch();
        int GetSearchCount(string ipAddress);
    }
}