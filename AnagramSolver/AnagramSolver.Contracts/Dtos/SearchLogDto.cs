namespace AnagramSolver.Contracts.Dtos
{
	public class SearchLogDto
	{
		public int Id { get; set; }

		public string UserIp { get; set; }

		public DateTime TimeStamp { get; set; }

		public string Word { get; set; }

		public SearchLogDto()
		{

		}

		public SearchLogDto(string userIp, DateTime timeStamp, string word)
		{
			UserIp = userIp;
			TimeStamp = timeStamp;
			Word = word;
		}
	}
}
