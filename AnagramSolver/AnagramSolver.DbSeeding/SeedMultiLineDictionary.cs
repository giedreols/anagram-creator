using AnagramSolver.BusinessLogic.DictionaryFromFile;
using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Dtos.Obsolete;
using AnagramSolver.SqlActions;

namespace AnagramSolver.DbSeeding
{
    [Obsolete]
    internal static class SeedMultiLineDictionary
    {
        public static void Seed()
        {
            FileReader fileReader = new();
            var stringText = fileReader.ReadFile("zodynas.txt");
            var lines = Parser.ParseLinesWithTabs(stringText);
            List<FullWordDto> fullWordsFromTxtFile = Parser.ParseDictionaryWordsFromLines(lines);

            var distinctPartsOfSpeech = fullWordsFromTxtFile.Select(w => w.PartOfSpeechAbbreviation).Distinct().ToList();
            var ptable = new PartOfSpeechActions();

            foreach (var abb in distinctPartsOfSpeech)
            {
                ptable.Add(new PartOfSpeechDto { Abbreviation = abb });
            }

            var wtable = new WordsActions();

            var allMainForms = new List<FullWordDto>();

            foreach (var item in fullWordsFromTxtFile)
            {
                allMainForms.Add(new FullWordDto(item.MainForm, item.MainForm, item.PartOfSpeechAbbreviation));
            }

            var joinedList = fullWordsFromTxtFile.Concat(allMainForms).DistinctBy(w => w.OtherForm);


            foreach (var word in joinedList)
            {
                wtable.InsertWord(new FullWordDto(word.MainForm, word.OtherForm, word.PartOfSpeechAbbreviation));
            }
        }
    }
}
