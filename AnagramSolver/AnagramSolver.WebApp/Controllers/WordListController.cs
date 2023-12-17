using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WordListViewModel = AnagramSolver.WebApp.Models.WordListViewModel;

namespace AnagramSolver.WebApp.Controllers
{
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class WordListController : Controller
    {
        private readonly IWordServer _wordServer;
        private readonly IFileService _fileService;

        private readonly int pageSize;

        public WordListController(IWordServer wordServer, IFileService fileService, MyConfiguration config)
        {
            _wordServer = wordServer;
            _fileService = fileService;
            pageSize = config.ConfigOptions.TotalAmount;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(int page = 1)
        {
            ViewData["Message"] = TempData["Message"];

            WordsPerPageDto wordsPerPage = await _wordServer.GetWordsByPageAsync(page, pageSize);

            Models.WordListViewModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, pageSize);

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SearchAsync(string inputWord = "", int page = 1)
        {
            if (inputWord.IsNullOrEmpty())
                return await IndexAsync();

            ViewData["Word"] = inputWord;

            WordsPerPageDto matchingWords = await _wordServer.GetMatchingWordsAsync(inputWord, page, pageSize);

            WordListViewModel viewModel = new(matchingWords.Words, page, matchingWords.TotalWordsCount, pageSize);

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadFileAsync()
        {
            byte[] fileInBytes = await _fileService.GetFileWithWordsAsync();

            return File(fileInBytes, "text/plain", "zodynas.txt");
        }
    }
}
