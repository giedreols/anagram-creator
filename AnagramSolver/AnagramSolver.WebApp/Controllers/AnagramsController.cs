using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace AnagramSolver.WebApp.Controllers
{
	[RoutePrefix("[Controller]")]
	public class AnagramsController : Controller
	{
		private readonly IAnagramGenerator _anagramSolver;

		public AnagramsController(IAnagramGenerator anagramSolver)
		{
			_anagramSolver = anagramSolver;
		}

		[Microsoft.AspNetCore.Mvc.HttpGet()]
		public IActionResult Get(string inputWord)
		{
			AnagramWordsModel model = new(inputWord, _anagramSolver.GetAnagrams(inputWord));

			HttpContext.Session.SetString("SearchDateTime", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
			HttpContext.Session.SetString("LastWord", inputWord);

			return View("../Home/WordWithAnagrams", model);
		}
	}
}
