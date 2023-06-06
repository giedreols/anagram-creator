using AnagramSolver.Contracts.Models;

namespace AnagramSolver.Contracts.Interfaces
{
	public interface IWordRepository
	{
		List<WordModel> GetWords();

		bool SaveWord(string word);

		PageWordModel GetWordsByPage(int page, int totalAmount);

		byte[] GetFile();
	}
}
