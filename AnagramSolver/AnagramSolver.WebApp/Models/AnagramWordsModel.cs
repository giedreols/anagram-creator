namespace AnagramSolver.WebApp.Models;

public class AnagramWordsModel
{
	public int Id { get; private set; }
	public string Word { get; private set; }
	public List<string> Anagrams { get; private set; }

	public AnagramWordsModel(string word, List<string> anagrams)
	{
		Word = word;
		Anagrams = anagrams;
	}
}
