namespace AnagramSolver.Contracts.Dtos
{
    public class WordListViewDto
    {
        public Dictionary<int, string> CurrentPageWords { get; private set; }

        public int CurrentPage { get; private set; }

        public int TotalWords { get; private set; }

        public int PageSize { get; private set; }

        public int TotalPages { get; private set; }

        public WordListViewDto(Dictionary<int, string> currentPageWords, int currentPage, int totalWords, int pageSize)
        {
            CurrentPageWords = currentPageWords;
            CurrentPage = currentPage;
            TotalWords = totalWords;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(decimal.Divide(TotalWords, PageSize));
        }
    }
}
