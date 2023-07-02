using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public class SearchLogActions : DbAccess, ISearchLogActions
	{
		public void Add(SearchLogModel item)
		{
			var query = @"INSERT INTO [dbo].[SearchLog]
							([UserIp]
					       ,[SearchWord]
						   ,[TimeStamp])
					   VALUES
					     (@userIp
						  ,@word
						  ,@timeStamp);

						DECLARE @LatestSearchLogId INT;
						DECLARE @AnagramIds TABLE (anagramId INT);

						SELECT TOP 1 @LatestSearchLogId = Id 
						FROM SearchLog
						ORDER BY Id DESC;

						INSERT INTO @AnagramIds (anagramId)
						SELECT Id FROM Anagrams 
						WHERE SearchWord = @word AND timestamp > (SELECT DATEADD(MONTH, -1, GETDATE()));

						INSERT INTO [dbo].[SearchedAnagramsLog]
						([AnagramId], [SearchLogId])
						SELECT anagramId, @LatestSearchLogId
						FROM @AnagramIds;";

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

		public SearchLogModel GetLastSearch()
		{
			string query = @"declare @SearchId INT
							select TOP 1 @SearchId = Id from SearchLog order by Id desc
							SELECT s.UserIp as ip, s.TimeStamp as time, s.SearchWord as word, w.OtherForm as anagram
							from SearchLog as s
							left join SearchedAnagramsLog as sa on s.Id = sa.SearchLogId
							left join Anagrams as a on a.Id = sa.AnagramId
							left join Words as w on a.WordId = w.Id
							where s.Id = @SearchId 
							AND
							s.TimeStamp > (SELECT DATEADD(MONTH, -1, GETDATE()))";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			DataTable dataTable = ExecuteCommand(command);

			if (dataTable.Rows.Count == 0)
			{
				return new SearchLogModel();
			}

			SearchLogModel result = new(
				dataTable.Rows[0].Field<string>("ip"), dataTable.Rows[0].Field<DateTime>("time"), dataTable.Rows[0].Field<string>("word"));

			result.Anagrams = dataTable.AsEnumerable().Select(row => row.Field<string>("anagram")).ToList();

			return result;
		}
	}
}