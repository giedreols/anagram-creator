using AnagramSolver.BusinessLogic;
using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
	[ApiController]
	[Route("[Controller]")]
	public class NewWordController : Controller
	{
		private readonly IWordRepository _wordRepository;
		private readonly IAnagramGenerator _anagramSolver;
		private readonly MyConfiguration _config;
		private readonly IHelpers _helpers;

		public NewWordController(IWordRepository wordRepository, IAnagramGenerator anagramSolver, MyConfiguration config, IHelpers helpers)
		{
			_wordRepository = wordRepository;
			_anagramSolver = anagramSolver;
			_config = config;
			_helpers = helpers;
		}

		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

        [HttpPost]
        public ActionResult Create([FromForm] string newWord, [FromForm] string newAbbreviation)
        {
			ViewData["CurrentWord"] = newWord;
            ViewData["Abbreviation"] = newAbbreviation;

            ViewData["SavedMessage"] = "Žodis išsaugotas žodyne";

			var validity = InputWordValidator.Validate(newWord, _config.MinLength, _config.MaxLength);

			if (validity != null)
			{
				return View("Index", new WordFailedToSaveModel(validity));
			}

			if (!_wordRepository.SaveWord(new Contracts.Dtos.FullWordDto(newWord) { PartOfSpeechAbbreviation = newAbbreviation}))
			{
				return View("Index", new WordFailedToSaveModel(WordRejectionReasons.AlreadyExists));
			}

			AnagramWordsModel model = new(newWord, _anagramSolver.GetAnagrams(newWord));

			_helpers.LogSearch(newWord);

			return View("../Home/WordWithAnagrams", model);
		}
	}
}
