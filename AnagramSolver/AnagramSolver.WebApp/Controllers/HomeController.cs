using Microsoft.AspNetCore.Mvc;
using System.Web.Http;

namespace AnagramSolver.WebApp.Controllers
{
	[RoutePrefix("[Controller]")]
	[Microsoft.AspNetCore.Mvc.Route("{action=Index}")]
	public class HomeController : Controller
	{
		[Microsoft.AspNetCore.Mvc.HttpGet()]
		public ActionResult Index()
		{
			return View();
		}
	}
}