namespace AnagramSolver.DbActions.Entities
{
	public class Word
	{
		public int Id { get; set; }
		public string MainForm { get; set; }
		public string OtherForm { get; set; }

		public int? PartOfSpeechId { get; set; }
		public PartOfSpeech? PartsOfSpeech { get; set; }

		public int? AnagramId { get; set; }
		public Anagram? Anagrams { get; set; }
	}
}
