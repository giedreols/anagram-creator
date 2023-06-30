namespace AnagramSolver.Contracts.Models
{
	public class CachedAnagramModel
	{
		public bool IsCached { get; set; }

		public IList<string> Anagrams { get; set; }

		public CachedAnagramModel(bool isCached, IList<string> anagrams)
		{
			IsCached = isCached;
			Anagrams = anagrams;
		}
	}
}
