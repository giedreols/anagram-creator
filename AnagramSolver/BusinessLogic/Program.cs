// See https://aka.ms/new-console-template for more information
// this file should be in AnagramSolver.Cli
using BusinessLogic.AnagramActions;
using BusinessLogic.InputWordActions;
using Cli;
using Contracts.Models;


// Create configuration

bool repeat = true;
Renderer renderer = new Renderer();

try
{
    renderer.ShowHeader();
    AnagramSolver anagramSolver = new AnagramSolver();
    Console.InputEncoding = Encoding.Unicode;
    Console.OutputEncoding = Encoding.UTF8;

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

        // feels like a few too many comments?
        // generate anagrams
        List<string> anagrams = anagramSolver.GetAnagrams(inputWord);

        // show anagrams
        renderer.ShowAnagrams(anagrams.ValidateAmount());

        // ask about repeating
        repeat = renderer.DoRepeat();
    }
}
// Do not see a point of this exception eating (we get out of the loop, and the only thing that is left is exit the program 
catch (Exception)
{
    renderer.ShowError("Impossible to proceed.");
    return;
}