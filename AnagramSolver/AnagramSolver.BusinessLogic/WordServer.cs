using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.Models;
using Newtonsoft.Json;
using System.Data;

namespace AnagramSolver.BusinessLogic
{
    public class WordServer : IWordServer
    {
        private readonly IWordRepository _wordRepo;
        private readonly IPartOfSpeechRespository _partOfSpeechRepo;
        private readonly HttpClient _httpClient;


        public WordServer(IWordRepository _wordRepo, IPartOfSpeechRespository partOfSpeechRepo, HttpClient httpClient)
        {
            this._wordRepo = _wordRepo;
            _partOfSpeechRepo = partOfSpeechRepo;
            _httpClient = httpClient;
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

        public IEnumerable<string> GetAnagrams(string inputWord)
        {
            IEnumerable<string> anagrams = _wordRepo.GetAnagrams(inputWord);

            return anagrams.ToList();
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

        public bool DeleteWord(int wordId)
        {
            return _wordRepo.Delete(wordId);
        }

        public WordResultDto SaveWord(FullWordDto word, ConfigOptionsDto config)
        {
            WordResultDto newWord = new();

            var errorMessage = InputWordValidator.Validate(word.OtherForm, config.MinLength, config.MaxLength);

            if (errorMessage != null)
            {
                newWord.ErrorMessage = errorMessage;
            }
            else if (_wordRepo.IsWordExists(word.OtherForm) != 0)
            {
                newWord.ErrorMessage = ErrorMessages.AlreadyExists;
            }
            else
            {
                word.PartOfSpeechId = _partOfSpeechRepo.InsertPartOfSpeechIfDoesNotExist(word.PartOfSpeechAbbreviation);
                newWord.Id = _wordRepo.Add(word);

                if (newWord.Id == 0)
                {
                    newWord.ErrorMessage = ErrorMessages.UnknowReason;
                }
            }

            return newWord;
        }

        public int GetWordId(string word)
        {
            return _wordRepo.IsWordExists(word);
        }

        public WordResultDto UpdateWord(int wordId, string newForm, ConfigOptionsDto config)
        {
            WordResultDto updatedWord = new();

            var errorMessage = InputWordValidator.Validate(newForm, config.MinLength, config.MaxLength);

            if (errorMessage != null)
            {
                updatedWord.ErrorMessage = errorMessage;
            }
            else if (_wordRepo.IsWordExists(newForm) != 0)
            {
                updatedWord.ErrorMessage = ErrorMessages.AlreadyExists;
            }
            else
            {
                FullWordDto word = new(wordId, newForm);
                bool isUpdated = _wordRepo.Update(word);

                if (!isUpdated)
                {
                    updatedWord.ErrorMessage = ErrorMessages.UnknowReason;
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
