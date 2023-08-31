using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Dtos.Obsolete;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.SqlActions
{
    [Obsolete("Use EF.Code.First instead")]
    public class WordsActions : DbAccessHelper, IWordRepository
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

        public IEnumerable<string> GetWords()
        {
            string query = @"SELECT Words.OtherForm 
							from Words LEFT JOIN PartsOfSpeech ON PartOfSpeechId = PartsOfSpeech.Id";

            SqlCommand command = new()
            {
                CommandText = query,
                CommandType = CommandType.Text
            };

            DataTable dataTable = ExecuteCommand(command);

            IEnumerable<string> result = dataTable.AsEnumerable()
                .Select(row => row.Field<string>("OtherForm")).ToList();

            return result;
        }

        public IEnumerable<string> GetMatchingWords(string inputWord)
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

            IEnumerable<string> result = dataTable.AsEnumerable()
                .Select(row => row.Field<string>("a")).ToList();

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

        public int InsertAnagrams(WordWithAnagramsDto anagrams)
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

        private static SqlCommand CreateConnectionForWordWithAnagrams(SqlConnection connection, WordWithAnagramsDto anagrams)
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

        public IEnumerable<string> GetAnagrams(string word)
        {
            string query = @"
							DECLARE @count INT;
							set @count = (SELECT COUNT(*) from Anagrams where SearchWord = @word)

							DECLARE @timestamp datetime;
							set @timestamp = (SELECT TOP 1 TimeStamp from Anagrams where SearchWord = @word order by TimeStamp DESC)

							IF (@count = 0 OR @timestamp < (SELECT DATEADD(MINUTE, -5, GETDATE())))
							BEGIN
								SELECT null as a
							END
							ELSE 
								SELECT OtherForm as a from Words JOIN Anagrams ON Words.Id = Anagrams.WordId 
								where Anagrams.SearchWord = @word AND timestamp >= (SELECT DATEADD(MINUTE, -5, GETDATE()))";

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

        public int Add(FullWordDto parameters)
        {
            throw new NotImplementedException();
        }

        public bool Update(FullWordDto parameters)
        {
            throw new NotImplementedException();
        }

        public int Delete(string word)
        {
            throw new NotImplementedException();
        }

        Dictionary<int, string> IWordRepository.GetWords()
        {
            throw new NotImplementedException();
        }

        Dictionary<int, string> IWordRepository.GetMatchingWords(string inputWord)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int wordId)
        {
            throw new NotImplementedException();
        }

        int IWordRepository.IsWordExists(string inputWord)
        {
            throw new NotImplementedException();
        }

        public bool AddList(IList<FullWordDto> list)
        {
            throw new NotImplementedException();
        }
    }
}
