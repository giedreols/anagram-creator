using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces
{
	public interface ICacheActions
	{
		int InsertAnagrams(WordWithAnagramsModel anagrams);
		IEnumerable<string> GetCachedAnagrams(string word);
	}
}
