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
        
        public WordApiController(IWordServer wordServer)
        {
            _wordServer = wordServer;
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
            WordsPerPageDto wordsPerPage = await _wordServer.GetWordsByPageAsync(page);

            WordListViewModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, wordsPerPage.PageSize);

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

            WordsPerPageDto wordsPerPage = await _wordServer.GetWordsByPageAsync(page);

            WordListViewModel viewModel = new(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, wordsPerPage.PageSize);

            return Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsyncApi(int wordId, string oldForm, string newForm, int page)
        {
            IEnumerable<string> anagrams;

            WordResultDto updatedWord = await _wordServer.UpdateWordAsync(wordId, newForm);

            if (updatedWord.Word == newForm)
            {
                WordsPerPageDto wordsPerPage = await _wordServer.GetWordsByPageAsync(page);

                return Ok(new WordListViewModel(wordsPerPage.Words, page, wordsPerPage.TotalWordsCount, wordsPerPage.PageSize));
            }

            // not upddated - error
            return Ok();
        }
    }
}
