using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;

namespace AnagramSolver.EF.DbFirst
{
    public class WordRepo : IWordRepository
    {
        public Dictionary<int, string> GetMatchingWords(string inputWord)
        {
            using var context = new AnagramSolverDataContext();

            Dictionary<int, string> matchingItems = context.Words
                        .Where(item => item.OtherForm.Contains(inputWord))
                        .Where(item => !item.IsDeleted)
                        .ToDictionary(w => w.Id, w => w.OtherForm);

            return matchingItems;
        }

        public Dictionary<int, string> GetWords()
        {
            var FullWordList = new List<FullWordDto>();

            using var context = new AnagramSolverDataContext();

            Dictionary<int, string> words = context.Words
                .Where(item => !item.IsDeleted)
                .ToDictionary(w => w.Id, w => w.OtherForm);

            return words;
        }

        public int Add(FullWordDto parameters)
        {
            using var context = new AnagramSolverDataContext();

            var newWord = new Word
            {
                MainForm = parameters.MainForm,
                OtherForm = parameters.OtherForm,
                OrderedForm = new(parameters.OtherForm.ToLower(System.Globalization.CultureInfo.CurrentCulture).OrderByDescending(a => a).ToArray()),
            };

            context.Words.Add(newWord);

            bool isSaved = context.SaveChanges() > 0;

            if (isSaved)
                return newWord.Id;

            return 0;
        }

        public bool Update(FullWordDto parameters)
        {
            using var context = new AnagramSolverDataContext();

            var wordToUpdate = context.Words
                .Where(w => !w.IsDeleted)
                .FirstOrDefault(w => w.Id == parameters.Id);

            if (wordToUpdate != null)
            {
                wordToUpdate.OtherForm = parameters.OtherForm;
                wordToUpdate.MainForm = parameters.MainForm;
                wordToUpdate.OrderedForm = new(parameters.OtherForm.ToLower(System.Globalization.CultureInfo.CurrentCulture).OrderByDescending(a => a).ToArray());
            }

            bool result = context.SaveChanges() > 0;

            return result;
        }

        public bool Delete(int wordId)
        {
            using var context = new AnagramSolverDataContext();

            var wordTodelete = context.Words
                .Where(w => !w.IsDeleted)
                .FirstOrDefault(w => w.Id == wordId);

            if (wordTodelete != null)
                wordTodelete.IsDeleted = true;

            return context.SaveChanges() > 0;

        }

        public bool IsWordExists(string inputWord)
        {
            using var context = new AnagramSolverDataContext();

            return context.Words.Any(w => w.OtherForm.Equals(inputWord));
        }

        public IEnumerable<string> GetAnagrams(string word)
        {
            using var context = new AnagramSolverDataContext();

            string orderedWord = new(word.Replace(" ", "").OrderByDescending(w => w).ToArray());

            IEnumerable<string> anagrams = context.Words
                                                    .Where(w => w.OrderedForm == orderedWord && w.OtherForm != word)
                                                    .Select(x => x.OtherForm)
                                                    .Distinct()
                                                    .OrderBy(w => w)
                                                    .ToList();
            return anagrams;
        }

        public void AddOrderedFormForAllWords()
        {
            using var context = new AnagramSolverDataContext();

            List<Word> words = context.Words.ToList();

            foreach (Word word in words)
            {
                word.OrderedForm = new string(word.OtherForm.ToLower().OrderByDescending(c => c).ToArray());
                context.Update(word);
            }

            context.SaveChanges();
        }
    }
}
