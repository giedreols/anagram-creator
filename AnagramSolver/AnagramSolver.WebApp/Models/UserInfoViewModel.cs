namespace AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class UserInfoViewModel : PageModel
{
    public required string Ip { get; set; }
    public required string SearchDateTime { get; set; }
    public required AnagramViewModel? AnagramWords { get; set; }
}
