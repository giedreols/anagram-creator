using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic
{
	public static class WordActions
	{
		public static List<string> ParseLines(string text)
		{
			string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			return lines.ToList();
		}

		public static List<DictionaryWordModel> ParseDictionaryWordsFromLines(List<string> linesList)
		{
			List<DictionaryWordModel> wordList = new();

			try
			{
				foreach (string line in linesList)
				{
					if (line.Contains('\t'))
					{
						string[] fields = line.Split('\t');
						wordList.Add(new DictionaryWordModel(fields[0], fields[2], fields[1]));
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

		public static List<string> ParseWordsFromDictionaryFile(List<string> linesList)
		{
			List<string> wordList = new();

			try
			{
				foreach (string line in linesList)
				{
					if (line.Contains('\t'))
					{
						string[] fields = line.Split('\t');
						wordList.Add(fields[0]);
						wordList.Add(fields[2]);
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

		public static List<WordModel> ConvertStringListToAnagramWordList(List<string> stringList)
		{
			List<WordModel> tempList = new();

			foreach (var word in stringList)
			{
				tempList.Add(new WordModel(word));
			}

			return tempList.DistinctBy(word => word.LowerCaseForm).ToList().OrderByDescending(word => word.MainForm.Length).ToList();
		}
	}
}
