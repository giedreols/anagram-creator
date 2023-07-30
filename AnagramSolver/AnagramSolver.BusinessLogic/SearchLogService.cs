using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic
{
    public class SearchLogService : ISearchLogService
    {
        private readonly ISearchLogRepository _searchLogRepo;
        private readonly IWordLogRepository _wordLogRepo;

        public SearchLogService(ISearchLogRepository searchLogRepo, IWordLogRepository wordLogRepo)
        {
            _searchLogRepo = searchLogRepo;
            _wordLogRepo = wordLogRepo;
        }

        public bool HasSpareSearch(string ipAddress, int maxSearchCount)
        {
            int currentSearchCount = _searchLogRepo.GetSearchCount(ipAddress);
            int newWordsCount = _wordLogRepo.GetEntriesCount(ipAddress, WordOpEnum.ADD);
            int deletedWordsCount = _wordLogRepo.GetEntriesCount(ipAddress, WordOpEnum.DELETE);
            int editedWordsCount = _wordLogRepo.GetEntriesCount(ipAddress, WordOpEnum.EDIT);


            return (currentSearchCount - newWordsCount + deletedWordsCount - editedWordsCount) < maxSearchCount;
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
