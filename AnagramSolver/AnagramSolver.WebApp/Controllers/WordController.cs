using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class WordController : Controller
    {
        private readonly IWordServer _wordServer;
        private readonly ConfigOptionsDto _configOptions;
        private readonly IWordLogService _wordLogService;
        private readonly ISearchLogService _searchLogService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly string ipAddress;

        public WordController(IWordServer wordServer, IWordLogService wordLogService, ISearchLogService searchLogService, MyConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _wordServer = wordServer;
            _wordLogService = wordLogService;
            _configOptions = config.ConfigOptions;
            _httpContextAccessor = httpContextAccessor;
            _searchLogService = searchLogService;

            ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
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
                Id = savedWord.Id,
                AnagramWords = new AnagramViewModel(newWord)
            };

            if (savedWord.IsSaved)
            {
                _wordLogService.Log(savedWord.Id, ipAddress, WordOpEnum.ADD);

                newWordModel.AnagramWords.Anagrams = _wordServer.GetAnagrams(newWord).ToList();

                return View("../Home/WordWithAnagrams", newWordModel.AnagramWords);
            }

            return View("../NewWord/Index", newWordModel);
        }

        [HttpGet]
        public IActionResult Get(string inputWord)
        {
            if (inputWord.IsNullOrEmpty())
                return View("../Home/Index");

            if (_searchLogService.HasSpareSearch(ipAddress, _configOptions.SearchCount))
            {
                AnagramViewModel model = new(inputWord, _wordServer.GetAnagrams(inputWord).ToList());
                _searchLogService.LogSearch(inputWord, ipAddress);
                return View("../Home/WordWithAnagrams", model);
            }

            else return View("../Home/WordWithAnagrams", new AnagramViewModel(inputWord, "Anagramų paieškų limitas iš šio IP adreso išnaudotas. Nori daugiau paieškų?"));
        }

        [HttpGet]
        public ActionResult Delete(int wordId)
        {
            bool isDeleted = _wordServer.DeleteWord(wordId);

            if (isDeleted)
            {
                _wordLogService.Log(wordId, ipAddress, WordOpEnum.DELETE);
            }

            return RedirectToAction("Index", "WordList");
        }

        [HttpGet]
        public ActionResult Update(int wordId, string newForm)
        {
            bool isUpdated = _wordServer.UpdateWord(wordId, newForm);

            if (isUpdated)
            {
                _wordLogService.Log(wordId, ipAddress, WordOpEnum.EDIT);
            }

            // palikti tame paciame lape, kuriame buvo?

            return RedirectToAction("Index", "WordList");
        }
    }
}
