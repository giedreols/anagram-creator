using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;

namespace AnagramSolver.EF.DbFirst
{
	public class DbFirstCacheActions : ICacheActions
	{
		public IEnumerable<string> GetCachedAnagrams(string word)
		{
			using var context = new AnagramSolverDataContext();

			string orderedWord = new (word.OrderByDescending(w => w).ToArray());

			IEnumerable<string> anagrams = context.Words
													.Where(w => w.OrderedForm == orderedWord && w.OtherForm != word)
													.Select(x => x.OtherForm)
													.ToList();

			return anagrams;
		}

		public int InsertAnagrams(WordWithAnagramsDto anagrams)
		{
			return 1;
		}
	}
}
