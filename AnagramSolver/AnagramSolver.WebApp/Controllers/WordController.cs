using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ErrorMessageEnumModel = AnagramSolver.WebApp.Models.ErrorMessageEnumModel;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class WordController : Controller
    {
        private readonly IWordServer _wordServer;
        private readonly IWordLogService _wordLogService;
        private readonly ISearchLogService _searchLogService;

        public WordController(IWordServer wordServer, IWordLogService wordLogService, ISearchLogService searchLogService)
        {
            _wordServer = wordServer;
            _wordLogService = wordLogService;
            _searchLogService = searchLogService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync([FromForm] string newWord = "", [FromForm] string newAbbreviation = "")
        {
            WordResultDto savedWord = await _wordServer.SaveWordAsync(new FullWordDto(newWord, newWord, newAbbreviation));

            switch ((ErrorMessageEnumModel)savedWord.ErrorMessage)
            {
                case ErrorMessageEnumModel.Ok:

                    await _wordLogService.LogWordAsync(savedWord.Id, WordOpEnum.Add);

                    ViewData["Message"] = "Žodis išsaugotas žodyne";

                    AnagramViewModel result = new()
                    {
                        Anagrams = await _wordServer.GetAnagramsAsync(newWord),
                        Word = newWord,
                        WordId = savedWord.Id
                    };

                    ResultModel<AnagramViewModel> model = new()
                    {
                        Result = result
                    };
                    
                    return View("../Home/WordWithAnagrams", model);

                default:
                    ViewData["Word"] = newWord;
                    ViewData["Abbreviation"] = newAbbreviation;
                    ViewData["Message"] = Helpers.ConvertErrorEnumToMessage((ErrorMessageEnumModel)savedWord.ErrorMessage);

                    return View("../NewWord/Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAsyncRazor(string inputWord = "")
        {
            ViewData["IsFormVisible"] = false;
            ViewData["Word"] = inputWord;

            GetAnagramsResultDto result = await _wordServer.GetAnagramsNewAsync(inputWord);

            ResultModel<AnagramViewModel> model = new() { Result = new AnagramViewModel(result.WordAndAnagrams.Word) };

            foreach (var message in result.ErrorMessages) model.ErrorMessages.Add((ErrorMessageEnumModel)message);

            if (model.ErrorMessages.Contains(ErrorMessageEnumModel.Empty))
                return View("../Home/Index");

            if (model.ErrorMessages.Contains(ErrorMessageEnumModel.SearchLimit))
            {
                return View("../Home/WordWithAnagrams", model);
            }

            model.Result.WordId = result.WordAndAnagrams.WordId;
            model.Result.Anagrams = result.WordAndAnagrams.Anagrams;

            return View("../Home/WordWithAnagrams", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetAnagramicaAsync(string inputWordAnagramica = "")
        {
            ViewData["IsFormVisible"] = false;
            ViewData["Word"] = inputWordAnagramica;

            if (inputWordAnagramica.IsNullOrEmpty())
                return View("../Home/Index");

            if (!await _searchLogService.HasSpareSearchAsync())
                View("../Home/WordWithAnagrams");

            var anagrams = await _wordServer.GetAnagramsUsingAnagramicaAsync(inputWordAnagramica);

            await _searchLogService.LogSearchAsync(inputWordAnagramica);

            ResultModel<AnagramViewModel> model = new() { Result = new AnagramViewModel(inputWordAnagramica, anagrams.ToList()) };

            return View("../Home/WordWithAnagrams", model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int wordId, string word)
        {
            bool isDeleted = await _wordServer.DeleteWordAsync(wordId);

            if (isDeleted)
            {
                await _wordLogService.LogWordAsync(wordId, WordOpEnum.Delete);
                TempData["Message"] = "Žodis ištrintas.";
                return RedirectToAction("Index", "WordList");
            }

            ViewData["Message"] = "Žodžio ištrinti nepavyko. Kažkas blogai suprogramuota, sorry.";

            var anagrams = await _wordServer.GetAnagramsAsync(word);

            AnagramViewModel model = new AnagramViewModel(wordId, word, anagrams.ToList());

            return View("../Home/WordWithAnagrams", model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromForm] int wordId, [FromForm] string oldForm, [FromForm] string newForm = "")
        {
            ViewData["newForm"] = newForm;
            ViewData["oldForm"] = oldForm;
            ViewData["WordId"] = wordId;

            AnagramViewModel result = new();
            result.WordId = wordId;
            ResultModel<AnagramViewModel> model = new();

            if (newForm == oldForm)
            {
                ViewData["IsFormVisible"] = true;

                result.Anagrams = await _wordServer.GetAnagramsAsync(oldForm);
                result.Word = oldForm;
                model.Result = result;

                return View("../Home/WordWithAnagrams", model);
            }

            WordResultDto updatedWord = await _wordServer.UpdateWordAsync(wordId, newForm);

            if (updatedWord.Word == newForm)
            {
                ViewData["Message"] = "Žodis atnaujintas.";

                await _wordLogService.LogWordAsync(wordId, WordOpEnum.Edit);
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
                model.ErrorMessages.Add((ErrorMessageEnumModel)updatedWord.ErrorMessage);
            }

            model.Result= result;

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
