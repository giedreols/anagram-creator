using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.Contracts.Models;
using AnagramSolver.DbActions;
using AnagramSolver.DbActions.Models;

// duomenu issitraukimas

FileReader fileReader = new();
var stringText = fileReader.ReadFile();
var lines = Converter.ParseLines(stringText);
List<FullWordModel> fullWordsFromTxtFile = Converter.ParseDictionaryWordsFromLines(lines);


// kalbos daliu issitraukimas ir sudejimas i konteksta

var distinctPartsOfSpeech = fullWordsFromTxtFile.Select(w => w.PartOfSpeechAbbreviation).Distinct().ToList();
var ptable = new PartOfSpeechActions();

foreach (var abb in distinctPartsOfSpeech)
{
	ptable.Add(new PartOfSpeech { Abbreviation = abb });
}

var wtable = new WordsActions();

var allMainForms = new List<FullWordModel>();

foreach (var item in fullWordsFromTxtFile)
{
	allMainForms.Add(new FullWordModel(item.MainForm) { OtherForm = item.MainForm, PartOfSpeechAbbreviation = item.PartOfSpeechAbbreviation });
}

var joinedList = fullWordsFromTxtFile.Concat(allMainForms).DistinctBy(w => w.OtherForm);


foreach (var word in joinedList)
{
	wtable.InsertWord(new FullWordModel(word.MainForm, word.OtherForm, word.PartOfSpeechAbbreviation));
}