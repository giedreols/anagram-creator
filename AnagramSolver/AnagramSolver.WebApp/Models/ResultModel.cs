using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnagramSolver.WebApp.Models;

public class ResultModel<T> : PageModel
{
    public T Result { get; set; }

    public IList<ErrorMessageEnumModel> ErrorMessages { get; set; } = new List<ErrorMessageEnumModel>();
}