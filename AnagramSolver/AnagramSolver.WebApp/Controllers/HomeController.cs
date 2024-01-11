using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    [Route("{action=Index}")]

    public class HomeController : Controller
    {
        private readonly ISearchLogService _logService;

        public HomeController(ISearchLogService logService)
        {
            _logService = logService;
        }

        // jei neturiu ka veikt, galiu errora pataisyt cia dar

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            if (await _logService.HasSpareSearchAsync())
                return View();

            else return View(new ErrorModel("Anagramų paieškų limitas iš šio IP adreso išnaudotas. Nori daugiau paieškų?"));
        }
    }
}