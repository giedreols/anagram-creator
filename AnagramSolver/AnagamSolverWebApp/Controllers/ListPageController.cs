using AnagamSolverWebApp.Models;
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

		// error handlinga, jeigu itemu maziau negu nurodyta pageSize arba ieskomo puslapio nera
		public IActionResult Index(int currentPage = 1, int pageSize = 100)
		{
			// ar tikrai gerai gauti visus žodžius kiekvieną kartą?
			// ar geriau gauti visus ir turėti šitoj klasėj (ne metode), ar kaskart gauti nepilną sąrašą pagal indeksą?

			var allItems = _dictionary.GetWords();
			var totalItems = allItems.Count;

			int totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
			List<string> currentPageItems = allItems.OrderBy(w => w.MainForm).Skip((currentPage - 1) * pageSize).Take(pageSize).Select(w => w.MainForm).ToList();

			ListPageModel viewModel = new()
			{
				Words = currentPageItems,
				Count = totalItems,
				CurrentPage = currentPage,
			};

			return View(viewModel);
		}
	}
}
