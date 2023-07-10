using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DictionaryFromFile;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.SqlActions;

namespace AnagramSolver.DbSeeding
{
    internal static class SeedMultiLineDictionary
    {
        public static void Seed()
        {
            FileReader fileReader = new();
            var stringText = fileReader.ReadFile("zodynas.txt");
            var lines = Converter.ParseLinesWithTabs(stringText);
            List<FullWordDto> fullWordsFromTxtFile = Converter.ParseDictionaryWordsFromLines(lines);

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
                allMainForms.Add(new FullWordDto(item.MainForm) { OtherForm = item.MainForm, PartOfSpeechAbbreviation = item.PartOfSpeechAbbreviation });
            }

            var joinedList = fullWordsFromTxtFile.Concat(allMainForms).DistinctBy(w => w.OtherForm);


            foreach (var word in joinedList)
            {
                wtable.InsertWord(new FullWordDto(word.MainForm, word.OtherForm, word.PartOfSpeechAbbreviation));
            }
        }
    }
}
