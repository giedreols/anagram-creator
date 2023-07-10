using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AnagramsController : Controller
    {
        private readonly IWordRepository _wordRepo;
        private readonly ILogHelper _helpers;


        public AnagramsController(IWordRepository wordRepo, ILogHelper helpers)
        {
            _wordRepo = wordRepo;
            _helpers = helpers;
        }

        [HttpGet]
        public IActionResult Get(string inputWord)
        {
            if (inputWord.IsNullOrEmpty())
            {
                return View("../Home/Index");
            }

            AnagramWordsModel model = new(inputWord, _wordRepo.GetAnagrams(inputWord).ToList());

            _helpers.LogSearch(inputWord);

            return View("../Home/WordWithAnagrams", model);
        }
    }
}
