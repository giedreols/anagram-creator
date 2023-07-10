using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public class WordsActions : DbAccess, IWordsActions
	{
		public void InsertWord(FullWordDto parameters)
		{
			string query;

			SqlCommand command = new()
			{
				CommandType = CommandType.Text
			};

			command.Parameters.AddRange(new[]
			{
				new SqlParameter("@mainForm", parameters.MainForm),
				new SqlParameter("@otherForm", parameters.OtherForm),
			});

			if (!parameters.PartOfSpeechAbbreviation.IsNullOrEmpty())
			{
				query = @"DECLARE @partOfSpeechId INT; 
						SELECT @partOfSpeechId = Id 
						FROM PartsOfSpeech 
						WHERE Abbreviation = @partOfSpeech;

						IF @partOfSpeechId IS NULL
						BEGIN
						INSERT INTO PartsOfSpeech ([Abbreviation])
						VALUES (@partOfSpeech)
						SELECT @partOfSpeechId = Id 
						FROM PartsOfSpeech 
						WHERE Abbreviation = @partOfSpeech
						END

					INSERT INTO [dbo].[Words]
					    ([MainForm]
					    ,[OtherForm]
					    ,[PartOfSpeechId])
					VALUES
						(@mainForm
					   ,@otherForm
					   ,@partOfSpeechId)";

				command.Parameters.Add(new SqlParameter("@partOfSpeech", parameters.PartOfSpeechAbbreviation));
			}
			else
			{
				query = @"
					INSERT INTO [dbo].[Words]
					    ([MainForm]
					    ,[OtherForm])
					VALUES
						(@mainForm
					   ,@otherForm)";

			}

			command.CommandText = query;

			ExecuteCommand(command);
		}

		public IEnumerable<FullWordDto> GetWords()
		{
			string query = @"SELECT Words.MainForm, Words.OtherForm, PartsOfSpeech.Abbreviation
							from Words LEFT JOIN PartsOfSpeech ON PartOfSpeechId = PartsOfSpeech.Id";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			DataTable dataTable = ExecuteCommand(command);

			IEnumerable<FullWordDto> result = dataTable.AsEnumerable()
				.Select(row => new FullWordDto(row.Field<string>("MainForm"), row.Field<string>("OtherForm"), row.Field<string>("Abbreviation")));

			return result;
		}

		public IEnumerable<FullWordDto> GetMatchingWords(string inputWord)
		{
			string query = @"SELECT DISTINCT [OtherForm] AS a 
				FROM [AnagramSolverData].[dbo].[Words] 
				WHERE [OtherForm] LIKE @word ";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			command.Parameters.Add(new SqlParameter("@word", inputWord));

			DataTable dataTable = ExecuteCommand(command);

			List<FullWordDto> result = dataTable.AsEnumerable()
				.Select(row => new FullWordDto(row.Field<string>("a"))).ToList();

			return result;
		}

		public bool IsWordExists(string word)
		{
			string query = "SELECT * FROM [AnagramSolverData].[dbo].[Words] WHERE OtherForm = @word";

			SqlCommand command = new()
			{
				CommandText = query,
				CommandType = CommandType.Text
			};

			command.Parameters.AddWithValue("@word", word);

			DataTable dataTable = ExecuteCommand(command);

			if (dataTable.Rows.Count > 0)
				return true;

			return false;
		}

        bool IWordsActions.InsertWord(FullWordDto parameters)
        {
            throw new NotImplementedException();
        }
    }
}
