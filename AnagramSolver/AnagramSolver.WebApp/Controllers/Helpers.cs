using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.WebApp.Controllers
{
	public class Helpers
	{
		private readonly IWordRepository _wordRepository;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public Helpers(IWordRepository wordRepository, IHttpContextAccessor httpContextAccessor)
		{
			_wordRepository = wordRepository;
			_httpContextAccessor = httpContextAccessor;
		}

		public void LogSearch(string inputWord)
		{
			string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

			_wordRepository.LogSearch(new SearchLogModel(ipAddress, DateTime.Now, inputWord));
		}
	}
}
