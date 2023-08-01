using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic
{
    public class LogService : ISearchLogService, IWordLogService
    {
        private readonly ISearchLogRepository _searchLogRepo;
        private readonly IWordLogRepository _wordLogRepo;

        public LogService(ISearchLogRepository searchLogRepo, IWordLogRepository wordLogRepo)
        {
            _searchLogRepo = searchLogRepo;
            _wordLogRepo = wordLogRepo;
        }

        public bool HasSpareSearch(string ipAddress, int maxSearchCount)
        {
            int currentSearchCount = _searchLogRepo.GetSearchCount(ipAddress);
            int newWordsCount = _wordLogRepo.GetEntriesCount(ipAddress, WordOpEnum.Add);
            int deletedWordsCount = _wordLogRepo.GetEntriesCount(ipAddress, WordOpEnum.Delete);
            int editedWordsCount = _wordLogRepo.GetEntriesCount(ipAddress, WordOpEnum.Edit);

            return (currentSearchCount - newWordsCount + deletedWordsCount - editedWordsCount) < maxSearchCount;
        }

        public void LogSearch(string inputWord, string ipAddress)
        {
            _searchLogRepo.Add(new SearchLogDto(ipAddress, DateTime.Now, inputWord));
        }

        public void LogWord(int wordId, string ipAddress, WordOpEnum action)
        {
            _wordLogRepo.Add(new WordLogDto(ipAddress, action, wordId));
        }

        public SearchLogDto GetLastSearchInfo()
        {
            return _searchLogRepo.GetLastSearch();
        }
    }
}
