using AnagramSolver.Contracts.Models;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public static class DbAccess
	{
		public static void InsertWord(DictionaryWordModel parameters)
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

			ExecuteConnection(command);
		}

		private static void ExecuteConnection(SqlCommand command)
		{
			var connstringBuilder = new SqlConnectionStringBuilder("Data Source=.\\MSSQLSERVER01;Initial Catalog=AnagramSolverData;Integrated Security=True");
			
			DataTable dataTable = new();
			using SqlConnection connection = new(connstringBuilder.ConnectionString);
			connection.Open();
			command.Connection = connection;
			using SqlDataAdapter dataAdapter = new(command);
			dataAdapter.Fill(dataTable);
		}
	}
}
