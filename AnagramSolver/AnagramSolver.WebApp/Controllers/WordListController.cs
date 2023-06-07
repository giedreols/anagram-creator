using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Web.Http;

namespace AnagramSolver.WebApp.Controllers
{
	[RoutePrefix("[Controller]")]
	public class WordListController : Controller
	{
		private readonly IWordRepository _dictionary;
		private readonly MyConfiguration _config;

		public WordListController(IWordRepository dictionary, MyConfiguration congif)
		{
			_dictionary = dictionary;
			_config = congif;
		}

		[Microsoft.AspNetCore.Mvc.HttpGet()]
		public IActionResult Index(int page = 1)
		{
			int pageSize = _config.TotalAmount;

			PageWordModel wordsPerPage = _dictionary.GetWordsByPage(page, pageSize);

			WordListModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, pageSize);

			return View(viewModel);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet()]
		public IActionResult Search(string inputWord, int page = 1)
		{
			ViewData["CurrentWord"] = inputWord;

			int pageSize = _config.TotalAmount;

			PageWordModel matchingWords = _dictionary.GetMatchingWords(inputWord, page, pageSize);

			WordListModel viewModel = new(matchingWords.Words, page, matchingWords.TotalWordsCount, pageSize);

			return View("Index", viewModel);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet()]
		public IActionResult DownloadFile()
		{
			byte[] fileInBytes = _dictionary.GetFile();

			return File(fileInBytes, "text/plain", "zodynas.txt");
		}
	}
}
