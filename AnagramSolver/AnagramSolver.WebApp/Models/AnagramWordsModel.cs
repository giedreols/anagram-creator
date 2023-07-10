namespace AnagramSolver.WebApp.Models;

public class AnagramWordsModel
{
    public string Word { get; private set; }
    public IList<string> Anagrams { get; set; }

    public AnagramWordsModel(string word, IList<string> anagrams)
    {
        Word = word;
        Anagrams = anagrams;
    }

    public AnagramWordsModel(string word)
    {
        Word = word;
    }
}
