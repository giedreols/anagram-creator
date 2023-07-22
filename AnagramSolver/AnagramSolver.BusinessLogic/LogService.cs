using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic
{
    public class LogService
    {
        private readonly ILogRepository _logRepo;

        public LogService(ILogRepository logRepo)
        {
            _logRepo = logRepo;
        }

        // i app config iskelti skaiciu, kiek turi paiesku
        public bool HasSpareSearch(string ipAddress)
        {
            int searchCount = _logRepo.GetSearchCount(ipAddress);

            return searchCount < 5;
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
