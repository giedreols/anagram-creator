using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class WordListController : Controller
    {
        private readonly IWordServer _wordServer;
        private readonly IFileService _fileService;

        private readonly int pageSize;

        public WordListController(IWordServer wordServer, IFileService fileService, MyConfiguration congif)
        {
            _wordServer = wordServer;
            _fileService = fileService;
            pageSize = congif.ConfigOptions.TotalAmount;
        }

        [HttpGet]
        public IActionResult Index(int page = 1)
        {
            ViewData["Message"] = TempData["Message"];

            WordsPerPageDto wordsPerPage = _wordServer.GetWordsByPageAsync(page, pageSize);

            WordListViewModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, pageSize);

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync(string inputWord = "", int page = 1)
        {
            if (inputWord.IsNullOrEmpty())
                return Index();

            ViewData["Word"] = inputWord;

            WordsPerPageDto matchingWords = await _wordServer.GetMatchingWordsAsync(inputWord, page, pageSize);

            WordListViewModel viewModel = new(matchingWords.Words, page, matchingWords.TotalWordsCount, pageSize);

            return View("Index", viewModel);
        }

        [HttpGet]
        public IActionResult DownloadFile()
        {
            byte[] fileInBytes = _fileService.GetFileWithWords();

            return File(fileInBytes, "text/plain", "zodynas.txt");
        }
    }
}
