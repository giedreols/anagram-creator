namespace AnagramSolver.Contracts.Dtos
{
	public class WordsPerPageDto
	{
		public List<string> Words { get; private set; }

		public int PageSize { get; private set; }

		public int TotalWordsCount { get; private set; }

		public WordsPerPageDto(List<string> words, int pageSize, int totalWordsCount)
		{
			Words = words;
			PageSize = pageSize;
			TotalWordsCount = totalWordsCount;
		}
	}
}