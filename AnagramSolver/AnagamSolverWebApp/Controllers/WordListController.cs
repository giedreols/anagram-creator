using AnagamSolverWebApp.Models;
using BusinessLogic.DictionaryActions;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagamSolverWebApp.Controllers
{
	public class WordListController : Controller
	{
		IWordRepository _dictionary;

		// turbut ne ok kurti vis naujus zodyno objektus skirtinguose kontroleriuose?
		public WordListController()
		{
			_dictionary = new WordDictionary(new FileReader());
		}

		// sarasas zodziu po 100 i viena lapa
		// prideti puslapiavima
		public IActionResult Index()
		{
			var words = _dictionary.GetWords();

			// kazkoks wtf sitas modelis... 
			WordListModel wordList = new() { Words = words.Select(word => word.MainForm).ToList() };

			return View(wordList);
		}
	}
}
