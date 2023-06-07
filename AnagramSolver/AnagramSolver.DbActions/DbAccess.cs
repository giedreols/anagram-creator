using AnagramSolver.Contracts.Models;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public class DbAccess
	{
		public void InsertWord(DictionaryWordModel parameters)
		{
			var query = "INSERT INTO [dbo].[Words] ([Id], [MainForm], [OtherForm], [PartOfSpeech]) VALUES (@id, @mainForm, @otherForm, @partOfSpeech)";

			SqlCommand command = new SqlCommand
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			command.Parameters.AddRange(new[]
			{
				new SqlParameter { ParameterName = $"@id", Value = $"{parameters.Id}" },
				new SqlParameter { ParameterName = $"@mainForm", Value = $"{parameters.MainForm}" },
				new SqlParameter { ParameterName = $"@otherForm", Value = $"{parameters.OtherForm}" },
				new SqlParameter { ParameterName = $"@partOfSpeech", Value = $"{parameters.PartOfSpeech}" }
			});

			ExecuteCommand(command);
		}

		public List<DictionaryWordModel> GetWords()
		{
			string query = "SELECT DISTINCT [MainForm] AS a " +
				"FROM [AnagramSolverData].[dbo].[Words] " +
				"UNION " +
				"SELECT DISTINCT [OtherForm] AS a " +
				"FROM [AnagramSolverData].[dbo].[Words]";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			DataTable dataTable = ExecuteCommand(command);

			List<DictionaryWordModel> result = dataTable.AsEnumerable()
				.Select(row => new DictionaryWordModel(row.Field<string>("a"))).ToList();

			return result;
		}


		public List<DictionaryWordModel> GetMatchingWords(string inputWord)
		{
			string query = "SELECT DISTINCT [MainForm] AS a " +
				"FROM [AnagramSolverData].[dbo].[Words] " +
				"WHERE [MainForm] LIKE @word " +
				"UNION " +
				"SELECT DISTINCT [OtherForm] AS a " +
				"FROM [AnagramSolverData].[dbo].[Words] " +
				"WHERE [OtherForm] LIKE @word";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			command.Parameters.Add(new SqlParameter { ParameterName = $"@word", Value = $"%{inputWord}%" });

			DataTable dataTable = ExecuteCommand(command);

			List<DictionaryWordModel> result = dataTable.AsEnumerable()
				.Select(row => new DictionaryWordModel(row.Field<string>("a"))).ToList();

			return result;
		}


		public bool IsWordExists(string word)
		{
			string query = "SELECT * FROM [AnagramSolverData].[dbo].[Words] WHERE MainForm = @word OR OtherForm = @word";

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

		public int GetMaxId()
		{
			string query = "SELECT TOP 1 Id FROM Words ORDER BY Id DESC";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			DataTable dataTable = ExecuteCommand(command);

			int result = (int)dataTable.Rows[0][0];

			return result;
		}

		private static DataTable ExecuteCommand(SqlCommand command)
		{
			var connstringBuilder = new SqlConnectionStringBuilder("Data Source=.\\MSSQLSERVER01;Initial Catalog=AnagramSolverData;Integrated Security=True");

			DataTable dataTable = new();
			using SqlConnection connection = new(connstringBuilder.ConnectionString);
			connection.Open();
			command.Connection = connection;
			using SqlDataAdapter dataAdapter = new(command);
			dataAdapter.Fill(dataTable);
			return dataTable;
		}

	}
}
