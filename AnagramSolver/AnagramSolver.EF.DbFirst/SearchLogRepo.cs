using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.DbFirst
{
    public class SearchLogRepo : ISearchLogRepository
    {
        private readonly AnagramSolverDataContext _context;

        public SearchLogRepo(AnagramSolverDataContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(SearchLogDto item)
        {
            SearchLog model = new()
            {
                SearchWord = item.Word,
                TimeStamp = item.TimeStamp,
                UserIp = item.UserIp,
            };

            await _context.SearchLog.AddAsync(model);
            return await _context.SaveChangesAsync();
        }

        public async Task<SearchLogDto> GetLastSearchAsync()
        {
            var lastItem = await _context.SearchLog.OrderByDescending(item => item.TimeStamp).FirstOrDefaultAsync();

            if (lastItem != null)
            {
                return new SearchLogDto(lastItem.UserIp, lastItem.TimeStamp, lastItem.SearchWord);
            }

            else return new SearchLogDto(string.Empty, DateTime.MinValue, string.Empty);
        }

        public async Task<int> GetSearchCountAsync(string ipAddress)
        {
            int totalCount = await _context.SearchLog.Where(item => item.UserIp.Equals(ipAddress)).CountAsync();

            return totalCount;
        }
    }
}
