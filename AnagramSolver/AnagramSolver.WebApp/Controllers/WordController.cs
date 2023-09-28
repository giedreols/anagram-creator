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
        public async Task<ActionResult> CreateAsync([FromForm] string newWord = "", [FromForm] string newAbbreviation = "")
        {
            ViewData["Word"] = newWord;
            ViewData["Abbreviation"] = newAbbreviation;

            WordResultDto savedWord = await _wordServer.SaveWordAsync(new FullWordDto(newWord, newWord, newAbbreviation), _configOptions);

            if (savedWord.Id > 0)
            {
                await _wordLogService.LogWordAsync(savedWord.Id, ipAddress, WordOpEnum.Add);

                ViewData["Message"] = "Žodis išsaugotas žodyne";

                var anagrams = await _wordServer.GetAnagramsAsync(newWord);

                return View("../Home/WordWithAnagrams", new AnagramViewModel(savedWord.Id, newWord, anagrams.ToList()));
            }

            ViewData["Message"] = savedWord.ErrorMessage;

            return View("../NewWord/Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(string inputWord = "")
        {
            ViewData["IsFormVisible"] = false;
            ViewData["Word"] = inputWord;

            if (inputWord.IsNullOrEmpty())
                return View("../Home/Index");

            if (!await _searchLogService.HasSpareSearchAsync(ipAddress, _configOptions.SearchCount))
            {
                View("../Home/WordWithAnagrams");
            }

            int wordId = await _wordServer.GetWordIdAsync(inputWord);

            var anagrams = await _wordServer.GetAnagramsAsync(inputWord);

            _searchLogService.LogSearch(inputWord, ipAddress);

            return View("../Home/WordWithAnagrams", new AnagramViewModel(wordId, inputWord, anagrams.ToList()));
        }

        [HttpGet]
        public async Task<IActionResult> GetAnagramicaAsync(string inputWordAnagramica = "")
        {
            ViewData["IsFormVisible"] = false;
            ViewData["Word"] = inputWordAnagramica;

            if (inputWordAnagramica.IsNullOrEmpty())
                return View("../Home/Index");

            if (!await _searchLogService.HasSpareSearchAsync(ipAddress, _configOptions.SearchCount))
                View("../Home/WordWithAnagrams");

            var anagrams = await _wordServer.GetAnagramsUsingAnagramicaAsync(inputWordAnagramica);

            _searchLogService.LogSearch(inputWordAnagramica, ipAddress);

            return View("../Home/WordWithAnagrams", new AnagramViewModel(inputWordAnagramica, anagrams.ToList()));
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(int wordId, string word)
        {
            bool isDeleted = await _wordServer.DeleteWordAsync(wordId);

            if (isDeleted)
            {
                await _wordLogService.LogWordAsync(wordId, ipAddress, WordOpEnum.Delete);
                TempData["Message"] = "Žodis ištrintas.";
                return RedirectToAction("Index", "WordList");
            }

            ViewData["Message"] = "Žodžio ištrinti nepavyko. Kažkas blogai suprogramuota, sorry.";

            var anagrams = await _wordServer.GetAnagramsAsync(word);

            return View("../Home/WordWithAnagrams", new AnagramViewModel(wordId, word, anagrams.ToList()));
        }

        [HttpPost]
        public async Task<ActionResult> UpdateAsync([FromForm] int wordId, [FromForm] string oldForm, [FromForm] string newForm = "")
        {
            ViewData["newForm"] = newForm;
            ViewData["oldForm"] = oldForm;
            ViewData["WordId"] = wordId;

            IEnumerable<string> anagrams;

            if (newForm == oldForm)
            {
                ViewData["IsFormVisible"] = true;

                anagrams = await _wordServer.GetAnagramsAsync(oldForm);

                return View("../Home/WordWithAnagrams", new AnagramViewModel(wordId, oldForm, anagrams.ToList()));
            }

            WordResultDto updatedWord = await _wordServer.UpdateWordAsync(wordId, newForm, _configOptions);

            if (updatedWord.Word == newForm)
            {
                ViewData["Message"] = "Žodis atnaujintas.";

                await _wordLogService.LogWordAsync(wordId, ipAddress, WordOpEnum.Edit);
                ViewData["IsFormVisible"] = false;

                anagrams = await _wordServer.GetAnagramsAsync(newForm);

                return View("../Home/WordWithAnagrams", new AnagramViewModel(wordId, newForm, anagrams.ToList()));
            }

            ViewData["Message"] = updatedWord.ErrorMessage;
            ViewData["IsFormVisible"] = true;

            anagrams = await _wordServer.GetAnagramsAsync(oldForm);

            return View("../Home/WordWithAnagrams", new AnagramViewModel(wordId, oldForm, anagrams.ToList()));
        }

        [HttpPost]
        public IActionResult ToggleFormVisibility(bool isVisible)
        {
            ViewData["IsFormVisible"] = isVisible;
            return Json(new { success = true });
        }
    }
}
