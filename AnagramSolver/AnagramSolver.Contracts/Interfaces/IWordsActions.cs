using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces
{
	public interface IWordsActions
	{
		void InsertWord(FullWordModel parameters);
		IEnumerable<FullWordModel> GetWords();
		IEnumerable<FullWordModel> GetMatchingWords(string inputWord);
	}
}
