using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic.DictionaryActions
{
	public class WordDictionary : IWordRepository
	{
		private readonly IFileReader _fileReader;

		public WordDictionary(IFileReader fileReader)
		{
			_fileReader = fileReader;
		}

		public List<WordWithFormsModel> GetWords()
		{
			List<string> words = ReadWords();

			return Converter.ConvertStringListToAnagramWordList(words);
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
			List<WordWithFormsModel> currentWords = GetWords();

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

		public WordsPerPageModel GetMatchingWords(string inputWord, int page, int pageSize)
		{
			throw new NotImplementedException();
		}

		public WordWithAnagramsModel GetCachedAnagrams(string word)
		{
			throw new NotImplementedException();
		}

		public void CacheAnagrams(WordWithAnagramsModel anagrams)
		{
			throw new NotImplementedException();
		}

		CachedAnagramModel IWordRepository.GetCachedAnagrams(string word)
		{
			throw new NotImplementedException();
		}
	}
}
