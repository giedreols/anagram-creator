using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;

namespace AnagramSolver.EF.DbFirst
{
	public class DbFirstPartOfSpeechActions : IPartOfSpeechActions
	{
		public void Add(PartOfSpeechDto item)
		{
			using var context = new AnagramSolverDataContext();

			PartOfSpeech partsOfSpeech = new()
			{
				FullWord = item.FullWord,
				Abbreviation = item.Abbreviation,
			};

			context.Add(partsOfSpeech);
			context.SaveChanges();
		}
	}
}
