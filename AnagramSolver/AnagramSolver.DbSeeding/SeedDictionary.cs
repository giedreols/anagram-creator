using AnagramSolver.BusinessLogic.DictionaryFromFile;
using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.DbSeeding
{
    [Obsolete]
    internal class SeedDictionary
    {
        private readonly IWordRepository _wordRepo;
        private readonly IPartOfSpeechRespository _partOfSpeechRepo;

        public SeedDictionary(IWordRepository wordRepository, IPartOfSpeechRespository partOfSpeechRespository)
        {
            _wordRepo = wordRepository;
            _partOfSpeechRepo = partOfSpeechRespository;
        }

        public async Task SeedAsync()
        {
            FileReader fileReader = new();
            var stringText = fileReader.ReadFile("zodynas.txt");
            //var stringText = fileReader.ReadFile("lithuanian-words-list.txt");
            IEnumerable<string> lines = Parser.ParseSingleLines(stringText).DistinctBy(l => l).ToList();

            var newList = new List<FullWordDto>();

            foreach (var line in lines)
            {
                if (!line.Contains('.') && !line.Contains('-') && line.Any(char.IsLetter))
                {
                    string[] values = line.Split('\t');
                    newList.Add(new FullWordDto(mainForm: values[0], otherForm: values[2], partOfSpeechAbbreviation: values[1]));

                    // newList.Add(new FullWordDto(line));
                }
            }

            foreach (var word in newList)
            {
                word.PartOfSpeechId = await _partOfSpeechRepo.InsertPartOfSpeechIfDoesNotExistAsync(word.PartOfSpeechAbbreviation);
            }

            string result = await _wordRepo.AddListAsync(newList) ? "Success." : "Failed.";

            Console.WriteLine("Seeding finished. Result: " + result);
        }
    }
}
