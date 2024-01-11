using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;

namespace AnagramSolver.BusinessLogic
{
    public class LogService : ISearchLogService, IWordLogService
    {
        private readonly ISearchLogRepository _searchLogRepo;
        private readonly IWordLogRepository _wordLogRepo;
        private readonly ITimeProvider _timeProvider;
        private readonly IConfigReader _configReader;

        private readonly string _ipAddress;

        public LogService(ISearchLogRepository searchLogRepo, IWordLogRepository wordLogRepo, ITimeProvider timeProvider, IHttpContextAccessor httpContextAccessor, IConfigReader config)
        {
            _searchLogRepo = searchLogRepo;
            _wordLogRepo = wordLogRepo;
            _timeProvider = timeProvider;
            _configReader = config;
            _ipAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public async Task<bool> HasSpareSearchAsync()
        {
            int currentSearchCount = await _searchLogRepo.GetSearchCountAsync(_ipAddress);
            int newWordsCount = await _wordLogRepo.GetEntriesCountAsync(_ipAddress, WordOpEnum.Add);
            int deletedWordsCount = await _wordLogRepo.GetEntriesCountAsync(_ipAddress, WordOpEnum.Delete);
            int editedWordsCount = await _wordLogRepo.GetEntriesCountAsync(_ipAddress, WordOpEnum.Edit);

            return (currentSearchCount - newWordsCount + deletedWordsCount - editedWordsCount) < _configReader.ConfigOptions.SearchCount;
        }

        public async Task<int> LogSearchAsync(string inputWord)
        {
            return await _searchLogRepo.AddAsync(new SearchLogDto(_ipAddress, _timeProvider.UtcNow, inputWord));
        }

        public async Task<int> LogWordAsync(int wordId, WordOpEnum action)
        {
            return await _wordLogRepo.AddAsync(new WordLogDto(_ipAddress, action, wordId, _timeProvider.UtcNow));
        }

        public async Task<SearchLogDto> GetLastSearchInfoAsync()
        {
            return await _searchLogRepo.GetLastSearchAsync();
        }
    }
}
