using AnagramSolver.BusinessLogic.DictionaryFromFile;
using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Dtos.Obsolete;
using AnagramSolver.Contracts.Interfaces;

bool repeat = true;

IRenderer renderer = new CommandLineView();
var configOptions = new MyConfiguration().ConfigOptions;

IFileReader fileReader = new FileReader();

renderer.ShowHeader();

while (repeat)
{
    string word = renderer.GetWord();

    var invalidityReason = word.Validate(configOptions.MinLength, configOptions.MaxLength);

    if (invalidityReason != null)
    {
        renderer.RejectWord(invalidityReason);
        continue;
    }

    Task<WordWithAnagramsDto?> response = new Client().ExecuteGetAnagramsAsync(word);

    response.Wait();

    if (response == null || response.Result == null)
    {
        renderer.ShowError("Anagramų parodyti nepavyko");
    }
    else
    {
        renderer.ShowAnagrams(response.Result.Anagrams.ToList().TrimIfTooManyItems(configOptions.TotalAmount));
    }

    repeat = renderer.DoRepeat();

}