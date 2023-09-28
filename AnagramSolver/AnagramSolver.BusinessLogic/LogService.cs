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

        public async Task<bool> HasSpareSearchAsync(string ipAddress, int maxSearchCount)
        {
            int currentSearchCount = await _searchLogRepo.GetSearchCountAsync(ipAddress);
            int newWordsCount = await _wordLogRepo.GetEntriesCountAsync(ipAddress, WordOpEnum.Add);
            int deletedWordsCount = await _wordLogRepo.GetEntriesCountAsync(ipAddress, WordOpEnum.Delete);
            int editedWordsCount = await _wordLogRepo.GetEntriesCountAsync(ipAddress, WordOpEnum.Edit);

            return (currentSearchCount - newWordsCount + deletedWordsCount - editedWordsCount) < maxSearchCount;
        }

        public async Task<int> LogSearchAsync(string inputWord, string ipAddress)
        {
            return await _searchLogRepo.AddAsync(new SearchLogDto(ipAddress, DateTime.UtcNow, inputWord));
        }

        public async Task<int> LogWordAsync(int wordId, string ipAddress, WordOpEnum action)
        {
            return await _wordLogRepo.AddAsync(new WordLogDto(ipAddress, action, wordId));
        }

        public async Task<SearchLogDto> GetLastSearchInfoAsync()
        {
            return await _searchLogRepo.GetLastSearchAsync();
        }
    }
}
