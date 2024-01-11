using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ErrorMessageEnum = AnagramSolver.WebApp.Models.ErrorMessageEnum;

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

        private readonly string _ipAddress;

        public WordController(IWordServer wordServer, IWordLogService wordLogService, ISearchLogService searchLogService, IConfigReader config, IHttpContextAccessor httpContextAccessor)
        {
            _wordServer = wordServer;
            _wordLogService = wordLogService;
            _configOptions = config.ConfigOptions;
            _httpContextAccessor = httpContextAccessor;
            _searchLogService = searchLogService;

            _ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] string newWord = "", [FromForm] string newAbbreviation = "")
        {
            WordResultDto savedWord = await _wordServer.SaveWordAsync(new FullWordDto(newWord, newWord, newAbbreviation), _configOptions);

            switch ((ErrorMessageEnum)savedWord.ErrorMessage)
            {
                case ErrorMessageEnum.Ok:

                    await _wordLogService.LogWordAsync(savedWord.Id, _ipAddress, WordOpEnum.Add);

                    ViewData["Message"] = "Žodis išsaugotas žodyne";

                    AnagramViewModel result = new()
                    {
                        Anagrams = await _wordServer.GetAnagramsAsync(newWord),
                        Word = newWord,
                        WordId = savedWord.Id
                    };

                    GetAnagramsResultModel model = new()
                    {
                        WordAndAnagrams = result
                    };

                    return View("../Home/WordWithAnagrams", model);

                default:
                    ViewData["Word"] = newWord;
                    ViewData["Abbreviation"] = newAbbreviation;
                    ViewData["Message"] = Helpers.ConvertErrorEnumToMessage((ErrorMessageEnum)savedWord.ErrorMessage);

                    return View("../NewWord/Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsyncRazor(string inputWord = "")
        {
            ViewData["IsFormVisible"] = false;
            ViewData["Word"] = inputWord;

            GetAnagramsResultDto result = await _wordServer.GetAnagramsNewAsync(inputWord);

            GetAnagramsResultModel model = new() { WordAndAnagrams = new AnagramViewModel(result.WordAndAnagrams.Word) };

            foreach (var message in result.ErrorMessages) model.ErrorMessages.Add((ErrorMessageEnum)message);

            if (model.ErrorMessages.Contains(ErrorMessageEnum.Empty))
                return View("../Home/Index");

            if (model.ErrorMessages.Contains(ErrorMessageEnum.SearchLimit))
            {
                return View("../Home/WordWithAnagrams", model);
            }

            model.WordAndAnagrams.WordId = result.WordAndAnagrams.WordId;
            model.WordAndAnagrams.Anagrams = result.WordAndAnagrams.Anagrams;

            return View("../Home/WordWithAnagrams", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAnagramicaAsync(string inputWordAnagramica = "")
        {
            ViewData["IsFormVisible"] = false;
            ViewData["Word"] = inputWordAnagramica;

            if (inputWordAnagramica.IsNullOrEmpty())
                return View("../Home/Index");

            if (!await _searchLogService.HasSpareSearchAsync(_ipAddress, _configOptions.SearchCount))
                View("../Home/WordWithAnagrams");

            var anagrams = await _wordServer.GetAnagramsUsingAnagramicaAsync(inputWordAnagramica);

            await _searchLogService.LogSearchAsync(inputWordAnagramica, _ipAddress);

            GetAnagramsResultModel model = new() { WordAndAnagrams = new AnagramViewModel(inputWordAnagramica, anagrams.ToList()) };

            return View("../Home/WordWithAnagrams", model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int wordId, string word)
        {
            bool isDeleted = await _wordServer.DeleteWordAsync(wordId);

            if (isDeleted)
            {
                await _wordLogService.LogWordAsync(wordId, _ipAddress, WordOpEnum.Delete);
                TempData["Message"] = "Žodis ištrintas.";
                return RedirectToAction("Index", "WordList");
            }

            ViewData["Message"] = "Žodžio ištrinti nepavyko. Kažkas blogai suprogramuota, sorry.";

            var anagrams = await _wordServer.GetAnagramsAsync(word);

            return View("../Home/WordWithAnagrams", new AnagramViewModel(wordId, word, anagrams.ToList()));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromForm] int wordId, [FromForm] string oldForm, [FromForm] string newForm = "")
        {
            ViewData["newForm"] = newForm;
            ViewData["oldForm"] = oldForm;
            ViewData["WordId"] = wordId;

            AnagramViewModel result = new();
            result.WordId = wordId;
            GetAnagramsResultModel model = new();

            if (newForm == oldForm)
            {
                ViewData["IsFormVisible"] = true;

                result.Anagrams = await _wordServer.GetAnagramsAsync(oldForm);
                result.Word = oldForm;
                model.WordAndAnagrams = result;

                return View("../Home/WordWithAnagrams", model);
            }

            WordResultDto updatedWord = await _wordServer.UpdateWordAsync(wordId, newForm, _configOptions);

            if (updatedWord.Word == newForm)
            {
                ViewData["Message"] = "Žodis atnaujintas.";

                await _wordLogService.LogWordAsync(wordId, _ipAddress, WordOpEnum.Edit);
                ViewData["IsFormVisible"] = false;

                result.Anagrams = await _wordServer.GetAnagramsAsync(newForm);
                result.Word = newForm;
            }

            else
            {
                ViewData["Message"] = updatedWord.ErrorMessage;
                ViewData["IsFormVisible"] = true;

                result.Anagrams = await _wordServer.GetAnagramsAsync(oldForm);
                result.Word = oldForm;
                model.ErrorMessages.Add((ErrorMessageEnum)updatedWord.ErrorMessage);
            }

            model.WordAndAnagrams = result;

            return View("../Home/WordWithAnagrams", model);
        }

        [HttpPost]
        public IActionResult ToggleFormVisibility(bool isVisible)
        {
            ViewData["IsFormVisible"] = isVisible;
            return Json(new { success = true });
        }
    }
}
