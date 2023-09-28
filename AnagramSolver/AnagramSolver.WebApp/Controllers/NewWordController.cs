using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class NewWordController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            return View();
        }
    }
}
