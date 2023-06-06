using AnagramSolver.Contracts.Models;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public static class DbAccess
	{
		public static void InsertWords(string query, DictionaryWordModel parameters)
		{

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

			var connstringBuilder = new SqlConnectionStringBuilder("Data Source=.\\MSSQLSERVER01;Initial Catalog=AnagramSolverData;Integrated Security=True");

			ExecuteConnection(connstringBuilder.ConnectionString, command);
		}

		private static void ExecuteConnection(string connectionString, SqlCommand command)
		{
			DataTable dataTable = new();
			using SqlConnection connection = new(connectionString);
			connection.Open();
			command.Connection = connection;
			using SqlDataAdapter dataAdapter = new(command);
			dataAdapter.Fill(dataTable);
		}
	}
}
