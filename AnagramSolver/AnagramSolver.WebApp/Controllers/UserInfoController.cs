using AnagramSolver.BusinessLogic;
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
        private readonly ILogService _logService;
        private readonly IWordServer _wordServer;

        public UserInfoController(ILogService logService, IWordServer wordServer)
        {
            _logService = logService;
            _wordServer = wordServer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            SearchLogDto lastSearchInfo = _logService.GetLastSearchInfo();

            var word = string.IsNullOrEmpty(lastSearchInfo.Word) ? "nėra" : lastSearchInfo.Word;

			UserInfoViewModel model = new()
            {
				AnagramWords = new AnagramViewModel(word),
                SearchDateTime = lastSearchInfo.TimeStamp.Equals(DateTime.MinValue) ? "nėra" : lastSearchInfo.TimeStamp.ToString(),
                Ip = string.IsNullOrEmpty(lastSearchInfo.UserIp) ? "nėra" : lastSearchInfo.UserIp,
            };

            if (!lastSearchInfo.Word.IsNullOrEmpty())
            {
                IList<string> anagrams = _wordServer.GetAnagrams(lastSearchInfo.Word).ToList();
                model.AnagramWords.Anagrams = anagrams;
            }

            return View(model);
        }
    }
}
