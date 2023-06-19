using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.DbActions;
using System.Data;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
	// nėra testų, bet kadangi paskui perdarinėsiu su EntityFramework, tai gal ir nereikia, nes keisis

	public class DataBaseActions : IWordRepository
	{
		private readonly WordTableAccess _dbWordTableAccess;
		private readonly CachedWordTableAccess _dbCachedWordTableAccess;

		public DataBaseActions(WordTableAccess dbWordTableAccess, CachedWordTableAccess dbCachedWordTableAccess)
		{
			_dbWordTableAccess = dbWordTableAccess;
			_dbCachedWordTableAccess = dbCachedWordTableAccess;
		}

		public List<WordWithFormsModel> GetWords()
		{
			List<FullWordModel> words = _dbWordTableAccess.GetWords();
			return Converter.ConvertDictionaryWordListToAnagramWordList(words);
		}

		public WordsPerPageModel GetMatchingWords(string inputWord, int page = 1, int pageSize = 100)
		{
			IList<WordWithFormsModel> matchingWords = Converter.ConvertDictionaryWordListToAnagramWordList(_dbWordTableAccess.GetMatchingWords(inputWord)).OrderBy(a => a.LowerCaseForm).ToList();

			var totalPages = (int)Math.Ceiling(matchingWords.Count / (double)pageSize);

			List<string> currentPageItems = matchingWords.Skip((page - 1) * pageSize).Select(w => w.LowerCaseForm).Take(pageSize).ToList();

			return new WordsPerPageModel(currentPageItems, pageSize, matchingWords.Count);
		}

		public WordsPerPageModel GetWordsByPage(int page = 1, int pageSize = 100)
		{
			var allWords = GetWords().OrderBy(w => w.LowerCaseForm).ToList();
			var totalPages = (int)Math.Ceiling(allWords.Count / (double)pageSize);

			List<string> currentPageItems = allWords.Skip((page - 1) * pageSize).Select(w => w.LowerCaseForm).Take(pageSize).ToList();

			return new WordsPerPageModel(currentPageItems, pageSize, allWords.Count);
		}

		public bool SaveWord(string word)
		{
			if (_dbWordTableAccess.IsWordExists(word))
			{
				return false;
			}
			else
			{
				int id = _dbWordTableAccess.GetMaxId() + 1;
				_dbWordTableAccess.InsertWord(new FullWordModel(id, word));
			}

			return true;
		}

		public byte[] GetFileWithWords()
		{
			var wordList = _dbWordTableAccess.GetWords();

			IList<string> stringList = new List<string>();

			foreach (var word in wordList)
			{
				stringList.Add($"{word.MainForm}\t{word.PartOfSpeech}\t{word.OtherForm}");
			}

			string concatenatedString = string.Join("\n", stringList);

			byte[] fileBytes = Encoding.UTF8.GetBytes(concatenatedString);

			return fileBytes;
		}

		public CachedAnagramModel GetCachedAnagrams(string word)
		{
			var anagrams = _dbCachedWordTableAccess.GetCachedAnagrams(word);

			if (anagrams.Count == 0)
			{
				return new CachedAnagramModel(false, new List<string>());
			}

			if (anagrams[0] == null)
			{
				return new CachedAnagramModel(true, new List<string>());
			}

			return new CachedAnagramModel(true, anagrams);
		}

		public bool CacheAnagrams(WordWithAnagramsModel anagrams)
		{
			int rowsAffected = _dbCachedWordTableAccess.InsertAnagrams(anagrams);

			if (rowsAffected == anagrams.Anagrams.Count) return true;
			
			return false;
		}
	}
}
