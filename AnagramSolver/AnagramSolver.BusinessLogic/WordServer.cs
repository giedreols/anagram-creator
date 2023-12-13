using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Data;
using System.Net;

namespace AnagramSolver.BusinessLogic
{
    public class WordServer : IWordServer
    {
        private readonly IWordRepository _wordRepo;
        private readonly IPartOfSpeechRespository _partOfSpeechRepo;
        private readonly HttpClient _httpClient;
        private readonly ISearchLogService _searchLogService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConfigOptionsDto _configOptions;

        private readonly string _ipAddress;

        public WordServer(IWordRepository wordRepo, IPartOfSpeechRespository partOfSpeechRepo, HttpClient httpClient,
            ISearchLogService searchLogService, IHttpContextAccessor httpContextAccessor, IMyConfiguration config)
        {
            _wordRepo = wordRepo;
            _partOfSpeechRepo = partOfSpeechRepo;
            _httpClient = httpClient;
            _searchLogService = searchLogService;
            _httpContextAccessor = httpContextAccessor;

            _ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            _configOptions = config.ConfigOptions;
        }

        public async Task<WordsPerPageDto> GetMatchingWordsAsync(string inputWord, int page = 1, int pageSize = 100)
        {
            Dictionary<int, string> matchingWords = await _wordRepo.GetMatchingWordsAsync(inputWord);

            return await SordWordsByPageAsync(matchingWords, page, pageSize);
        }

        public async Task<WordsPerPageDto> GetWordsByPageAsync(int page = 1, int pageSize = 100)
        {
            Dictionary<int, string> allWords = await _wordRepo.GetWordsAsync();

            return await SordWordsByPageAsync(allWords, page, pageSize);
        }

        private static async Task<WordsPerPageDto> SordWordsByPageAsync(Dictionary<int, string> words, int page, int pageSize)
        {
            var allWords = await Task.Run(() => words.OrderBy(w => w.Value).ToList());

            var totalPages = (int)Math.Ceiling(allWords.Count / (double)pageSize);

            List<KeyValuePair<int, string>> currentPageItems = allWords
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            return new WordsPerPageDto(currentPageItems.ToDictionary(kv => kv.Key, kv => kv.Value), pageSize, allWords.Count);
        }

        public async Task<IEnumerable<string>> GetAnagramsAsync(string inputWord)
        {
            return await _wordRepo.GetAnagramsAsync(inputWord);
        }

        public async Task<GetAnagramsResultDto> GetAnagramsNewAsync(string inputWord)
        {
            var result = new GetAnagramsResultDto();

            if (!await _searchLogService.HasSpareSearchAsync(_ipAddress, _configOptions.SearchCount))
            {
                result.ErrorMessages.Add(ErrorMessageEnum.SearchLimit);
                return result;
            }

            if (inputWord.IsNullOrEmpty())
            {
                result.ErrorMessages.Add(ErrorMessageEnum.Empty);
                return result;
            }

            result.WordAndAnagrams.Word = inputWord.Trim();
            result.WordAndAnagrams.WordId = await _wordRepo.GetWordIdAsync(inputWord);
            result.WordAndAnagrams.Anagrams = await _wordRepo.GetAnagramsAsync(inputWord);

            await _searchLogService.LogSearchAsync(inputWord, _ipAddress);

            return result;
        }

        public async Task<IEnumerable<string>> GetAnagramsUsingAnagramicaAsync(string inputWordAnagramica)
        {
            string apiUrl = $"http://www.anagramica.com/all/{inputWordAnagramica}";
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                AnagramApiResponse apiResponse = JsonConvert.DeserializeObject<AnagramApiResponse>(responseBody);

                var result = apiResponse.All.AsEnumerable().Where(w => w != inputWordAnagramica).Distinct().ToList();

                return result;
            }
            else
            {
                throw new Exception("Bad request. An error occurred while fetching data from the API.");
            }
        }

        public async Task<bool> DeleteWordAsync(int wordId)
        {
            return await _wordRepo.DeleteAsync(wordId);
        }

        // validacijos nesutampa su redagavimo
        public async Task<WordResultDto> SaveWordAsync(FullWordDto word, ConfigOptionsDto config)
        {
            WordResultDto newWord = new();

            var errorMessage = InputWordValidator.Validate(word.OtherForm, config.MinLength, config.MaxLength);

            if (errorMessage != ErrorMessageEnum.Ok)
            {
                newWord.ErrorMessage = errorMessage;
            }
            else if (await _wordRepo.GetWordIdAsync(word.OtherForm) != 0)
            {
                newWord.ErrorMessage = ErrorMessageEnum.AlreadyExists;
            }
            else
            {
                word.PartOfSpeechId = await _partOfSpeechRepo.InsertPartOfSpeechIfDoesNotExistAsync(word.PartOfSpeechAbbreviation);
                newWord.Id = await _wordRepo.AddAsync(word);

                if (newWord.Id == 0)
                {
                    newWord.ErrorMessage = ErrorMessageEnum.UnknowReason;
                }
            }

            return newWord;
        }

        public async Task<int> GetWordIdAsync(string word)
        {
            return await _wordRepo.GetWordIdAsync(word);
        }

        public async Task<WordResultDto> UpdateWordAsync(int wordId, string newForm, ConfigOptionsDto config)
        {
            WordResultDto updatedWord = new();

            var errorMessage = InputWordValidator.Validate(newForm, config.MinLength, config.MaxLength);

            if (errorMessage != ErrorMessageEnum.Ok)
            {
                updatedWord.ErrorMessage = errorMessage;
            }
            else if (await _wordRepo.GetWordIdAsync(newForm) != 0)
            {
                updatedWord.ErrorMessage = ErrorMessageEnum.AlreadyExists;
            }
            else
            {
                FullWordDto word = new(wordId, newForm);
                bool isUpdated = await _wordRepo.UpdateAsync(word);

                if (!isUpdated)
                {
                    updatedWord.ErrorMessage = ErrorMessageEnum.UnknowReason;
                }
                else
                {
                    updatedWord.Word = newForm;
                }
            }
            return updatedWord;
        }
    }
}
