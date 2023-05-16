using AnagramSolverWebApp.Models;
using BusinessLogic.DictionaryActions;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace AnagamSolverWebApp.Controllers
{
	public class ListPageController : Controller
	{
		IWordRepository _dictionary;

		// turbut ne ok kurti vis naujus zodyno objektus skirtinguose kontroleriuose?
		public ListPageController()
		{
			_dictionary = new WordDictionary(new FileReader());
		}

		public IActionResult Index(int page = 1, int pageSize = 100)
		{
			// ar tikrai gerai gauti visus žodžius kiekvieną kartą?
			// ar geriau gauti visus ir turėti šitoj klasėj, ar gal geriau kaskart gauti nepilną sąrašą pagal indeksą?

			var allWords = _dictionary.GetWords();

			var totalPages = (int)Math.Ceiling(allWords.Count / (double)pageSize);

			List<string> currentPageItems = allWords.OrderBy(w => w.LowerCaseForm).Skip((page - 1) * pageSize).Take(pageSize).Select(w => w.MainForm).ToList();

			ListPageModel viewModel = new(currentPageItems, page, allWords.Count, pageSize);

			return View(viewModel);
		}
	}
}
