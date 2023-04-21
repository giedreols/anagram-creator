using BusinessLogic.AnagramActions;
using BusinessLogic.InputWordActions;
using Cli;
using Contracts.Interfaces;
using Contracts.Models;


bool repeat = true;

IRenderer renderer = new CommandLineView();
Configuration configuration = new();
IAnagramSolver anagramSolver = new AnagramSolver();

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

    //List<string> anagrams = anagramSolver.GetAnagrams(inputWord.MainForm);
    List<List<string>> anagrams = anagramSolver.GetAnagramsMultiWord(inputWord.MainForm);


    renderer.ShowAnagrams(anagrams.ValidateAmount(configuration.TotalAmount));

    repeat = renderer.DoRepeat();
}

