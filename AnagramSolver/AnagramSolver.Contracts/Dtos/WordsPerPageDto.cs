namespace AnagramSolver.Contracts.Dtos
{
    public class WordsPerPageDto
    {
        public Dictionary<int, string> Words { get; private set; }

        public int PageSize { get; private set; }

        public int TotalWordsCount { get; private set; }

        public WordsPerPageDto(Dictionary<int, string> words, int pageSize, int totalWordsCount)
        {
            Words = words;
            PageSize = pageSize;
            TotalWordsCount = totalWordsCount;
        }
    }
}