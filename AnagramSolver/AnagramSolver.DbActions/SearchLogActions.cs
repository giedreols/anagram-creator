using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public class SearchLogActions : DbAccess, ISearchLogActions
	{
		public void Add(SearchLogDto item)
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

		public SearchLogDto GetLastSearch()
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
				return new SearchLogDto();
			}

			SearchLogDto result = new(
				dataTableLog.Rows[0].Field<string>("ip"), dataTableLog.Rows[0].Field<DateTime>("time"), dataTableLog.Rows[0].Field<string>("word"));

			string queryAnagrams = @"Declare @word nvarchar(50);
							select top 1 @word = SearchWord from SearchLog 
							where TimeStamp > (SELECT DATEADD(MONTH, -1, GETDATE()))
							order by Id desc

							SELECT w.OtherForm as anagram from Words as w
							right JOIN Anagrams as a ON w.Id = a.WordId 

							where (a.SearchWord = @word)
							and
							a.TimeStamp > (SELECT DATEADD(MINUTE, -1, GETDATE()))";

			SqlCommand commandAnagrams = new()
			{
				CommandText = queryAnagrams,
				CommandType = CommandType.Text
			};

			DataTable dataTableAnagrams = ExecuteCommand(commandAnagrams);
				
			result.Anagrams = dataTableAnagrams.AsEnumerable().Select(row => row.Field<string>("anagram")).ToList();

			return result;
		}
	}
}