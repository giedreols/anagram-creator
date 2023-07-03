using AnagramSolver.Contracts.Dtos;

namespace AnagramSolver.Contracts.Interfaces
{
	public interface IWordsActions
	{
		void InsertWord(FullWordDto parameters);
		IEnumerable<FullWordDto> GetWords();
		IEnumerable<FullWordDto> GetMatchingWords(string inputWord);
		bool IsWordExists(string inputWord);
	}
}
