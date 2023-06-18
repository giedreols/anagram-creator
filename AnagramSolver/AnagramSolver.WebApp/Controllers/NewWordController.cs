using AnagramSolver.BusinessLogic;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace AnagramSolver.WebApp.Controllers
{
	[RoutePrefix("[Controller]")]
	public class NewWordController : Controller
	{
		private readonly IWordRepository _wordRepository;
		private readonly IAnagramGenerator _anagramSolver;
		private readonly MyConfiguration _config;

		public NewWordController(IWordRepository wordRepository, IAnagramGenerator anagramSolver, MyConfiguration config)
		{
			_wordRepository = wordRepository;
			_anagramSolver = anagramSolver;
			_config = config;
		}

		public ActionResult Index()
		{
			return View();
		}

		[Microsoft.AspNetCore.Mvc.HttpPost()]
		public ActionResult Create(string newWord)
		{
			ViewData["CurrentWord"] = newWord;
			ViewData["SavedMessage"] = "Žodis išsaugotas žodyne";

			var validity = InputWordValidator.Validate(newWord, _config.MinLength, _config.MaxLength);

			if (validity != null)
			{
				return View("Index", new WordFailedToSaveModel(validity));
			}

			if (!_wordRepository.SaveWord(newWord))
			{
				return View("Index", new WordFailedToSaveModel(WordRejectionReasons.AlreadyExists));
			}

			AnagramWordsModel model = new(newWord, _anagramSolver.GetAnagrams(newWord));

			return View("../Home/WordWithAnagrams", model);
		}
	}
}
