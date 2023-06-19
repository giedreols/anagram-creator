using AnagramSolver.Contracts.Models;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	// nėra testų, bet kadangi paskui perdarinėsiu su EntityFramework, tai gal ir nereikia, nes keisis daug kas

	public class WordTableAccess : DbAccess
	{
		public void InsertWord(FullWordModel parameters)
		{
			var query = "INSERT INTO [dbo].[Words] ([Id], [MainForm], [OtherForm], [PartOfSpeech]) VALUES (@id, @mainForm, @otherForm, @partOfSpeech)";

			SqlCommand command = new()
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

		public List<FullWordModel> GetWords()
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

			List<FullWordModel> result = dataTable.AsEnumerable()
				.Select(row => new FullWordModel(row.Field<string>("a"))).Where(w => w.MainForm != "").ToList();

			return result;
		}

		public List<FullWordModel> GetMatchingWords(string inputWord)
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

			List<FullWordModel> result = dataTable.AsEnumerable()
				.Select(row => new FullWordModel(row.Field<string>("a"))).ToList();

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
	}
}
