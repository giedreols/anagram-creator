﻿using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using System.Data;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class WordDataBaseAccess : IWordRepository
    {
        private readonly IWordsActions _dbWordTableAccess;
        private readonly ISearchLogActions _dbSearchLogActions;

        public WordDataBaseAccess(IWordsActions dbWordTableAccess, ISearchLogActions searchLogActions)
        {
            _dbWordTableAccess = dbWordTableAccess;
            _dbSearchLogActions = searchLogActions;
        }

        public IEnumerable<string> GetWords()
        {
            List<string> words = _dbWordTableAccess.GetWords().ToList();
            return words;
        }

        public WordsPerPageDto GetMatchingWords(string inputWord, int page = 1, int pageSize = 100)
        {
            List<string> words = _dbWordTableAccess.GetMatchingWords(inputWord).ToList();
            IList<WordWithFormsDto> matchingWords = Converter.ConvertDictionaryWordListToAnagramWordList(words).OrderBy(a => a.LowerCaseForm).ToList();

            var totalPages = (int)Math.Ceiling(matchingWords.Count / (double)pageSize);

            List<string> currentPageItems = matchingWords.Skip((page - 1) * pageSize).Select(w => w.LowerCaseForm).Take(pageSize).ToList();

            return new WordsPerPageDto(currentPageItems, pageSize, matchingWords.Count);
        }

        public WordsPerPageDto GetWordsByPage(int page = 1, int pageSize = 100)
        {
            var allWords = GetWords().OrderBy(w => w).ToList();
            var totalPages = (int)Math.Ceiling(allWords.Count / (double)pageSize);

            List<string> currentPageItems = allWords.Skip((page - 1) * pageSize).Select(w => w).Take(pageSize).ToList();

            return new WordsPerPageDto(currentPageItems, pageSize, allWords.Count);
        }

        public NewWordDto SaveWord(FullWordDto word, ConfigOptionsDto config)
        {
            var errorMessage = InputWordValidator.Validate(word.OtherForm, config.MinLength, config.MaxLength);

            NewWordDto newWord = new NewWordDto();

            if (errorMessage != null)
            {
                newWord.IsSaved = false; newWord.ErrorMessage = errorMessage;
            }
            else if (_dbWordTableAccess.IsWordExists(word.OtherForm))
            {
                newWord.IsSaved = false; newWord.ErrorMessage = ErrorMessages.AlreadyExists;
            }
            else
            {
                newWord.IsSaved = _dbWordTableAccess.InsertWord(word);

                if (!newWord.IsSaved)
                {
                    newWord.ErrorMessage = ErrorMessages.UnknowReason;
                }
            }

            return newWord;
        }

        public byte[] GetFileWithWords()
        {
            IEnumerable<string> wordList = _dbWordTableAccess.GetWords();

            IList<string> stringList = new List<string>();

            foreach (var word in wordList)
            {
                stringList.Add(word);
            }

            string concatenatedString = string.Join("\n", stringList);

            byte[] fileBytes = Encoding.UTF8.GetBytes(concatenatedString);

            return fileBytes;
        }

        public void CacheAnagrams(WordWithAnagramsDto anagrams)
        {
            _dbWordTableAccess.InsertAnagrams(anagrams);
        }

        public void LogSearchInfo(SearchLogDto model)
        {
            _dbSearchLogActions.Add(model);
        }

        public SearchLogDto GetLastSearchInfo()
        {
            return _dbSearchLogActions.GetLastSearch();
        }

        public IEnumerable<string> GetAnagrams(string inputWord)
        {
            IEnumerable<string> anagrams = _dbWordTableAccess.GetAnagrams(inputWord);

            return anagrams.ToList();
        }
    }
}