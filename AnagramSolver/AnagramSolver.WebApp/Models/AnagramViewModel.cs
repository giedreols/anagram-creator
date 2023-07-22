namespace AnagramSolver.WebApp.Models;

public class AnagramViewModel
{
    public string Word { get; private set; }
    public IList<string> Anagrams { get; set; }

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
