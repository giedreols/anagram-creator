using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
	public class WordListController : Controller
	{
		private readonly IWordRepository _dictionary;
		private readonly MyConfiguration _config;

		public WordListController(IWordRepository dictionary, MyConfiguration congif)
		{
			_dictionary = dictionary;
			_config = congif;
		}

		public IActionResult Index(int page = 1)
		{
			var pageSize = _config.TotalAmount;
			var allWords = _dictionary.GetWords().OrderBy(w => w.LowerCaseForm).ToList();
			var totalPages = (int)Math.Ceiling(allWords.Count / (double)pageSize);

			List<string> currentPageItems = allWords.Skip((page - 1) * pageSize).Take(pageSize).Select(w => w.MainForm).ToList();

			WordListModel viewModel = new(currentPageItems, page, allWords.Count, pageSize);

			return View(viewModel);
		}
	}
}
