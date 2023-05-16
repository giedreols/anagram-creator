using AnagramSolverWebApp.Models;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AnagramSolverWebApp.Controllers
{
	public class HomeController : Controller
	{
		private IAnagramSolver _anagramSolver;

		public HomeController(IAnagramSolver anagramSolver)
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