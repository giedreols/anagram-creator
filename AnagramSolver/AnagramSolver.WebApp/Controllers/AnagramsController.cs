using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.WebApp.Controllers
{
	[ApiController]
	[Route("[Controller]/[Action]")]
	public class AnagramsController : Controller
	{
		private readonly IAnagramGenerator _anagramSolver;
		private readonly IHelpers _helpers;


		public AnagramsController(IAnagramGenerator anagramSolver, IHelpers helpers)
		{
			_anagramSolver = anagramSolver;
			_helpers = helpers;
		}

		[HttpGet]
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
