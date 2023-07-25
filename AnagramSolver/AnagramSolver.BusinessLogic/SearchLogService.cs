using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic
{
    public class SearchLogService : ISearchLogService
    {
        private readonly ISearchLogRepository _searchLogRepo;

        public SearchLogService(ISearchLogRepository searchLogRepo)
        {
            _searchLogRepo = searchLogRepo;
        }

        // turetu ieskoti per abu logus

        public bool HasSpareSearch(string ipAddress, int maxSearchCount)
        {
            int currentSearchCount = _searchLogRepo.GetSearchCount(ipAddress);

            return currentSearchCount < maxSearchCount;
        }

        public void LogSearch(string inputWord, string ipAddress)
        {
            _searchLogRepo.Add(new SearchLogDto(ipAddress, DateTime.Now, inputWord));
        }

        public SearchLogDto GetLastSearchInfo()
        {
            return _searchLogRepo.GetLastSearch();
        }
    }
}
