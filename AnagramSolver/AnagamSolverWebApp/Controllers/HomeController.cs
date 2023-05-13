using AnagamSolverWebApp.Models;
using BusinessLogic.AnagramActions;
using BusinessLogic.DictionaryActions;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnagamSolverWebApp.Controllers
{
	// TODO: PRIDĖTI MYGTUKĄ, KURIS PARODO VISĄ SĄRAŠĄ

	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private IAnagramSolver _anagramSolver;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
			_anagramSolver = new AnagramSolver(new WordDictionary(new FileReader()));
		}

		public IActionResult Index(string inputWord = "")
		{
			AnagramWordsModel wordWithAnagrams;

			if (inputWord != "")
			{
				wordWithAnagrams = new(inputWord, _anagramSolver.GetAnagrams(inputWord));
			}
			else
			{
				wordWithAnagrams = new("", new List<string>());
			}

			return View(wordWithAnagrams);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}