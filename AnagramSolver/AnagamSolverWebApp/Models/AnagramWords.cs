using System.ComponentModel.DataAnnotations;

namespace AnagamSolverWebApp.Models;

public class AnagramWords
{
    public int Id { get; private set; }
    public string Word { get; private set; }
    public List<string> Anagrams { get; private set; }

	public AnagramWords(string word, List<string> anagrams)
	{
		Word = word;
		Anagrams = anagrams;
	}
}
