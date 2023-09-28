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
        private readonly ISearchLogService _logService;
        private readonly IWordServer _wordServer;

        public UserInfoController(ISearchLogService logService, IWordServer wordServer)
        {
            _logService = logService;
            _wordServer = wordServer;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            SearchLogDto lastSearchInfo = await _logService.GetLastSearchInfoAsync();

            var word = string.IsNullOrEmpty(lastSearchInfo.Word) ? "nėra" : lastSearchInfo.Word;

            UserInfoViewModel model = new()
            {
                AnagramWords = new AnagramViewModel(word),
                SearchDateTime = lastSearchInfo.TimeStamp.Equals(DateTime.MinValue) ? "nėra"
                        : TimeZoneInfo.ConvertTimeFromUtc((DateTime)lastSearchInfo.TimeStamp, TimeZoneInfo.FindSystemTimeZoneById("Europe/Vilnius")).ToString(),
                Ip = string.IsNullOrEmpty(lastSearchInfo.UserIp) ? "nėra" : lastSearchInfo.UserIp,
            };

            if (!lastSearchInfo.Word.IsNullOrEmpty())
            {
                var anagrams = await _wordServer.GetAnagramsAsync(lastSearchInfo.Word);
                model.AnagramWords.Anagrams = anagrams.ToList();
            }

            return View(model);
        }
    }
}
