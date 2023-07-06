using Microsoft.AspNetCore.Mvc;


namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Route("{action=Index}")]

    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}