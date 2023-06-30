using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace AnagramSolver.WebApp.Controllers
{
	[RoutePrefix("[Controller]")]
	public class AnagramsController : Controller
	{
		private readonly IAnagramGenerator _anagramSolver;
		//private readonly IHttpContextAccessor _httpContextAccessor;
		//private readonly IWordRepository _wordRepository;


		//public AnagramsController(IAnagramGenerator anagramSolver, IHttpContextAccessor httpContextAccessor, IWordRepository wordRepository)
		//{
		//	_anagramSolver = anagramSolver;
		//	_httpContextAccessor = httpContextAccessor;
		//	_wordRepository = wordRepository;
		//}

		public AnagramsController(IAnagramGenerator anagramSolver)
		{
			_anagramSolver = anagramSolver;
		}

		[Microsoft.AspNetCore.Mvc.HttpGet()]
		public IActionResult Get(string inputWord)
		{
			AnagramWordsModel model = new(inputWord, _anagramSolver.GetAnagrams(inputWord));

			LogSearch(inputWord);

			return View("../Home/WordWithAnagrams", model);
		}

		private void LogSearch(string inputWord)
		{
			//string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

			//_wordRepository.LogSearch(new SearchLogModel(ipAddress, DateTime.Now, inputWord));
		}
	}
}
