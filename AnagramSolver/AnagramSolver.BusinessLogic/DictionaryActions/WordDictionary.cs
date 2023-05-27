using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AnagramSolver.BusinessLogic.DictionaryActions
{
	public class WordDictionary : IWordRepository
	{
		private readonly IFileReader _fileReader;

		public WordDictionary(IFileReader fileReader)
		{
			_fileReader = fileReader;
		}

		public List<AnagramWordModel> GetWords()
		{
			List<string> words = ReadWords();

			return WordActions.ConvertStringListToAnagramWordList(words);
		}

		public PageWordModel GetWordsByPage(int page = 1, int pageSize = 100)
		{
			var allWords = GetWords().OrderBy(w => w.LowerCaseForm).ToList();
			var totalPages = (int)Math.Ceiling(allWords.Count / (double)pageSize);

			List<string> currentPageItems = allWords.Skip((page - 1) * pageSize).Select(w => w.LowerCaseForm).Take(pageSize).ToList();

			return new PageWordModel(currentPageItems, pageSize, allWords.Count);
		}

		public bool SaveWord(string word)
		{
			List<AnagramWordModel> currentWords = GetWords();

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

		private List<string> ReadWords()
		{
			string text = _fileReader.ReadFile();
			List<string> linesList = WordActions.ParseLines(text);
			List<string> wordList = WordActions.ParseWordsFromDictionaryFile(linesList);

			return wordList;
		}
	}
}
