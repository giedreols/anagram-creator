using Contracts.Interfaces;
using Contracts.Models;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace BusinessLogic.DictionaryActions
{
	public class WordDictionary : IWordRepository
	{
		private readonly IFileReader _fileReader;

		public WordDictionary(IFileReader fileReader)
		{
			_fileReader = fileReader;
		}

		// kaip logiskiau - ar metode saugot kintamuosius, ar klaseje? privacius metodus geriau void ar return tipo daryt?
		// ar man isvis yra prasme turet zodyno modeli, kuri iskart konvertuoju i anagramos modeli? ar geriau stringa iskart konvertuoti i anagramos modeli?
		// o gal yra prasme - nes bus paprasciau kitokio formato zodyno faila naudot?
		public ImmutableList<AnagramWord> GetWords()
		{
			ReadOnlyCollection<DictWord> words = ReadWords();
			return ConvertDictionaryWordsToAnagramWords(words);
		}

		private static ImmutableList<AnagramWord> ConvertDictionaryWordsToAnagramWords(ReadOnlyCollection<DictWord> words)
		{
			List<AnagramWord> tempList = new();

			foreach (var word in words)
			{
				tempList.Add(new AnagramWord(word.MainForm));
				tempList.Add(new AnagramWord(word.AnotherForm));
			}

			tempList = (tempList.DistinctBy(word => word.LowerCaseForm).ToList()).OrderByDescending(word => word.MainForm.Length).ToList();

			return tempList.ToImmutableList();
		}

		private ReadOnlyCollection<DictWord> ReadWords()
		{
			string text = _fileReader.ReadFile();

			List<string> linesList = ParseLines(text);

			List<DictWord> wordList = ParseWords(linesList);

			return new ReadOnlyCollection<DictWord>(wordList.Distinct().ToList());
		}

		private static List<string> ParseLines(string text)
		{
			string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			return lines.ToList();
		}

		private static List<DictWord> ParseWords(List<string> linesList)
		{
			List<DictWord> wordList = new();

			try
			{
				foreach (string line in linesList)
				{
					if (line.Contains('\t'))
					{
						string[] fields = line.Split('\t');
						DictWord word = new(fields[0], fields[1], fields[2]);
						wordList.Add(word);
					}
				}
				if (wordList.Count == 0) { throw new Exception("Dictionary is empty"); }
			}

			catch
			{
				throw new IndexOutOfRangeException("Dictionary is empty of file structure is incorrect; Each line should contain at least 3 fields, separated by tabs");
			}

			return wordList;
		}
	}
}
