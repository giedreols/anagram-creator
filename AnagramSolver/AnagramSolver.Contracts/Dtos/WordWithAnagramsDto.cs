namespace AnagramSolver.Contracts.Dtos

{
	public class WordWithAnagramsDto
	{
		public string Word { get; set; }

		public IEnumerable<string> Anagrams { get; set; }

		public WordWithAnagramsDto(string word, IEnumerable<string> anagrams)
		{
			Word = word;
			Anagrams = anagrams;
		}
	}
}