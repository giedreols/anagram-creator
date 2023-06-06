using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.DbActions;
using System.Data;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
	public class WordDataBase : IWordRepository
	{
		private readonly DbAccess _dbAccess;

		public WordDataBase(DbAccess dbAccess)
		{
			_dbAccess = dbAccess;
		}

		public List<WordModel> GetWords()
		{
			List<DictionaryWordModel> words = _dbAccess.GetWords();
			return WordActions.ConvertDictionaryWordListToAnagramWordList(words);
		}

		// as taip isivaizduociau, kad sita logika kazkaip reiktu perkelti i queri ir netraukti kaskart visu zodziu, ane?
		// bet gal neverta to daryt, nes ant entityframeworko perdarinėsiu vis tiek ir keisis logika?
		public PageWordModel GetWordsByPage(int page = 1, int pageSize = 100)
		{
			var allWords = GetWords().OrderBy(w => w.LowerCaseForm).ToList();
			var totalPages = (int)Math.Ceiling(allWords.Count / (double)pageSize);

			List<string> currentPageItems = allWords.Skip((page - 1) * pageSize).Select(w => w.LowerCaseForm).Take(pageSize).ToList();

			return new PageWordModel(currentPageItems, pageSize, allWords.Count);
		}

		public bool SaveWord(string word)
		{
			if (_dbAccess.IsWordExists(word))
			{
				return false;
			}
			else
			{
				int id = _dbAccess.GetMaxId() + 1;
				_dbAccess.InsertWord(new DictionaryWordModel(id, word));
			}

			return true;
		}

		public byte[] GetFile()
		{
			var wordList = _dbAccess.GetWords();

			List<string> stringList = new List<string>();

			foreach (var word in wordList)
			{
				stringList.Add($"{word.MainForm}\t{word.PartOfSpeech}\t{word.OtherForm}");
			}

			string concatenatedString = string.Join("\n", stringList);

			byte[] fileBytes = Encoding.UTF8.GetBytes(concatenatedString);

			return fileBytes;
		}
	}
}
