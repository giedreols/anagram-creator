namespace AnagramSolver.DbActions.Models
{
	public class SearchLog
	{
		public int Id { get; set; }

		public string UserIp { get; set; }

		public DateTime TimeStamp { get; set; }

		public int AnagramId { get; set; }
	}
}
