using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst;

namespace AnagramSolver.BusinessLogic
{
    public class WordLogService : IWordLogService
    {
        private readonly IWordLogRepository _wordLogRepo;

        public WordLogService(IWordLogRepository wordLogRepo)
        {
            _wordLogRepo = wordLogRepo;
        }

        public void LogNewWord(int wordId, string ipAddress)
        {
            _wordLogRepo.Add(new WordLogDto(ipAddress, WordOpEnum.ADD, wordId));
        }
    }
}
