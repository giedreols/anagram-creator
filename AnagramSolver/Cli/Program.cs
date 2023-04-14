using Cli;
using Contracts.Models;
using System.Text;
using BusinessLogic.AnagramActions;
using BusinessLogic.InputWordActions;
using Contracts.Interfaces;


bool repeat = true;
IRenderer renderer = new CommandLineView();
Configuration configuration = new();

renderer.ShowHeader();
IAnagramSolver anagramSolver = new AnagramSolver();
Console.InputEncoding = Encoding.Unicode;
Console.OutputEncoding = Encoding.UTF8;

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

    renderer.ShowAnagrams(anagrams.ValidateAmount(configuration.TotalAmount));

    repeat = renderer.DoRepeat();
}

