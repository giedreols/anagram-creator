using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnagramSolver.WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly IAnagramGenerator _anagramSolver;

		public HomeController(IAnagramGenerator anagramSolver)
		{
			_anagramSolver = anagramSolver;
		}

		public IActionResult Index(string inputWord = "")
		{
			AnagramWordsModel wordWithAnagrams = inputWord != "" ? new(inputWord, _anagramSolver.GetAnagrams(inputWord)) : new("", new List<string>());
			return View(wordWithAnagrams);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}