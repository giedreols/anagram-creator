using AnagramSolver.Contracts.Models;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public class CachedWordTableAccess : DbAccess
	{
		public int InsertAnagrams(WordWithAnagramsModel wordAndAnagrams)
		{
			var connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=AnagramSolverData;Integrated Security=True";

			using (SqlConnection connection = new(connectionString))
			{
				connection.Open();

				DataTable anagramsTable = new();
				anagramsTable.Columns.Add("MainWord", typeof(string));
				anagramsTable.Columns.Add("Anagram", typeof(string));

				if (wordAndAnagrams.Anagrams.Count == 0)
				{
					anagramsTable.Rows.Add(wordAndAnagrams.Word);
				}
				else
				{
					foreach (var word in wordAndAnagrams.Anagrams)
					{
						anagramsTable.Rows.Add(wordAndAnagrams.Word, word);
					}
				}

				SqlCommand command = new("InsertCachedAnagrams", connection);
				command.CommandType = CommandType.StoredProcedure;

				SqlParameter parameter = command.Parameters.AddWithValue("@anagrams", anagramsTable);
				parameter.SqlDbType = SqlDbType.Structured;
				parameter.TypeName = "dbo.CachedAnagramsType";

				int rowsAffected = command.ExecuteNonQuery();

				return rowsAffected;
			}
		}

		public IList<string> GetCachedAnagrams(string word)
		{
			string query = "SELECT DISTINCT [Anagram] AS a " +
				"FROM [AnagramSolverData].[dbo].[CachedAnagrams] " +
				"WHERE [MainWord] = @word ";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			command.Parameters.AddWithValue("@word", word);

			DataTable dataTable = ExecuteCommand(command);

			return dataTable.AsEnumerable().Select(row => row.Field<string>("a")).ToList();
		}

		public bool IsAnagramsCached(string word)
		{
			string query = "SELECT * FROM [AnagramSolverData].[dbo].[CachedAnagrams] WHERE MainWord = @word";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			command.Parameters.Add(new SqlParameter { ParameterName = $"@word", Value = $"{word}" });

			DataTable dataTable = ExecuteCommand(command);

			if (dataTable.Rows.Count > 0)
				return true;

			return false;
		}
	}
}
