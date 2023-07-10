using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
	[Route("[Controller]/[Action]")]
	public class WordListController : Controller
	{
		private readonly IWordRepository _dictionary;
		private readonly MyConfiguration _config;

		public WordListController(IWordRepository dictionary, MyConfiguration congif)
		{
			_dictionary = dictionary;
			_config = congif;
		}

		[HttpGet]
		public IActionResult Index(int page = 1)
		{
			int pageSize = _config.ConfigOptions.TotalAmount;

			WordsPerPageDto wordsPerPage = _dictionary.GetWordsByPage(page, pageSize);

			WordListModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, pageSize);

			return View("Index", viewModel);
		}

		[HttpGet]
		public IActionResult Search(string inputWord, int page = 1)
		{
			if (inputWord.IsNullOrEmpty())
			{
				return Index();
			}

			ViewData["CurrentWord"] = inputWord;

			int pageSize = _config.ConfigOptions.TotalAmount;

			WordsPerPageDto matchingWords = _dictionary.GetMatchingWords(inputWord, page, pageSize);

			WordListModel viewModel = new(matchingWords.Words, page, matchingWords.TotalWordsCount, pageSize);

			return View("Index", viewModel);
		}

		[HttpGet]
		public IActionResult DownloadFile()
		{
			byte[] fileInBytes = _dictionary.GetFileWithWords();

			return File(fileInBytes, "text/plain", "zodynas.txt");
		}
	}
}
