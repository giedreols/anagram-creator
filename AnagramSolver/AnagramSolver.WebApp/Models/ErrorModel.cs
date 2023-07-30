using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnagramSolver.WebApp.Models
{
    public class ErrorModel : PageModel
    {
        public string Message { get; private set; }

        public ErrorModel(string message)
        {
            Message = message;
        }
    }
}
