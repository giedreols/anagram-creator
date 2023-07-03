namespace AnagramSolver.DbActions.Entities
{
	public class Anagram
	{
		public int Id { get; set; }

		public ICollection<Word> Words { get; set; }
	}
}
