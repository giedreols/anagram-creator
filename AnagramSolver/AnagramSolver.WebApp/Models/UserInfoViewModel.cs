namespace AnagramSolver.WebApp.Models;

public class UserInfoViewModel
{
    public required string Ip { get; set; }
    public required string SearchDateTime { get; set; }
	public required AnagramViewModel? AnagramWords { get; set; }
}
