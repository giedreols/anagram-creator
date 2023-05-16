using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnagramSolverWebApp.Models
{
	public class ListPageModel : PageModel
	{
		public List<string> CurrentPageWords { get; set; }

		public int CurrentPage { get; private set; }

		public int TotalWords { get; private set; }

		public int PageSize { get; private set; }

		public int TotalPages { get; private set; }

		public ListPageModel(List<string> currentPageWords, int currentPage, int totalWords, int pageSize)
		{
			CurrentPageWords = currentPageWords;
			CurrentPage = currentPage;
			TotalWords = totalWords;
			PageSize = pageSize;
			TotalPages = (int)Math.Ceiling(decimal.Divide(TotalWords, PageSize));
		}
	}
}
