using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLogRepository
    {
        void Add(SearchLogDto item);
        SearchLogDto GetLastSearch();
        int GetSearchCount(string ipAddress);
    }
}