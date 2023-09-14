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

        public WordsPerPageDto GetMatchingWords(string inputWord, int page = 1, int pageSize = 100)
        {
            Dictionary<int, string> matchingWords = _wordRepo.GetMatchingWords(inputWord);

            return SordWordsByPage(matchingWords, page, pageSize);
        }

        public WordsPerPageDto GetWordsByPage(int page = 1, int pageSize = 100)
        {
            Dictionary<int, string> allWords = _wordRepo.GetWords();

            return SordWordsByPage(allWords, page, pageSize);
        }

        private static WordsPerPageDto SordWordsByPage(Dictionary<int, string> words, int page, int pageSize)
        {
            Dictionary<int, string> allWords = words.OrderBy(w => w.Value).ToDictionary(w => w.Key, w => w.Value);

            var totalPages = (int)Math.Ceiling(allWords.Count / (double)pageSize);

            Dictionary<int, string> currentPageItems = allWords
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToDictionary(w => w.Key, w => w.Value);

            return new WordsPerPageDto(currentPageItems, pageSize, allWords.Count);
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

        public NewWordDto SaveWord(FullWordDto word, ConfigOptionsDto config)
        {
            NewWordDto newWord = new();

            var errorMessage = InputWordValidator.Validate(word.OtherForm, config.MinLength, config.MaxLength);

            if (errorMessage != null)
            {
                newWord.IsSaved = false; newWord.ErrorMessage = errorMessage;
            }
            else if (_wordRepo.IsWordExists(word.OtherForm) != 0)
            {
                newWord.IsSaved = false; newWord.ErrorMessage = ErrorMessages.AlreadyExists;
            }
            else
            {
                word.PartOfSpeechId = _partOfSpeechRepo.InsertPartOfSpeechIfDoesNotExist(word.PartOfSpeechAbbreviation);
                newWord.Id = _wordRepo.Add(word);
                newWord.IsSaved = newWord.Id > 0;

                if (!newWord.IsSaved)
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

        public NewWordDto UpdateWord(int wordId, string newForm, ConfigOptionsDto config)
        {
            NewWordDto newWord = new();

            var errorMessage = InputWordValidator.Validate(newForm, config.MinLength, config.MaxLength);

            if (errorMessage != null)
            {
                newWord.IsSaved = false; newWord.ErrorMessage = errorMessage;
            }
            else if (_wordRepo.IsWordExists(newForm) != 0)
            {
                newWord.IsSaved = false; newWord.ErrorMessage = ErrorMessages.AlreadyExists;
            }
            else
            {
                FullWordDto word = new(wordId, newForm);
                newWord.IsSaved = _wordRepo.Update(word);

                if (!newWord.IsSaved)
                {
                    newWord.ErrorMessage = ErrorMessages.UnknowReason;
                }
            }

            return newWord;
        }
    }
}
