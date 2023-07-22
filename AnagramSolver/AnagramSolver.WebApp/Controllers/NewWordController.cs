using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class NewWordController : Controller
    {
        private readonly IWordServer _wordServer;
        private readonly ConfigOptionsDto _configOptions;

        public NewWordController(IWordServer wordServer, MyConfiguration config)
        {
            _wordServer = wordServer;
            _configOptions = config.ConfigOptions;
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

            NewWordDto savedWord = _wordServer.SaveWord(new FullWordDto(newWord, newWord, newAbbreviation), _configOptions);

            NewWordViewModel newWordModel = new()
            {
                IsSaved = savedWord.IsSaved,
                ErrorMessage = savedWord.ErrorMessage,
                AnagramWords = new AnagramViewModel(newWord)
            };

            if (savedWord.IsSaved)
            {
                newWordModel.AnagramWords.Anagrams = _wordServer.GetAnagrams(newWord).ToList();
                return View("../Home/WordWithAnagrams", newWordModel.AnagramWords);
            }

            return View("Index", newWordModel);
        }
    }
}
