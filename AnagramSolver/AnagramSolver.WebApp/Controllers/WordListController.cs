using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
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
			int pageSize = _config.TotalAmount;

			PageWordModel wordsPerPage = _dictionary.GetWordsByPage(page, pageSize);

			WordListModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, pageSize);

			return View(viewModel);
		}
	}
}
