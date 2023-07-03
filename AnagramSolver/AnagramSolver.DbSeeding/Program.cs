﻿using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.DbActions;

FileReader fileReader = new();
var stringText = fileReader.ReadFile();
var lines = Converter.ParseLines(stringText);
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