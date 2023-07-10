using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.AnagramActions;
using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

bool repeat = true;

IRenderer renderer = new CommandLineView();
MyConfiguration configuration = new();

IFileReader fileReader = new FileReader();

renderer.ShowHeader();

while (repeat)
{
	string word = renderer.GetWord();

	var invalidityReason = word.Validate(configuration.configOptions.MinLength, configuration.configOptions.MaxLength);

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
		renderer.ShowAnagrams(response.Result.Anagrams.ToList().TrimIfTooManyItems(configuration.configOptions.TotalAmount));
	}

	repeat = renderer.DoRepeat();

}