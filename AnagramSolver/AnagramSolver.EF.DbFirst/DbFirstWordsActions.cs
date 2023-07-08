using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.DbFirst
{
	public class DbFirstWordsActions : IWordsActions
	{
		public DbFirstWordsActions()
		{

		}

		public IEnumerable<FullWordDto> GetMatchingWords(string inputWord)
		{
			using var context = new AnagramSolverDataContext();

			var matchingItems = context.Words
						.Where(item => item.OtherForm.Contains(inputWord));

			List<FullWordDto> convertedWords = new List<FullWordDto>();

			matchingItems.ForEachAsync(item =>
			{
				convertedWords.Add(new FullWordDto(item.OtherForm));
			});

			return convertedWords;
		}

		public IEnumerable<FullWordDto> GetWords()
		{
			var FullWordList = new List<FullWordDto>();

			using var context = new AnagramSolverDataContext();

			var words = context.Words.AsEnumerable();

			FullWordList.AddRange(words.Select(w => new FullWordDto(w.OtherForm)).ToList());

			return FullWordList.AsEnumerable();
		}

		// o kaip santrumpa ideti? ja reikia rasyt i kita lentele, o cia tik id?

		public void InsertWord(FullWordDto parameters)
		{
			using var context = new AnagramSolverDataContext();

			var newWord = new Word
			{
				MainForm = parameters.MainForm,
				OtherForm = parameters.OtherForm,
			};

			context.Words.Add(newWord);
			context.SaveChanges();
		}

		public bool IsWordExists(string inputWord)
		{
			using var context = new AnagramSolverDataContext();

			return context.Words.Any(w => w.OtherForm.ToLower().Equals(inputWord.ToLower()));
		}
	}
}
