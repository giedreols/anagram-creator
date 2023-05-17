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
IAnagramGenerator anagramSolver = new AnagramGenerator(wordDictionary);

renderer.ShowHeader();

while (repeat)
{
	string word = renderer.GetWord();
	InputWordModel inputWord = new(word);

	inputWord.Validate(configuration.MinLength);

	if (inputWord.InvalidityReason != null)
	{
		renderer.RejectWord(inputWord.InvalidityReason);
		continue;
	}

	List<string> anagrams = anagramSolver.GetAnagrams(inputWord.Word);

	renderer.ShowAnagrams(anagrams.TrimIfTooManyItems(configuration.TotalAmount));

	repeat = renderer.DoRepeat();

}