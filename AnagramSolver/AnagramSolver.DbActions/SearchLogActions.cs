using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.SqlActions
{
    [Obsolete]
    public class SearchLogActions : DbAccessHelper, ISearchLogRepository
    {
        public void AddAsync(SearchLogDto item)
        {
            var query = @"INSERT INTO [dbo].[SearchLog]
							([UserIp]
					       ,[SearchWord]
						   ,[TimeStamp])
					   VALUES
					     (@userIp
						  ,@word
						  ,@timeStamp);";

            SqlCommand command = new()
            {
                CommandText = query,
                CommandType = CommandType.Text
            };

            command.Parameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@word", item.Word),
                new SqlParameter("@userIp", item.UserIp),
                new SqlParameter("@timeStamp", item.TimeStamp),
            });

            ExecuteCommand(command);
        }

        public SearchLogDto GetLastSearchAsync()
        {
            string queryLog = @"select top 1 
								SearchWord as word, UserIp as ip, TimeStamp as time from SearchLog 
								where TimeStamp > (SELECT DATEADD(MONTH, -1, GETDATE())) order by Id desc";

            SqlCommand commandLog = new()
            {
                CommandText = queryLog,
                CommandType = CommandType.Text
            };

            DataTable dataTableLog = ExecuteCommand(commandLog);

            if (dataTableLog.Rows.Count == 0)
            {
                return new SearchLogDto(string.Empty, DateTime.MinValue, string.Empty);
            }

            SearchLogDto result = new(
                dataTableLog.Rows[0].Field<string>("ip"), dataTableLog.Rows[0].Field<DateTime>("time"), dataTableLog.Rows[0].Field<string>("word"));

            return result;
        }

        public int GetSearchCountAsync(string ipAddress)
        {
            throw new NotImplementedException();
        }

        Task<int> ISearchLogRepository.AddAsync(SearchLogDto item)
        {
            throw new NotImplementedException();
        }

        Task<SearchLogDto> ISearchLogRepository.GetLastSearchAsync()
        {
            throw new NotImplementedException();
        }

        Task<int> ISearchLogRepository.GetSearchCountAsync(string ipAddress)
        {
            throw new NotImplementedException();
        }
    }
}