using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.Contracts.Models;
using AnagramSolver.DbActions;

FileReader fileReader = new();
var stringText = fileReader.ReadFile();
var lines = WordActions.ParseLines(stringText);
List<DictionaryWordModel> words = WordActions.ParseDictionaryWordsFromLines(lines);

int id = 0;

foreach (var word in words)
{
	word.Id = id++;
	var query = "INSERT INTO [dbo].[Words] ([Id], [MainForm], [OtherForm], [PartOfSpeech]) VALUES (@id, @mainForm, @otherForm, @partOfSpeech)";

	DbAccess.InsertWords(query, word);
}