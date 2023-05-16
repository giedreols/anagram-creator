using AnagramSolverWebApp.Models;
using BusinessLogic.DictionaryActions;
using Contracts.Interfaces;
using Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagamSolverWebApp.Controllers
{
	public class ListPageController : Controller
	{
		IWordRepository _dictionary;
		private List<AnagramWord> _allWords;

		public ListPageController()
		{
			_dictionary = new WordDictionary(new FileReader());
			_allWords = _dictionary.GetWords().OrderBy(w => w.LowerCaseForm).ToList();
		}

		public IActionResult Index(int page = 1, int pageSize = 100)
		{
			var totalPages = (int)Math.Ceiling(_allWords.Count / (double)pageSize);

			List<string> currentPageItems = _allWords.Skip((page - 1) * pageSize).Take(pageSize).Select(w => w.MainForm).ToList();

			ListPageModel viewModel = new(currentPageItems, page, _allWords.Count, pageSize);

			return View(viewModel);
		}
	}
}
