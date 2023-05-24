namespace AnagramSolver.Contracts.Models
{
	public class PageWordModel
	{
		public List<string> Words { get; private set; }

		public int PageSize { get; private set; }

		public int TotalWordsCount { get; private set; }

		public PageWordModel(List<string> words, int pageSize, int totalWordsCount)
		{
			Words = words;
			PageSize = pageSize;
			TotalWordsCount = totalWordsCount;
		}
	}
}