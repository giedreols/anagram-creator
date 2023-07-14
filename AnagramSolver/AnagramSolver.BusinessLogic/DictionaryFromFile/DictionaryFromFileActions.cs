﻿using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.DictionaryFromFile
{
    [Obsolete("new implementation uses database")]
    public class DictionaryFromFileActions : IWordRepository
    {
        private readonly IFileReader _fileReader;

        public DictionaryFromFileActions(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }

        public IEnumerable<string> GetWords()
        {
            List<string> words = ReadWords();

            return words;
        }

        public WordsPerPageDto GetWordsByPage(int page = 1, int pageSize = 100)
        {
            var allWords = GetWords().OrderBy(w => w).ToList();
            var totalPages = (int)Math.Ceiling(allWords.Count / (double)pageSize);

            List<string> currentPageItems = allWords.Skip((page - 1) * pageSize).Select(w => w).Take(pageSize).ToList();

            return new WordsPerPageDto(currentPageItems, pageSize, allWords.Count);
        }

        public bool SaveWord(string word)
        {
            IEnumerable<string> currentWords = GetWords();

            foreach (var existingWord in currentWords)
            {
                if (existingWord.ToLower() == word.ToLower())
                {
                    return false;
                }
            }

            word += $"\t.\t{word}\t.";
            _fileReader.WriteFile(word);
            return true;
        }

        public byte[] GetFileWithWords()
        {
            var fileBytes = _fileReader.GetFile();

            return fileBytes;
        }

        private List<string> ReadWords()
        {
            string text = _fileReader.ReadFile("zodynas.txt");
            List<string> linesList = Converter.ParseLinesWithTabs(text);
            List<string> wordList = Converter.ParseWordsFromDictionaryFile(linesList);

            return wordList;
        }


        public NewWordDto SaveWord(FullWordDto word, ConfigOptionsDto config)
        {
            throw new NotImplementedException();
        }

        public WordsPerPageDto GetMatchingWords(string inputWord, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAnagrams(string word)
        {
            throw new NotImplementedException();
        }

        public void CacheAnagrams(WordWithAnagramsDto anagrams)
        {
            throw new NotImplementedException();
        }

        public void LogSearchInfo(SearchLogDto model)
        {
            throw new NotImplementedException();
        }

        public SearchLogDto GetLastSearchInfo()
        {
            throw new NotImplementedException();
        }
    }
}