using Cli;
using Contracts.Models;
using System.Text;
using BusinessLogic.AnagramActions;
using BusinessLogic.InputWordActions;


// Create configuration

bool repeat = true;
Renderer renderer = new ();
Configuration configuration = new();

try
{
    renderer.ShowHeader();
    AnagramSolver anagramSolver = new AnagramSolver();
    Console.InputEncoding = Encoding.Unicode;
    Console.OutputEncoding = Encoding.UTF8;

    while (repeat)
    {
        var word = renderer.GetWord();
        InputWord inputWord = new(word);

        inputWord.Validate(configuration.MinLength);

        if (inputWord.InvalidityReason != null)
        {
            renderer.RejectWord(inputWord.InvalidityReason);
            continue;
        }

        List<string> anagrams = anagramSolver.GetAnagrams(inputWord);

        renderer.ShowAnagrams(anagrams.ValidateAmount(configuration.TotalAmount)) ;

        repeat = renderer.DoRepeat();
    }
}
// Do not see a point of this exception eating (we get out of the loop, and the only thing that is left is exit the program 
catch (Exception)
{
    renderer.ShowError("Impossible to proceed.");
    return;
}