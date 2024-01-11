using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.ApiBackend.Controllers
{
    [Route("[Controller]/[Action]")]
    [ApiController]
    public class WordApiController : ControllerBase
    {
        private readonly IWordServer _wordServer;
        private readonly ConfigOptionsDto _configOptions;

        private readonly int _pageSize;


        public WordApiController(IWordServer wordServer, IConfigReader config)
        {
            _wordServer = wordServer;
            _configOptions = config.ConfigOptions;

            _pageSize = config.ConfigOptions.TotalAmount;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsyncApi(string inputWord = "")
        {
            var result = await _wordServer.GetAnagramsNewAsync(inputWord);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> CheckIfWordExistsAsyncApi(string inputWord = "")
        {
            int wordId = await _wordServer.GetWordIdAsync(inputWord);

            return Ok(new { Exists = wordId > 0 });
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsyncApi(int page = 1)
        {
            WordsPerPageDto wordsPerPage = await _wordServer.GetWordsByPageAsync(page, _pageSize);

            WordListViewModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, _pageSize);

            return Ok(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsyncApi(int wordId, int page = 1)
        {
            bool isDeleted = await _wordServer.DeleteWordAsync(wordId);

            if (!isDeleted)
            {
                //  show info message "Žodžio ištrinti nepavyko. Kažkas blogai suprogramuota, sorry."
            }

            WordsPerPageDto wordsPerPage = await _wordServer.GetWordsByPageAsync(page, _pageSize);

            WordListViewModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, _pageSize);

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsyncApi(int wordId, string oldForm, string newForm, int page)
        {
            IEnumerable<string> anagrams;

            if (newForm == oldForm)
            {
                // do nothing
            }

            WordResultDto updatedWord = await _wordServer.UpdateWordAsync(wordId, newForm, _configOptions);

            if (updatedWord.Word == newForm)
            {
                WordsPerPageDto wordsPerPage = await _wordServer.GetWordsByPageAsync(page, _pageSize);

                return Ok(new WordListViewModel(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, _pageSize));
            }

            // not upddated - error
            return Ok();
        }
    }
}
