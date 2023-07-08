using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.EF.DbFirst
{
	public class DbFirstCacheActions : ICacheActions
	{
		public IEnumerable<string> GetCachedAnagrams(string word)
		{
			using var context = new AnagramSolverDataContext();
			//throw new NotImplementedException();

			return new List<string>();

		}

		public int InsertAnagrams(WordWithAnagramsDto anagrams)
		{
			//throw new NotImplementedException();

			return 1;
		}
	}
}
