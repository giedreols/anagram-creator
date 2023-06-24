namespace AnagramSolver.Contracts.Interfaces
{
	public interface ICacheActions
	{
		int InsertAnagrams(IQueryable<string> anagrams);
		IEnumerable<string> GetCachedAnagrams(string word);
	}
}
