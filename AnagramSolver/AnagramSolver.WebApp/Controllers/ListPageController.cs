using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
	public class ListPageController : Controller
	{
		private IWordRepository _dictionary;
		private List<AnagramWord> _allWords;
		private MyConfiguration _config;

		public ListPageController(IWordRepository dictionary, MyConfiguration congif)
		{
			_dictionary = dictionary;
			_config = congif;
		}

		public IActionResult Index(int page = 1)
		{
			int pageSize = _config.TotalAmount;

			_allWords = _dictionary.GetWords().OrderBy(w => w.LowerCaseForm).ToList();

			var totalPages = (int)Math.Ceiling(_allWords.Count / (double)pageSize);

			List<string> currentPageItems = _allWords.Skip((page - 1) * pageSize).Take(pageSize).Select(w => w.MainForm).ToList();

			ListPageModel viewModel = new(currentPageItems, page, _allWords.Count, pageSize);

			return View(viewModel);
		}
	}
}
