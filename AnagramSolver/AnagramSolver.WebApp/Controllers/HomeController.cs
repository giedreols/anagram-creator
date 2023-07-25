using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Net;


namespace AnagramSolver.WebApp.Controllers
{
	[ApiController]
	[Route("[Controller]")]
	[Route("{action=Index}")]

	public class HomeController : Controller
	{
		private readonly IWordServer _wordServer;
		private readonly ISearchLogService _logService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly MyConfiguration _config;

		public HomeController(IWordServer wordServer, ISearchLogService logService, IHttpContextAccessor httpContextAccessor, MyConfiguration config)
		{
			_wordServer = wordServer;
			_logService = logService;
			_httpContextAccessor = httpContextAccessor;
			_config = config;
		}

		[HttpGet]
		public IActionResult Index()
		{
			string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

			if (_logService.HasSpareSearch(ipAddress, _config.ConfigOptions.SearchCount))
				return View();

			else return View(new ErrorModel("Anagramų paieškų limitas iš šio IP adreso išnaudotas. Nori daugiau paieškų?"));
		}
	}
}