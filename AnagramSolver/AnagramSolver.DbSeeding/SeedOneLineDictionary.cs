using AnagramSolver.BusinessLogic.DictionaryFromFile;
using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.EF.DbFirst;

namespace AnagramSolver.DbSeeding
{
    internal static class SeedOneLineDictionary
    {

        public static void Seed()
        {
            FileReader fileReader = new();
            var stringText = fileReader.ReadFile("lithuanian-words-list.txt");
            IEnumerable<string> lines = Parser.ParseSingleLines(stringText).DistinctBy(l => l).ToList();

            var newList = new List<string>();

            foreach (var line in lines)
            {
                if (!line.Contains(".") && !line.Contains("-") && !line.Any(char.IsDigit))
                {
                    newList.Add(line);
                }
            }

            var wtable = new WordRepo();

            foreach (var word in lines)
            {
                wtable.Add(new FullWordDto(word));
            }

            Console.WriteLine("end");
        }
    }
}
