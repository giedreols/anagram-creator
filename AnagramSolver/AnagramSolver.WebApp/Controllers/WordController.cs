using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class WordController : Controller
    {
        private readonly IWordServer _wordServer;
        private readonly ConfigOptionsDto _configOptions;
        private readonly IWordLogService _wordLogService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WordController(IWordServer wordServer, IWordLogService wordLogService, MyConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _wordServer = wordServer;
            _wordLogService = wordLogService;
            _configOptions = config.ConfigOptions;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public ActionResult Delete(string inputWord)
        {
            int wordId = _wordServer.DeleteWord(inputWord);

            if (wordId > 0)
            {
                string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _wordLogService.Log(wordId, ipAddress, WordOpEnum.DELETE);
            }

            return RedirectToAction("Index", "WordList");
        }

        [HttpGet]
        public ActionResult Update(string oldForm, string newForm)
        {
            int wordId = _wordServer.UpdateWord(oldForm, newForm);

            if (wordId > 0)
            {
                string ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _wordLogService.Log(wordId, ipAddress, WordOpEnum.EDIT);
            }
            // alikti tame paciame lape, kuriame buvo

            return RedirectToAction("Index", "WordList");
        }
    }
}
