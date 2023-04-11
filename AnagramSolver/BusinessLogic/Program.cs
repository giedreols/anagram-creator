// See https://aka.ms/new-console-template for more information
using BusinessLogic.AnagramActions;
using BusinessLogic.InputWordActions;
using Cli;
using Contracts.Models;

bool repeat = true;
Renderer renderer = new Renderer();
AnagramSolver anagramSolver = new AnagramSolver();

while (repeat)
{
    // ask for word
    InputWord inputWord = new InputWord();
    inputWord.MainForm = renderer.GetWord();

    if (!inputWord.IsValid())
    {
        renderer.RejectWord(inputWord.InvalidReason);
        continue;
    }

    // generate anagrams
    List<string> anagrams = anagramSolver.GetAnagrams(inputWord);

    // show anagrams
    renderer.ShowAnagrams(anagrams.ValidateAmount());

    // ask about repeating
    repeat = renderer.DoRepeat();
}