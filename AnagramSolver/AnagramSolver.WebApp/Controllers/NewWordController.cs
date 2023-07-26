using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst;
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
        private readonly IWordLogService _wordLogService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NewWordController(IWordServer wordServer, IWordLogService wordLogService, MyConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _wordServer = wordServer;
            _wordLogService = wordLogService;
            _configOptions = config.ConfigOptions;
            _httpContextAccessor = httpContextAccessor;
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
                Id = savedWord.Id,
                AnagramWords = new AnagramViewModel(newWord)
            };

            if (savedWord.IsSaved)
            {
                string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _wordLogService.Log(savedWord.Id, ipAddress, WordOpEnum.ADD);
                
                newWordModel.AnagramWords.Anagrams = _wordServer.GetAnagrams(newWord).ToList();
                
                return View("../Home/WordWithAnagrams", newWordModel.AnagramWords);
            }

            return View("Index", newWordModel);
        }
    }
}
