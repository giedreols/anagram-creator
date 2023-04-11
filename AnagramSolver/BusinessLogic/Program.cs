// See https://aka.ms/new-console-template for more information
using BusinessLogic.AnagramActions;
using BusinessLogic.InputWordActions;
using Cli;
using Contracts.Models;

bool repeat = true;
Renderer renderer = new Renderer();

try
{
    renderer.ShowHeader();
    AnagramSolver anagramSolver = new AnagramSolver();

    while (repeat)
    {
        // ask for word
        var word = renderer.GetWord();
        InputWord inputWord = new(word);

        inputWord.Validate();

        if (inputWord.InvalidityReason != null)
        {
            renderer.RejectWord(inputWord.InvalidityReason);
            continue;
        }

        // generate anagrams
        List<string> anagrams = anagramSolver.GetAnagrams(inputWord);

        // show anagrams
        renderer.ShowAnagrams(anagrams.ValidateAmount());

        // ask about repeating
        repeat = renderer.DoRepeat();
    }
}
catch (Exception)
{
    renderer.ShowError("Impossible to proceed.");
    return;
}