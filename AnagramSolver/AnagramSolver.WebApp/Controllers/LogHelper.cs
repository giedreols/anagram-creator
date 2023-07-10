using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.WebApp.Controllers
{
    public class LogHelper : ILogHelper
    {
        private readonly IWordRepository _wordRepo;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogHelper(IWordRepository wordRepo, IHttpContextAccessor httpContextAccessor)
        {
            _wordRepo = wordRepo;
            _httpContextAccessor = httpContextAccessor;
        }

        public void LogSearch(string inputWord)
        {
            string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            _wordRepo.LogSearchInfo(new SearchLogDto(ipAddress, DateTime.Now, inputWord));
        }
    }
}
