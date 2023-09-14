using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Dtos.Obsolete;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.BusinessLogic.DictionaryFromFile
{
    [Obsolete("new implementation uses database")]
    public class DictionaryFromFileActions : IWordServer
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
            throw new NotImplementedException();
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
            List<string> linesList = Parser.ParseLinesWithTabs(text);
            List<string> wordList = Parser.ParseWordsFromDictionaryFile(linesList);

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

        public int GetSearchCount(string ipAddress)
        {
            throw new NotImplementedException();
        }

        public bool DeleteWord(int wordId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateWord(int wordId, string newForm)
        {
            throw new NotImplementedException();
        }

        public NewWordDto UpdateWord(int wordId, string newForm, ConfigOptionsDto config)
        {
            throw new NotImplementedException();
        }

        public int GetWordId(string word)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<string> GetAnagramsUsingAnagramicaAsync(string word)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<string>> IWordServer.GetAnagramsUsingAnagramicaAsync(string word)
        {
            throw new NotImplementedException();
        }
    }
}
