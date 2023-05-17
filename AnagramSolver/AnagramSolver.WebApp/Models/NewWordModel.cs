namespace AnagramSolver.WebApp.Models;

public class NewWordModel
{
	public int Id { get; set; }
	public string Word { get; set; }

	public List<string> Anagrams { get; set; }
}