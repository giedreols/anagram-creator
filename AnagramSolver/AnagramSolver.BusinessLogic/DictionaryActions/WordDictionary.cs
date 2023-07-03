using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.DictionaryActions
{
	public class WordDictionary : IWordRepository
	{
		private readonly IFileReader _fileReader;

		public WordDictionary(IFileReader fileReader)
		{
			_fileReader = fileReader;
		}

		public List<WordWithFormsDto> GetWords()
		{
			List<string> words = ReadWords();

			return Converter.ConvertStringListToAnagramWordList(words);
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
			List<WordWithFormsDto> currentWords = GetWords();

			foreach (var existingWord in currentWords)
			{
				if (existingWord.LowerCaseForm == word.ToLower())
				{
					return false;
				}
			}

			word += $"\t.\t{word}\t.";
			_fileReader.WriteFile(word);
			return true;
		}

		public byte[] GetFileWithWords()
		{
			var fileBytes = _fileReader.GetFile();

			return fileBytes;
		}

		private List<string> ReadWords()
		{
			string text = _fileReader.ReadFile();
			List<string> linesList = Converter.ParseLines(text);
			List<string> wordList = Converter.ParseWordsFromDictionaryFile(linesList);

			return wordList;
		}

		IEnumerable<WordWithFormsDto> IWordRepository.GetWords()
		{
			throw new NotImplementedException();
		}

		public WordsPerPageDto GetMatchingWords(string inputWord, int page, int pageSize)
		{
			throw new NotImplementedException();
		}

		public CachedAnagramDto GetCachedAnagrams(string word)
		{
			throw new NotImplementedException();
		}

		public void CacheAnagrams(WordWithAnagramsDto anagrams)
		{
			throw new NotImplementedException();
		}

		public void LogSearchInfo(SearchLogDto model)
		{
			throw new NotImplementedException();
		}

		public SearchLogDto GetLastSearchInfo()
		{
			throw new NotImplementedException();
		}
	}
}
