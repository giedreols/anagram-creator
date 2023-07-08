using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
	public class DataBaseActions : IWordRepository
	{
		private readonly IWordsActions _dbWordTableAccess;
		private readonly ICacheActions _dbCachedWordTableAccess;
		private readonly ISearchLogActions _dbSearchLogActions;

		public DataBaseActions(ICacheActions dbCachedWordTableAccess, IWordsActions dbWordTableAccess, ISearchLogActions searchLogActions)
		{
			_dbWordTableAccess = dbWordTableAccess;
			_dbCachedWordTableAccess = dbCachedWordTableAccess;
			_dbSearchLogActions = searchLogActions;
		}

		public IEnumerable<WordWithFormsDto> GetWords()
		{
			List<FullWordDto> words = _dbWordTableAccess.GetWords().ToList();
			return Converter.ConvertDictionaryWordListToAnagramWordList(words);
		}

		public WordsPerPageDto GetMatchingWords(string inputWord, int page = 1, int pageSize = 100)
		{
			IList<WordWithFormsDto> matchingWords = Converter.ConvertDictionaryWordListToAnagramWordList(_dbWordTableAccess.
				GetMatchingWords(inputWord).ToList()).OrderBy(a => a.LowerCaseForm).ToList();

			var totalPages = (int)Math.Ceiling(matchingWords.Count / (double)pageSize);

			List<string> currentPageItems = matchingWords.Skip((page - 1) * pageSize).Select(w => w.LowerCaseForm).Take(pageSize).ToList();

			return new WordsPerPageDto(currentPageItems, pageSize, matchingWords.Count);
		}

		public WordsPerPageDto GetWordsByPage(int page = 1, int pageSize = 100)
		{
			var allWords = GetWords().OrderBy(w => w.LowerCaseForm).ToList();
			var totalPages = (int)Math.Ceiling(allWords.Count / (double)pageSize);

			List<string> currentPageItems = allWords.Skip((page - 1) * pageSize).Select(w => w.LowerCaseForm).Take(pageSize).ToList();

			return new WordsPerPageDto(currentPageItems, pageSize, allWords.Count);
		}

		public bool SaveWord(string word)
		{
			if (_dbWordTableAccess.IsWordExists(word))
			{
				return false;
			}
			else
			{
				_dbWordTableAccess.InsertWord(new FullWordDto(word));
			}
			return true;
		}

		public byte[] GetFileWithWords()
		{
			IEnumerable<FullWordDto> wordList = _dbWordTableAccess.GetWords();

			IList<string> stringList = new List<string>();

			foreach (var word in wordList)
			{
				stringList.Add($"{word.MainForm}\t{word.PartOfSpeechAbbreviation}\t{word.OtherForm}");
			}

			string concatenatedString = string.Join("\n", stringList);

			byte[] fileBytes = Encoding.UTF8.GetBytes(concatenatedString);

			return fileBytes;
		}

		public CachedAnagramDto GetCachedAnagrams(string word)
		{
			IList<string> anagrams = _dbCachedWordTableAccess.GetCachedAnagrams(word).ToList();

			if (anagrams.IsNullOrEmpty())
			{
				return new CachedAnagramDto(true, new List<string>());
			}

			if (anagrams[0] == null)
			{
				return new CachedAnagramDto(false, new List<string>());
			}

			return new CachedAnagramDto(true, anagrams);
		}

		public void CacheAnagrams(WordWithAnagramsDto anagrams)
		{
			_dbCachedWordTableAccess.InsertAnagrams(anagrams);
		}

		public void LogSearchInfo(SearchLogDto model)
		{
			_dbSearchLogActions.Add(model);
		}

		public SearchLogDto GetLastSearchInfo()
		{
			return _dbSearchLogActions.GetLastSearch();
		}
	}
}
