using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class UserInfoController : Controller
    {
        private readonly IWordRepository _wordRepo;

        public UserInfoController(IWordRepository wordRepo)
        {
            _wordRepo = wordRepo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            SearchLogDto lastSearchInfo = _wordRepo.GetLastSearchInfo();

            UserInfoModel model = new()
            {
                LastWord = string.IsNullOrEmpty(lastSearchInfo.Word) ? "nėra" : lastSearchInfo.Word,
                SearchDateTime = lastSearchInfo.TimeStamp.Equals(DateTime.MinValue) ? "nėra" : lastSearchInfo.TimeStamp.ToString(),
                Ip = string.IsNullOrEmpty(lastSearchInfo.UserIp) ? "nėra" : lastSearchInfo.UserIp,
            };

            if (!lastSearchInfo.Word.IsNullOrEmpty())
            {
                IList<string> anagrams = _wordRepo.GetAnagrams(lastSearchInfo.Word).ToList();
                model.Anagrams = anagrams;
            }

            return View(model);
        }
    }
}
