using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.DbActions;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
	public class DataBaseActions : IWordRepository
	{
		private readonly WordsActions _dbWordTableAccess;
		private readonly CacheActions _dbCachedWordTableAccess;

		public DataBaseActions(WordsActions dbWordTableAccess, CacheActions dbCachedWordTableAccess)
		{
			_dbWordTableAccess = dbWordTableAccess;
			_dbCachedWordTableAccess = dbCachedWordTableAccess;
		}

		public IEnumerable<WordWithFormsModel> GetWords()
		{
			List<FullWordModel> words = _dbWordTableAccess.GetWords().ToList();
			return Converter.ConvertDictionaryWordListToAnagramWordList(words);
		}

		public WordsPerPageModel GetMatchingWords(string inputWord, int page = 1, int pageSize = 100)
		{
			IList<WordWithFormsModel> matchingWords = Converter.ConvertDictionaryWordListToAnagramWordList(_dbWordTableAccess.
				GetMatchingWords(inputWord).ToList()).OrderBy(a => a.LowerCaseForm).ToList();

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
				_dbWordTableAccess.InsertWord(new FullWordModel(word));
			}

			return true;
		}

		public byte[] GetFileWithWords()
		{
			var wordList = _dbWordTableAccess.GetWords();

			IList<string> stringList = new List<string>();

			foreach (var word in wordList)
			{
				stringList.Add($"{word.MainForm}\t{word.PartOfSpeechAbbreviation}\t{word.OtherForm}");
			}

			string concatenatedString = string.Join("\n", stringList);

			byte[] fileBytes = Encoding.UTF8.GetBytes(concatenatedString);

			return fileBytes;
		}

		public CachedAnagramModel GetCachedAnagrams(string word)
		{
			List<string> anagrams = _dbCachedWordTableAccess.GetCachedAnagrams(word).ToList();

			if(anagrams.IsNullOrEmpty())
			{
				return new CachedAnagramModel(true, new List<string>());
			}

			if (anagrams[0] == null)
			{
				return new CachedAnagramModel(false, new List<string>());
			}

			return new CachedAnagramModel(true, anagrams.ToList());
		}

		public void CacheAnagrams(WordWithAnagramsModel anagrams)
		{
			_dbCachedWordTableAccess.InsertAnagrams(anagrams);
		}
	}
}
