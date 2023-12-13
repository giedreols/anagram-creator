using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnagramSolver.WebApp.Models;

public class GetAnagramsResultModel : PageModel
{
    public AnagramViewModel? WordAndAnagrams { get; set; }

    public IList<ErrorMessageEnum> ErrorMessages { get; set; } = new List<ErrorMessageEnum>();
}