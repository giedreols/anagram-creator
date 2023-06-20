using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

			WordsPerPageModel wordsPerPage = _dictionary.GetWordsByPage(page, pageSize);

			WordListModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, pageSize);

			return View("Index", viewModel);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet()]
		public IActionResult Search(string inputWord, int page = 1)
		{
			if(inputWord.IsNullOrEmpty())
			{
				return Index();
			}

			ViewData["CurrentWord"] = inputWord;

			int pageSize = _config.TotalAmount;

			WordsPerPageModel matchingWords = _dictionary.GetMatchingWords(inputWord, page, pageSize);

			WordListModel viewModel = new(matchingWords.Words, page, matchingWords.TotalWordsCount, pageSize);

			HttpContext.Session.SetString("SearchDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			HttpContext.Session.SetString("LastWord", inputWord);

			return View("Index", viewModel);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet()]
		public IActionResult DownloadFile()
		{
			byte[] fileInBytes = _dictionary.GetFileWithWords();

			return File(fileInBytes, "text/plain", "zodynas.txt");
		}
	}
}
