using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces
{
	public interface IWordRepository
	{
		List<AnagramWord> GetWords();

		bool SaveWord(string word);

		PageWordModel GetWordsByPage(int page, int totalAmount);

	}
}
