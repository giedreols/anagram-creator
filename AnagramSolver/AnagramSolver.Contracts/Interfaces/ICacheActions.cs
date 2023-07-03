using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
	public interface ICacheActions
	{
		int InsertAnagrams(WordWithAnagramsDto anagrams);
		IEnumerable<string> GetCachedAnagrams(string word);
	}
}
