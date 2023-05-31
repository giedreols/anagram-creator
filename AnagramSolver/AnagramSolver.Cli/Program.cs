using AnagramSolver.BusinessLogic.AnagramActions;
using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.BusinessLogic.InputWordActions;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

bool repeat = true;

IRenderer renderer = new CommandLineView();
MyConfiguration configuration = new();

IFileReader fileReader = new FileReader();
IWordRepository wordDictionary = new WordDictionary(fileReader);

renderer.ShowHeader();

while (repeat)
{
	string word = renderer.GetWord();

	var invalidityReason = word.Validate(configuration.MinLength, configuration.MaxLength);

	if (invalidityReason != null)
	{
		renderer.RejectWord(invalidityReason);
		continue;
	}

	Task<AnagramWordsModel?> response = new Client().ExecuteGetAnagramsAsync(word);

	response.Wait();

	if (response != null)
	{
		renderer.ShowAnagrams(response.Result.Anagrams.TrimIfTooManyItems(configuration.TotalAmount));
	}
	else
	{
		renderer.ShowError("Anagramų parodyti nepavyko");
	}

	repeat = renderer.DoRepeat();

}