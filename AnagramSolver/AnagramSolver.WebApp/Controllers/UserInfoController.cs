using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace AnagramSolver.WebApp.Controllers
{
	public class UserInfoController : Controller
	{
		private readonly IAnagramGenerator _anagramGenerator;

		public UserInfoController(IAnagramGenerator anagramGenerator)
		{
			_anagramGenerator = anagramGenerator;
		}

		public IActionResult Index()
		{
			var lastWord = HttpContext.Session.GetString("LastWord");

			UserInfoModel model = new()
			{
				LastWord = lastWord ?? "-",
				SearchDateTime = HttpContext.Session.GetString("SearchDateTime") ?? "-",
				Anagrams = lastWord != null ? _anagramGenerator.GetAnagrams(lastWord) : null
			};

			return View(model);
		}
	}
}
