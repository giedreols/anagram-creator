using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;

namespace AnagramSolver.EF.DbFirst
{
    public class SearchLogRepo : ISearchLogRepository
    {
        private readonly AnagramSolverDataContext _context;

        public SearchLogRepo(AnagramSolverDataContext context)
        {
            _context = context;
        }

        public void Add(SearchLogDto item)
        {
            SearchLog model = new()
            {
                SearchWord = item.Word,
                TimeStamp = item.TimeStamp,
                UserIp = item.UserIp,
            };

            _context.SearchLog.Add(model);
            _context.SaveChanges();
        }

        public SearchLogDto GetLastSearch()
        {
            var lastItem = _context.SearchLog.OrderByDescending(item => item.TimeStamp).FirstOrDefault();

            if (lastItem != null)
            {
                return new SearchLogDto(lastItem.UserIp, lastItem.TimeStamp, lastItem.SearchWord);
            }

            else return new SearchLogDto(string.Empty, DateTime.MinValue, string.Empty);
        }

        public int GetSearchCount(string ipAddress)
        {
            int totalCount = _context.SearchLog.Where(item => item.UserIp.Equals(ipAddress)).Count();

            return totalCount;
        }
    }
}
