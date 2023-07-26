using AnagramSolver.BusinessLogic.Helpers;
using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Dtos.Obsolete;
using AnagramSolver.Contracts.Interfaces;
using System.Data;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class WordServer : IWordServer
    {
        private readonly IWordRepository _wordRepo;

        public WordServer(IWordRepository _wordRepo)
        {
            this._wordRepo = _wordRepo;
        }

        public IEnumerable<string> GetWords()
        {
            List<string> words = _wordRepo.GetWords().ToList();
            return words;
        }

        public WordsPerPageDto GetMatchingWords(string inputWord, int page = 1, int pageSize = 100)
        {
            List<string> words = _wordRepo.GetMatchingWords(inputWord).OrderBy(a => a).ToList();

            var totalPages = (int)Math.Ceiling(words.Count / (double)pageSize);

            List<string> currentPageItems = words.Skip((page - 1) * pageSize).Select(w => w).Take(pageSize).ToList();

            return new WordsPerPageDto(currentPageItems, pageSize, words.Count);
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
            else if (_wordRepo.IsWordExists(word.OtherForm))
            {
                newWord.IsSaved = false; newWord.ErrorMessage = ErrorMessages.AlreadyExists;
            }
            else
            {
                newWord.Id = _wordRepo.Add(word);
                newWord.IsSaved = newWord.Id > 0;

                if (!newWord.IsSaved)
                {
                    newWord.ErrorMessage = ErrorMessages.UnknowReason;
                }
            }            

            return newWord;
        }

        public IEnumerable<string> GetAnagrams(string inputWord)
        {
            IEnumerable<string> anagrams = _wordRepo.GetAnagrams(inputWord);

            return anagrams.ToList();
        }

        public int DeleteWord(string word)
        {
            return _wordRepo.Delete(word);
        }

        public int UpdateWord(string oldForm, string newForm)
        {
            //return _wordRepo.Update(oldForm, newForm);
            return 0;
        }
    }
}
