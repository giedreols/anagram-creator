using AnagramSolverWebApp.Models;
using BusinessLogic.InputWordActions;
using Cli;
using Contracts.Interfaces;
using Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
	public class NewWordController : Controller
	{
		private IWordRepository _wordRepository;
		private IAnagramSolver _anagramSolver;
		private MyConfiguration _config;

		public NewWordController(IWordRepository wordRepository, IAnagramSolver anagramSolver, MyConfiguration config)
		{
			_wordRepository = wordRepository;
			_anagramSolver = anagramSolver;
			_config = config;
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(string newWord)
		{
			ViewData["CurrentWord"] = newWord;

			var validity = InputValidator.Validate(newWord, _config.MinLength, _config.MaxLength);

			if (validity != null)
			{
				return View("Index", new WordFailedToSaveModel(validity));
			}

			if (!_wordRepository.SaveWord(newWord))
			{
				return View("Index", new WordFailedToSaveModel(WordRejectionReasons.AlreadyExists));
			}

			NewWordModel model = new()
			{
				Word = newWord,
				Anagrams = _anagramSolver.GetAnagrams(newWord)
			};

			return View("WordCreated", model);

		}
	}
}
