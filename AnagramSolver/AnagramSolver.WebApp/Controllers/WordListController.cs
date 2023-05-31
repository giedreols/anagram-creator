using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class WordListController : Controller
	{
		private readonly IWordRepository _dictionary;
		private readonly MyConfiguration _config;

		public WordListController(IWordRepository dictionary, MyConfiguration congif)
		{
			_dictionary = dictionary;
			_config = congif;
		}

		[HttpGet()]
		public IActionResult Index(int page = 1)
		{
			int pageSize = _config.TotalAmount;

			PageWordModel wordsPerPage = _dictionary.GetWordsByPage(page, pageSize);

			WordListModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, pageSize);

			return View(viewModel);
		}

		[HttpGet()]
		public IActionResult DownloadFile()
		{
			byte[] fileInBytes = _dictionary.GetFile();

			return File(fileInBytes, "text/plain", "zodynas.txt");
		}
	}
}
