using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces
{
	public interface ICachedWordRepository
	{
		List<WordWithFormsModel> GetCachedAnagrams(string word);

		void SaveAnagrams(string mainWord, List<string> anagrams);

		bool IsWordExists(string word);
	}
}
