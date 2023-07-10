namespace AnagramSolver.WebApp.Models;

public class UserInfoModel
{
    public string Ip { get; set; }
    public string SearchDateTime { get; set; }
    public string LastWord { get; set; }
    public IList<string> Anagrams { get; set; }
}
