namespace AnagramSolver.Contracts.Models
{
	public class SearchLogModel
	{
		public int Id { get; set; }

		public string UserIp { get; set; }

		public DateTime TimeStamp { get; set; }

		public string Word { get; set; }

        public SearchLogModel(string userIp, DateTime timeStamp, string word)
        {
			UserIp = userIp;
			TimeStamp = timeStamp;
			Word = word;
        }
    }
}
