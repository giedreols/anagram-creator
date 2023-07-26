using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;
using WordOpEnum = AnagramSolver.Contracts.Dtos.WordOpEnum;

namespace AnagramSolver.EF.DbFirst
{
    public class WordLogRepo : IWordLogRepository
    {
        public void Add(WordLogDto item)
        {
            using var context = new AnagramSolverDataContext();

            WordLog model = new()
            {
                UserIp = item.UserIp,
                Operation = (Entities.WordOpEnum)item.Operation,
                WordId = item.WordId,
                TimeStamp = DateTime.Now
            };

            context.WordLog.Add(model);
            context.SaveChanges();
        }

        public int GetEntriesCount(string ipAddress, WordOpEnum operation)
        {
            using var context = new AnagramSolverDataContext();

            int opToInt = (int)operation;
            int wordsCount = context.WordLog.Where(item => item.UserIp.Equals(ipAddress) && (int)item.Operation == opToInt).Count();

            return wordsCount;
        }
    }
}
