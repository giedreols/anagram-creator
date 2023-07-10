using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface ISearchLogActions
    {
        void Add(SearchLogDto item);
        SearchLogDto GetLastSearch();
    }
}