using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepo;

        public LogService(ILogRepository logRepo)
        {
            _logRepo = logRepo;
        }

        public bool HasSpareSearch(string ipAddress, int maxSearchCount)
        {
            int currentSearchCount = _logRepo.GetSearchCount(ipAddress);

            return currentSearchCount < maxSearchCount;
        }

        public void LogSearch(string inputWord, string ipAddress)
        {
            _logRepo.Add(new SearchLogDto(ipAddress, DateTime.Now, inputWord));
        }

        public SearchLogDto GetLastSearchInfo()
        {
            return _logRepo.GetLastSearch();
        }
    }
}
