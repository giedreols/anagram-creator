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

HttpClient httpClient = new();

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

	string url = "http://localhost:5254/api/anagrams/" + word;
	HttpResponseMessage response = await httpClient.GetAsync(url);

	if (response.IsSuccessStatusCode)
	{
		string content = await response.Content.ReadAsStringAsync();
		var anagramWordsModel = new Converter<AnagramWordsModel>().ConvertFromJson(content);
		renderer.ShowAnagrams(anagramWordsModel.Anagrams.TrimIfTooManyItems(configuration.TotalAmount));
	}
	else
	{
		Console.WriteLine("Request failed with status code: " + response.StatusCode);
	}

	repeat = renderer.DoRepeat();

}