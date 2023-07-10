using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.SqlActions
{
    public class PartOfSpeechActions : DbAccessHelper, IPartOfSpeechActions
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
    }
}
