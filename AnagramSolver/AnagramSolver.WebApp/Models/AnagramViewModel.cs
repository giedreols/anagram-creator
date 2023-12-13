using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AnagramSolver.WebApp.Models;

public class AnagramViewModel : PageModel
{
    public string? Word { get; set; }

    public int? WordId { get; set; }

    public IEnumerable<string> Anagrams { get; set; } = new List<string>();

    public AnagramViewModel()
    {
    }

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
