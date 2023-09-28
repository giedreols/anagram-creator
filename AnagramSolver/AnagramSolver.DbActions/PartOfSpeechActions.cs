using AnagramSolver.Contracts.Dtos.Obsolete;
using AnagramSolver.Contracts.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.SqlActions
{
    [Obsolete("Out of date")]
    public class PartOfSpeechActions : DbAccessHelper, IPartOfSpeechRespository
    {
        public void Add(PartOfSpeechDto item)
        {
            var query = "INSERT INTO [dbo].[PartsOfSpeech] ([Abbreviation]) VALUES (@abbreviation)";

            SqlCommand command = new()
            {
                CommandText = query,
                CommandType = CommandType.Text
            };

            command.Parameters.Add(new SqlParameter("@abbreviation", item.Abbreviation));

            ExecuteCommand(command);
        }

        public int InsertPartOfSpeechIfDoesNotExistAsync(string abbreviation)
        {
            throw new NotImplementedException();
        }
    }
}
