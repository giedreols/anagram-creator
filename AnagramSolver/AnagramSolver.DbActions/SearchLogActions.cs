using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.DbActions.Models;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public class SearchLogActions : DbAccess, ISearchLogActions
	{
		public void Add(SearchLogModel item)
		{
			var query = @"USE [AnagramSolverData] GO 
						DECLARE @AnagramId INT; 
						SELECT @AnagramId = Id 
						FROM [dbo].[Anagrams]
						WHERE SearchWord = @word; 
						INSERT INTO [dbo].[SearchLog] 
								([UserIp] 
								,[TimeStamp]
								,[AnagramId]) 
						VALUES
								(@userIp
								,@timeStamp
								,@AnagramId);
						GO";

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
	}
}
