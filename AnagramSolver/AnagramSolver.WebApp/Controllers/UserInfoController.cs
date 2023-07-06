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
		private readonly IWordRepository _databaseActions;
		private readonly IAnagramGenerator _anagramGenerator;

		public UserInfoController(IWordRepository databaseActions, IAnagramGenerator anagramGenerator)
		{
			_databaseActions = databaseActions;
			_anagramGenerator = anagramGenerator;
		}

		[HttpGet]
		public IActionResult Index()
		{
			SearchLogDto lastSearchInfo = _databaseActions.GetLastSearchInfo();

			UserInfoModel model = new()
			{
				LastWord = string.IsNullOrEmpty(lastSearchInfo.Word) ? "nėra" : lastSearchInfo.Word,
				SearchDateTime = lastSearchInfo.TimeStamp.Equals(DateTime.MinValue) ? "nėra" : lastSearchInfo.TimeStamp.ToString(),
				Ip = string.IsNullOrEmpty(lastSearchInfo.UserIp) ? "nėra" : lastSearchInfo.UserIp,
			};

			if (!lastSearchInfo.Word.IsNullOrEmpty())
			{
				IList<string> anagrams = _anagramGenerator.GetAnagrams(lastSearchInfo.Word);
				model.Anagrams = anagrams;
			}

			return View(model);
		}
	}
}
