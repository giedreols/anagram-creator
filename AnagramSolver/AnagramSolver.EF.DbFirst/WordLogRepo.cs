using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.DbFirst
{
    public class WordLogRepo : IWordLogRepository
    {
        private readonly AnagramSolverDataContext _context;

        public WordLogRepo(AnagramSolverDataContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(WordLogDto item)
        {
            WordLog model = new()
            {
                UserIp = item.UserIp,
                Operation = item.Operation,
                WordId = item.WordId,
                TimeStamp = DateTime.UtcNow
            };

            await _context.WordLog.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> GetEntriesCountAsync(string ipAddress, WordOpEnum operation)
        {
            var count = await _context.WordLog
                                        .Where(item => item.UserIp.Equals(ipAddress) && (int)item.Operation == (int)operation)
                                        .CountAsync();

            return count;
        }
    }
}
