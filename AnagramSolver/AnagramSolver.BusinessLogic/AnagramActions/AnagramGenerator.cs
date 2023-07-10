using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.AnagramActions
{
	public class AnagramGenerator : IAnagramGenerator
	{
		private readonly IWordRepository _wordRepository;


        public AnagramGenerator(IWordRepository wordRepository)
		{
			_wordRepository = wordRepository;
		}

		public IList<string> GetAnagrams(string inputWord)
		{
            IEnumerable<string> anagrams = _wordRepository.GetAnagrams(inputWord);

            return anagrams.ToList();
		}
	}
}


