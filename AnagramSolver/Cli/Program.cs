using BusinessLogic.AnagramActions;
using BusinessLogic.InputWordActions;
using BusinessLogic.DictionaryActions;
using Cli;
using Contracts.Interfaces;
using Contracts.Models;
using System.Diagnostics;

bool repeat = true;

IRenderer renderer = new CommandLineView();
Configuration configuration = new();

IWordRepository wordDictionary = new WordDictionary();
IAnagramSolver anagramSolver = new AnagramSolver(wordDictionary);

renderer.ShowHeader();


while (repeat)
{

    string word = renderer.GetWord();
    InputWord inputWord = new(word);

    inputWord.Validate(configuration.MinLength);

    if (inputWord.InvalidityReason != null)
    {
        renderer.RejectWord(inputWord.InvalidityReason);
        continue;
    }

    List<string> anagrams = anagramSolver.GetAnagrams(inputWord.MainForm);

    renderer.ShowAnagrams(anagrams.TrimIfTooManyItems(configuration.TotalAmount));

    repeat = renderer.DoRepeat();

}