using AnagramSolver.Contracts.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public class CacheActions : DbAccess, ICacheActions
	{

		// NEVEIKIA (IR FRONTE NERA FUNKCIONALUMO), JEI REIKIA SURASTI ZODYNE NESANCIO ZODZIO ANAGRAMAS
		// jeigu is naujo iesko tos pacios anagramos - atnaujinti cache data? ar daryti log lentele, o cia neloginti datos isvis?
		public int InsertAnagrams(IQueryable<string> anagrams)
		{
			var connectionString = "Data Source=.\\MSSQLSERVER01;Initial Catalog=AnagramSolverData;Integrated Security=True";

			using SqlConnection connection = new(connectionString);
			connection.Open();

			string executeProcedureCode = @"USE [AnagramSolverData];
												EXEC dbo.CacheAnagrams @table;";

			DataTable anagramsTable = new();

			anagramsTable.Columns.AddRange(new DataColumn[] {
					new DataColumn("OtherForm", typeof(string)) ,
					new DataColumn("MainForm", typeof(string)) ,
					new DataColumn("PartOfSpeechId", typeof(int)) ,
					new DataColumn("AnagramId", typeof(int))
				});

			foreach (var word in anagrams)
			{
				anagramsTable.Rows.Add(word);
			}

			SqlCommand command = new(executeProcedureCode, connection)
			{
				CommandType = CommandType.Text
			};

			SqlParameter parameter = command.Parameters.AddWithValue("@table", anagramsTable);
			parameter.TypeName = "dbo.Words";

			int rowsAffected = command.ExecuteNonQuery();

			return rowsAffected;
		}

		public IEnumerable<string> GetCachedAnagrams(string word)
		{
			string query = @"DECLARE @anagramId INT;
				SELECT @anagramId = AnagramId FROM[AnagramSolverData].[dbo].[Words] 
				WHERE OtherForm = @word 
				SELECT DISTINCT 
				OtherForm FROM[AnagramSolverData].[dbo].[Words] 
				WHERE AnagramId = @anagramId; ";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			command.Parameters.AddWithValue("@word", word);

			DataTable dataTable = ExecuteCommand(command);

			return dataTable.AsEnumerable().Select(row => row.Field<string>("OtherForm"));
		}
	}
}
