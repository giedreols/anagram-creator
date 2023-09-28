﻿using AnagramSolver.Contracts.Interfaces;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MyConfiguration _config;

        public HomeController(ISearchLogService logService, IHttpContextAccessor httpContextAccessor, MyConfiguration config)
        {
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }

        // jei neturiu ka veikt, galiu errora pataisyt cia dar

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            if (await _logService.HasSpareSearchAsync(ipAddress, _config.ConfigOptions.SearchCount))
                return View();

            else return View(new ErrorModel("Anagramų paieškų limitas iš šio IP adreso išnaudotas. Nori daugiau paieškų?"));
        }
    }
}