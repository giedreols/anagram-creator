using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnagramSolver.WebApp.Models;

public class AnagramViewModel : PageModel
{
    public string Word { get; private set; }

    public int WordId { get; set; }

    public IList<string> Anagrams { get; set; }

    public AnagramViewModel(int wordId, string word, IList<string> anagrams)
    {
        WordId = wordId;
        Word = word;
        Anagrams = anagrams;
    }

    public AnagramViewModel(int wordId, string word)
    {
        WordId = wordId;
        Word = word;
        Anagrams = new List<string>();
    }

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
}
