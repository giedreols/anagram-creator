using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnagramSolver.WebApp.Models;

public class AnagramViewModel : PageModel
{
    public string Word { get; private set; }
    public IList<string> Anagrams { get; set; }
    public ErrorModel ErrorMessage { get; private set; }

    public AnagramViewModel(string word, IList<string> anagrams)
    {
        Word = word;
        Anagrams = anagrams;
    }

    public AnagramViewModel(string word)
    {
        Word = word;
        Anagrams = new List<string>();
    }

    public AnagramViewModel(string word, string errMessage)
    {
        Word = word;
        ErrorMessage = new ErrorModel(errMessage);
        Anagrams = new List<string>();
    }
}
