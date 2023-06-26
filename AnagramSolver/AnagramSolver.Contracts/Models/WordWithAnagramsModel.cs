namespace AnagramSolver.Contracts.Models

{
	public class WordWithAnagramsModel
	{
		public string Word { get; set; }

		public IEnumerable<string> Anagrams { get; set; }

		public WordWithAnagramsModel(string word, IEnumerable<string> anagrams)
		{
			Word = word;
			Anagrams = anagrams;
		}
	}
}