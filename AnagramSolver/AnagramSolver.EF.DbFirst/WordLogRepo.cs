using AnagramSolver.Contracts.Dtos;
using AnagramSolver.EF.DbFirst.Entities;

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
    }
}
