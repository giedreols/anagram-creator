using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.WebApp.Controllers
{
	public class UserInfoController : Controller
	{
		private readonly IWordRepository _databaseActions;

		public UserInfoController(IWordRepository databaseActions)
		{
			_databaseActions = databaseActions;
		}

		public IActionResult Index()
		{
			SearchLogDto data = _databaseActions.GetLastSearchInfo();

			UserInfoModel model = new()
			{
				LastWord = string.IsNullOrEmpty(data.Word) ? "nėra" : data.Word,
				SearchDateTime = data.TimeStamp.Equals(DateTime.MinValue) ? "nėra" : data.TimeStamp.ToString(),
				Ip = string.IsNullOrEmpty(data.UserIp) ? "nėra" : data.UserIp,
				Anagrams = !string.IsNullOrEmpty(data.Word) && data.Anagrams.Count() == 1 && data.Anagrams[0].IsNullOrEmpty() ? new List<string> { "nėra" } : data.Anagrams,
			};

			return View(model);
		}
	}
}
