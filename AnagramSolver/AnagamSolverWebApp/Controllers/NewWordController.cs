using AnagramSolverWebApp.Models;
using Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
	public class NewWordController : Controller
	{
		private IWordRepository _wordRepository;
		private IAnagramSolver _anagramSolver;

		public NewWordController(IWordRepository wordRepository, IAnagramSolver anagramSolver)
		{
			_wordRepository = wordRepository;
			_anagramSolver = anagramSolver;
		}

		public ActionResult Index()
		{
			return View();
		}

		// TODO: validuoja, kad nebūtų per trumpas arper ilgas
		// TODO: validuoja, kad būtų tinkami simboliai
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(string newWord)
		{
			bool isSaved = _wordRepository.SaveWord(newWord);

			NewWordModel model = new();

			if (isSaved)
			{
				model.Word = newWord;
				model.Anagrams = _anagramSolver.GetAnagrams(newWord);
			};

			return View("CreatedWordPage", model);

		}
	}
}
