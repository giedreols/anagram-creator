using AnagramSolver.Cli;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Web.Http;

namespace AnagramSolver.WebApp.Controllers
{
	[RoutePrefix("[Controller]")]
	[Microsoft.AspNetCore.Mvc.Route("{action=Index}")]
	public class HomeController : Controller
	{
		private readonly MyConfiguration _config;
		private readonly IAnagramGenerator _anagramGenerator;

        public HomeController(MyConfiguration config, IAnagramGenerator anagramGenerator)
        {
            _config = config;
			_anagramGenerator = anagramGenerator;
        }


        [Microsoft.AspNetCore.Mvc.HttpGet()]
		public ActionResult Index()
		{
			return View();
		}
	}
}