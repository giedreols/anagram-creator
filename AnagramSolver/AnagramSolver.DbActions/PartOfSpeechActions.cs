using AnagramSolver.DbActions.Models;
using System.Data;
using System.Data.SqlClient;

namespace AnagramSolver.DbActions
{
	public class PartOfSpeechActions : DbAccess
	{
		public void Add(PartOfSpeech item)
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
