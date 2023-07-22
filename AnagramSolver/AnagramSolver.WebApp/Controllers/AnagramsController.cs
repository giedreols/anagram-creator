using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class AnagramsController : Controller
    {
        private readonly Contracts.Interfaces.IWordServer _wordServer;
        private readonly BusinessLogic.LogService _logService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnagramsController(Contracts.Interfaces.IWordServer wordServer, BusinessLogic.LogService logService, IHttpContextAccessor httpContextAccessor)
        {
            _wordServer = wordServer;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public IActionResult Get(string inputWord)
        {
            if (inputWord.IsNullOrEmpty())
                return View("../Home/Index");

            string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (_logService.HasSpareSearch(ipAddress))
            {
                AnagramViewModel model = new(inputWord, _wordServer.GetAnagrams(inputWord).ToList());
                _logService.LogSearch(inputWord, ipAddress);
                return View("../Home/WordWithAnagrams", model);
            }

            else return View("../Home/Index", new ErrorModel("Deja, anagramų paieškų limitas " +
                "iš šio IP adreso išnaudotas"));
        }
    }
}
