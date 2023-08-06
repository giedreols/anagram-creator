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
        public ActionResult Create([FromForm] string newWord = "", [FromForm] string newAbbreviation = "")
        {
            ViewData["Word"] = newWord;
            ViewData["Abbreviation"] = newAbbreviation;

            NewWordDto savedWord = _wordServer.SaveWord(new FullWordDto(newWord, newWord, newAbbreviation), _configOptions);

            if (savedWord.IsSaved)
            {
                _wordLogService.LogWord(savedWord.Id, ipAddress, WordOpEnum.Add);

                ViewData["Message"] = "Žodis išsaugotas žodyne";

                AnagramViewModel anagrams = new(savedWord.Id, newWord, _wordServer.GetAnagrams(newWord).ToList());

                return View("../Home/WordWithAnagrams", anagrams);
            }

            ViewData["Message"] = savedWord.ErrorMessage;

            return View("../NewWord/Index");
        }

        [HttpGet]
        public IActionResult Get(string inputWord = "")
        {
            // patikrinti, ar toks zodis egzistuoja. jei taip - rodyti redagavima ir trynima. jei ne -rodyti itraukima i zodyna?

            ViewData["IsFormVisible"] = false;
            ViewData["Word"] = inputWord;

            if (inputWord.IsNullOrEmpty())
                return View("../Home/Index");


            if (!_searchLogService.HasSpareSearch(ipAddress, _configOptions.SearchCount))
            {
                View("../Home/WordWithAnagrams");
            }

            int wordId = _wordServer.GetWordId(inputWord);

            if (wordId == 0)
            {
                // nerodyti redagavimo ir trynimo mygtuku
            }

            AnagramViewModel model = new(wordId, inputWord, _wordServer.GetAnagrams(inputWord).ToList());
            _searchLogService.LogSearch(inputWord, ipAddress);
            return View("../Home/WordWithAnagrams", model);

        }

        // NEVEIKIA

        [HttpGet]
        public ActionResult Delete(int wordId = 0, int currentPage = 1)
        {
            if (wordId == 0)
            {
                TempData["Message"] = "Pasirink žodį, kurį nori ištrinti.";
            }

            bool isDeleted = _wordServer.DeleteWord(wordId);

            if (isDeleted)
            {
                _wordLogService.LogWord(wordId, ipAddress, WordOpEnum.Delete);
                TempData["Message"] = "Žodis ištrintas.";
            }
            else
            {
                TempData["Message"] = "Žodžio ištrinti nepavyko. Kažkas blogai suprogramuota, sorry.";
            }

            return RedirectToAction("Index", "WordList", new { page = currentPage });
        }

        [HttpPost]
        public ActionResult Update([FromForm] int wordId, [FromForm] string oldForm, [FromForm] string newForm = "")
        {
            ViewData["newForm"] = newForm;
            ViewData["oldForm"] = oldForm;
            ViewData["WordId"] = wordId;

            if (newForm == oldForm)
            {
                ViewData["IsFormVisible"] = true;

                AnagramViewModel model = new(wordId, oldForm, _wordServer.GetAnagrams(oldForm).ToList());
                return View("../Home/WordWithAnagrams", model);
            }

            var updatedWord = _wordServer.UpdateWord(wordId, newForm, _configOptions);

            if (updatedWord.IsSaved)
            {
                ViewData["Message"] = "Žodis atnaujintas.";

                _wordLogService.LogWord(wordId, ipAddress, WordOpEnum.Edit);
                ViewData["IsFormVisible"] = false;

                AnagramViewModel model = new(wordId, newForm, _wordServer.GetAnagrams(newForm).ToList());
                return View("../Home/WordWithAnagrams", model);
            }

            ViewData["Message"] = updatedWord.ErrorMessage;
            ViewData["IsFormVisible"] = true;

            return View("../Home/WordWithAnagrams", new AnagramViewModel(wordId, oldForm, _wordServer.GetAnagrams(oldForm).ToList()));

        }

        [HttpPost]
        public IActionResult ToggleFormVisibility(bool isVisible)
        {
            ViewData["IsFormVisible"] = isVisible;
            return Json(new { success = true });
        }
    }
}
