using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.BusinessLogic
{
	public interface ILogService
	{
		SearchLogDto GetLastSearchInfo();
		void LogSearch(string inputWord, string ipAddress);
		bool HasSpareSearch(string ipAddress, int searchCount);
	}
}