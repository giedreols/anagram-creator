using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic
{
    public class WordLogService : IWordLogService
    {
        private readonly IWordLogRepository _wordLogRepo;

        public WordLogService(IWordLogRepository wordLogRepo)
        {
            _wordLogRepo = wordLogRepo;
        }

        public void Log(int wordId, string ipAddress, WordOpEnum action)
        {
            _wordLogRepo.Add(new WordLogDto(ipAddress, action, wordId));
        }
    }
}
