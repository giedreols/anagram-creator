// See https://aka.ms/new-console-template for more information
using BusinessLogic;
using BusinessLogic.AnagramActions;
using BusinessLogic.InputWordActions;
using Cli;

bool repeat = true;
var renderer = new Renderer();
var anagramSolver = new AnagramSolver();

while (repeat)
{
    // ask for word
    var inputWord = new InputWord();
    inputWord.MainForm = renderer.GetWord();

    if (!inputWord.IsValid())
    {
        renderer.RejectWord(inputWord.InvalidReason);
        continue;
    }

    // generate anagrams
    var anagrams = anagramSolver.GetAnagrams(inputWord);

    // show anagrams
    renderer.ShowAnagrams(anagrams.ValidateAmount());

    // ask about repeating
    repeat = renderer.DoRepeat();
}