using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;

namespace AnagramSolver.EF.DbFirst
{
    public class SearchLogRepo : ISearchLogRepository
    {
        public void Add(SearchLogDto item)
        {
            using var context = new AnagramSolverDataContext();

            SearchLog model = new()
            {
                SearchWord = item.Word,
                TimeStamp = item.TimeStamp,
                UserIp = item.UserIp,
            };

            context.SearchLog.Add(model);
            context.SaveChanges();
        }

        public SearchLogDto GetLastSearch()
        {
            using var context = new AnagramSolverDataContext();

            var lastItem = context.SearchLog.OrderByDescending(item => item.TimeStamp).FirstOrDefault();

            if (lastItem != null)
            {
                return new SearchLogDto(lastItem.UserIp, lastItem.TimeStamp, lastItem.SearchWord);
            }

            else return new SearchLogDto(string.Empty, DateTime.MinValue, string.Empty);
		}

		public int GetSearchCount(string ipAddress)
        {
            using var context = new AnagramSolverDataContext();

            int totalCount = context.SearchLog.Where(item => item.UserIp.Equals(ipAddress)).Count();
            
            return totalCount;
        }
    }
}
