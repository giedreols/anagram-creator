using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DictionaryFromFile;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.EF.DbFirst;
using AnagramSolver.SqlActions;

namespace AnagramSolver.DbSeeding
{
    internal static class SeedOneLineDictionary
    {

        public static void Seed()
        {
            FileReader fileReader = new();
            var stringText = fileReader.ReadFile("lithuanian-words-list.txt");
            IEnumerable<string> lines = Converter.ParseSingleLines(stringText).DistinctBy(l => l).ToList();

            var newList = new List<string>();

            foreach (var line in lines)
            {
                if(!line.Contains(".") && !line.Contains("-") && !line.Any(char.IsDigit))
                {
                    newList.Add(line);
                }
            }

            var wtable = new DbFirstWordsActions();

            foreach (var word in lines)
            {
                wtable.InsertWord(new FullWordDto(word));
            }

            Console.WriteLine("end");
        }
    }
}
