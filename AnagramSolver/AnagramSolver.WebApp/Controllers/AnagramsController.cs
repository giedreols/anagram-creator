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
			AnagramWordsModel wordWithAnagrams = new(inputWord, _anagramSolver.GetAnagrams(inputWord));
			return Ok(wordWithAnagrams);
		}
	}
}
