using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Web.Http;

namespace AnagramSolver.WebApp.Controllers
{
	[RoutePrefix("[Controller]")]
	public class AnagramsController : Controller
	{
		private readonly IAnagramGenerator _anagramSolver;
		private readonly Helpers _helpers;


		public AnagramsController(IAnagramGenerator anagramSolver, Helpers helpers)
		{
			_anagramSolver = anagramSolver;
			_helpers = helpers;
		}

		[Microsoft.AspNetCore.Mvc.HttpGet()]
		public IActionResult Get(string inputWord)
		{
			if (inputWord.IsNullOrEmpty())
			{
				return View("../Home/Index");
			}

			AnagramWordsModel model = new(inputWord, _anagramSolver.GetAnagrams(inputWord));

			_helpers.LogSearch(inputWord);

			return View("../Home/WordWithAnagrams", model);
		}
	}
}
