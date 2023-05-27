using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace AnagramSolver.WebApp.Controllers
{
	[ApiController]
	[Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
	public class AnagramsController : ControllerBase
	{
		private readonly IAnagramGenerator _anagramSolver;

		public AnagramsController(IAnagramGenerator anagramSolver)
		{
			_anagramSolver = anagramSolver;
		}

		[Microsoft.AspNetCore.Mvc.HttpGet("{inputWord}")]
		public ActionResult Get(string inputWord)
		{
			AnagramWordsModel wordWithAnagrams = new(inputWord, _anagramSolver.GetAnagrams(inputWord));
			return Ok(wordWithAnagrams);
		}
	}
}
