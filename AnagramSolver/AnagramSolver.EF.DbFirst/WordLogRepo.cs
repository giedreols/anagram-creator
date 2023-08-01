using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;
using WordOpEnum = AnagramSolver.Contracts.Dtos.WordOpEnum;

namespace AnagramSolver.EF.DbFirst
{
    public class WordLogRepo : IWordLogRepository
    {
        private readonly AnagramSolverDataContext _context;

        public WordLogRepo(AnagramSolverDataContext context)
        {
            _context = context;
        }

        public void Add(WordLogDto item)
        {
            WordLog model = new()
            {
                UserIp = item.UserIp,
                Operation = (Entities.WordOpEnum)item.Operation,
                WordId = item.WordId,
                TimeStamp = DateTime.Now
            };

            _context.WordLog.Add(model);
            _context.SaveChanges();
        }

        public int GetEntriesCount(string ipAddress, WordOpEnum operation)
        {
            int opToInt = (int)operation;
            int wordsCount = _context.WordLog.Where(item => item.UserIp.Equals(ipAddress) && (int)item.Operation == opToInt).Count();

            return wordsCount;
        }
    }
}
