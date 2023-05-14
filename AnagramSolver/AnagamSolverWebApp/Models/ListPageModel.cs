using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnagamSolverWebApp.Models
{
	public class ListPageModel : PageModel
	{
		public List<string> Words { get; set; }
		
		public int CurrentPage { get; set; }

		public int Count { get; set; }

		public int PageSize { get; set; }

		public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

	}
}
