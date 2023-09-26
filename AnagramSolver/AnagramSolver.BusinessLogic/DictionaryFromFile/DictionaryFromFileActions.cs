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

        public WordsPerPageDto GetWordsByPageAsync(int page = 1, int pageSize = 100)
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
    }
}
