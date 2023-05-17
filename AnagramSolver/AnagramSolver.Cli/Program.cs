using AnagramSolver.BusinessLogic.AnagramActions;
using AnagramSolver.BusinessLogic.DictionaryActions;
using AnagramSolver.BusinessLogic.InputWordActions;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;

bool repeat = true;

IRenderer renderer = new CommandLineView();
MyConfiguration configuration = new();

IFileReader fileReader = new FileReader();
IWordRepository wordDictionary = new WordDictionary(fileReader);
IAnagramGenerator anagramSolver = new AnagramGenerator(wordDictionary);

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

	List<string> anagrams = anagramSolver.GetAnagrams(word);

	renderer.ShowAnagrams(anagrams.TrimIfTooManyItems(configuration.TotalAmount));

	repeat = renderer.DoRepeat();

}