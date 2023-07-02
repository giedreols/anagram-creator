using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public class CacheActions : DbAccess, ICacheActions
	{
		public int InsertAnagrams(WordWithAnagramsModel anagrams)
		{
			var connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=AnagramSolverData;Integrated Security=True";

			using SqlConnection connection = new(connectionString);
			connection.Open();

			SqlCommand command;

			switch (anagrams.Anagrams.IsNullOrEmpty())
			{
				case true:
					command = CreateConnectionForWordWithoutAnagrams(connection, anagrams.Word);
					break;

				case false:
					command = CreateConnectionForWordWithAnagrams(connection, anagrams);
					break;
			}

			return command.ExecuteNonQuery();
		}

		private static SqlCommand CreateConnectionForWordWithoutAnagrams(SqlConnection connection, string word)
		{
			var query = "INSERT INTO [dbo].[Anagrams] (SearchWord) VALUES (@MainWord)";

			SqlCommand command = new(query, connection)
			{
				CommandType = CommandType.Text
			};

			SqlParameter mainWordParam = command.Parameters.AddWithValue("@MainWord", word);
			mainWordParam.SqlDbType = SqlDbType.NVarChar;

			return command;
		}

		private static SqlCommand CreateConnectionForWordWithAnagrams(SqlConnection connection, WordWithAnagramsModel anagrams)
		{
			DataTable anagramsTable = new();

			anagramsTable.Columns.AddRange(new DataColumn[] {
					new DataColumn("OtherForm", typeof(string))
			});

			foreach (var word in anagrams.Anagrams)
			{
				anagramsTable.Rows.Add(word);
			}

			SqlCommand command = new("dbo.CacheAnagrams", connection)
			{
				CommandType = CommandType.StoredProcedure
			};

			SqlParameter parameter = command.Parameters.AddWithValue("@myTable", anagramsTable);
			parameter.TypeName = "dbo.Words";
			parameter.SqlDbType = SqlDbType.Structured;

			SqlParameter mainWordParam = command.Parameters.AddWithValue("@MainWord", anagrams.Word);
			mainWordParam.SqlDbType = SqlDbType.NVarChar;

			return command;
		}

		public IEnumerable<string> GetCachedAnagrams(string word)
		{
			string query = @"
							DECLARE @count INT;
							set @count = (SELECT COUNT(*) from Anagrams where SearchWord = @word)

							DECLARE @timestamp datetime;
							set @timestamp = (SELECT TOP 1 TimeStamp from Anagrams where SearchWord = @word order by TimeStamp DESC)

							IF (@count = 0 OR @timestamp < (SELECT DATEADD(MONTH, -1, GETDATE())))
							BEGIN
								SELECT null as a
							END
							ELSE 
								SELECT OtherForm as a from Words JOIN Anagrams ON Words.Id = Anagrams.WordId where Anagrams.SearchWord = @word AND timestamp > (SELECT DATEADD(MONTH, -1, GETDATE()))";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			command.Parameters.AddWithValue("@word", word);

			DataTable dataTable = ExecuteCommand(command);

			var result = dataTable.AsEnumerable().Select(row => row.Field<string>("a"));

			return result.ToList();
		}
	}
}
