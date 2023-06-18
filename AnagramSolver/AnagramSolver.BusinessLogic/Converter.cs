using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic
{
	public static class Converter
	{
		public static List<string> ParseLines(string text)
		{
			string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

			return lines.ToList();
		}

		public static List<FullWordModel> ParseDictionaryWordsFromLines(List<string> linesList)
		{
			List<FullWordModel> wordList = new();

			try
			{
				foreach (string line in linesList)
				{
					if (line.Contains('\t'))
					{
						string[] fields = line.Split('\t');
						wordList.Add(new FullWordModel(fields[0], fields[2], fields[1]));
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

		public static List<WordWithFormsModel> ConvertStringListToAnagramWordList(List<string> stringList)
		{
			List<WordWithFormsModel> tempList = new();

			foreach (var word in stringList)
			{
				tempList.Add(new WordWithFormsModel(word));
			}

			return tempList.DistinctBy(word => word.LowerCaseForm).ToList().OrderByDescending(word => word.MainForm.Length).ToList();
		}

		public static List<WordWithFormsModel> ConvertDictionaryWordListToAnagramWordList(List<FullWordModel> list)
		{
			List<WordWithFormsModel> tempList = new();

			foreach (var word in list)
			{
				tempList.Add(new WordWithFormsModel(word.MainForm));
				if (word.OtherForm != null) tempList.Add(new WordWithFormsModel(word.OtherForm));
			}

			return tempList.DistinctBy(word => word.LowerCaseForm).ToList().OrderByDescending(word => word.MainForm.Length).ToList();
		}
	}
}
