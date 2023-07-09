using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.IdentityModel.Tokens;

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

			IQueryable<Word> matchingItems = context.Words
						.Where(item => item.OtherForm.Contains(inputWord));

			List<FullWordDto> convertedWords = new();

			foreach (Word word in matchingItems)
			{
				convertedWords.Add(new FullWordDto(word.OtherForm));
			}

			return convertedWords;
		}

		public IEnumerable<FullWordDto> GetWords()
		{
			var FullWordList = new List<FullWordDto>();

			using var context = new AnagramSolverDataContext();

			var words = context.Words.Select(w => w.OtherForm).ToList();

			FullWordList.AddRange(words.Select(w => new FullWordDto(w)));

			return FullWordList.AsEnumerable();
		}

		// o kaip santrumpa irasyti???????????

		public void InsertWord(FullWordDto parameters)
		{
			using var context = new AnagramSolverDataContext();

			var newWord = new Word
			{
				MainForm = parameters.MainForm,
				OtherForm = parameters.OtherForm,
				OrderedForm = new(parameters.OtherForm.ToLower(System.Globalization.CultureInfo.CurrentCulture).OrderByDescending(a => a).ToArray()),
			};

			context.Words.Add(newWord);
			context.SaveChanges();
		}

		public bool IsWordExists(string inputWord)
		{
			using var context = new AnagramSolverDataContext();

			return context.Words.Any(w => w.OtherForm.Equals(inputWord));
		}


		public void AddOrderedFormForAllWords()
		{
			using var context = new AnagramSolverDataContext();

			List<Word> words = context.Words.ToList();

			foreach (Word word in words)
			{
				word.OrderedForm = new string(word.OtherForm.ToLower().OrderByDescending(c => c).ToArray());
				context.Update(word);
			}

			context.SaveChanges();
		}
	}
}
