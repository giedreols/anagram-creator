using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.DbActions;

namespace AnagramSolver.DbSeeding
{
	internal static class SeedOneLineDictionary
	{

		public static void Seed()
		{
			FileReader fileReader = new();
			var stringText = fileReader.ReadFile("lithuanian-words-list.txt");
			var lines = Converter.ParseSingleLines(stringText);

			var wtable = new WordsActions();

			List<string> allExistingWords = wtable.GetWords().Select(w => w.OtherForm).ToList();


			for (int i = lines.Count - 1; i >= 0; i--)
			{
				if (allExistingWords.Contains(lines[i]))
				{
					lines.RemoveAt(i);
				}
			}

			foreach (var word in lines)
			{
				wtable.InsertWord(new FullWordDto(word));
			}

			Console.WriteLine("end");
		}
	}
}
