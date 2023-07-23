using AnagramSolver.BusinessLogic;
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
		private readonly IWordServer _wordServer;
		private readonly ILogService _logService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly MyConfiguration _config;

		public AnagramsController(IWordServer wordServer, ILogService logService, IHttpContextAccessor httpContextAccessor, MyConfiguration config)
		{
			_wordServer = wordServer;
			_logService = logService;
			_httpContextAccessor = httpContextAccessor;
			_config = config;
		}

		[HttpGet]
		public IActionResult Get(string inputWord)
		{
			if (inputWord.IsNullOrEmpty())
				return View("../Home/Index");

			string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

			if (_logService.HasSpareSearch(ipAddress, _config.ConfigOptions.SearchCount))
			{
				AnagramViewModel model = new(inputWord, _wordServer.GetAnagrams(inputWord).ToList());
				_logService.LogSearch(inputWord, ipAddress);
				return View("../Home/WordWithAnagrams", model);
			}

			else return View("../Home/WordWithAnagrams", new AnagramViewModel(inputWord, "Anagramų paieškų limitas iš šio IP adreso išnaudotas"));
		}
	}
}
