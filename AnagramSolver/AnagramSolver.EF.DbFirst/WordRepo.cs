using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;

namespace AnagramSolver.EF.DbFirst
{
    public class WordRepo : IWordRepository
    {
        private readonly AnagramSolverDataContext _context;

        public WordRepo(AnagramSolverDataContext context)
        {
            _context = context;
        }

        public Dictionary<int, string> GetMatchingWords(string inputWord)
        {
            Dictionary<int, string> matchingItems = _context.Words
                        .Where(item => item.OtherForm.Contains(inputWord))
                        .Where(item => !item.IsDeleted)
                        .ToDictionary(w => w.Id, w => w.OtherForm);

            return matchingItems;
        }

        public Dictionary<int, string> GetWords()
        {
            var FullWordList = new List<FullWordDto>();

            Dictionary<int, string> words = _context.Words
                .Where(item => !item.IsDeleted)
                .ToDictionary(w => w.Id, w => w.OtherForm);

            return words;
        }

        public int Add(FullWordDto parameters)
        {
            var newWord = new Word
            {
                MainForm = parameters.MainForm,
                OtherForm = parameters.OtherForm,
                PartOfSpeechId = parameters.PartOfSpeechId > 0 ? parameters.PartOfSpeechId : null,
                OrderedForm = new(parameters.OtherForm.ToLower(System.Globalization.CultureInfo.CurrentCulture).OrderByDescending(a => a).ToArray()),
            };

            _context.Words.Add(newWord);

            bool isSaved = _context.SaveChanges() > 0;

            if (isSaved)
                return newWord.Id;

            return 0;
        }

        public bool AddList(IList<FullWordDto> words)
        {
            List<Word> list = new();

            foreach (var word in words)
            {
                list.Add(new Word
                {
                    MainForm = word.MainForm,
                    OtherForm = word.OtherForm,
                    PartOfSpeechId = word.PartOfSpeechId > 0 ? word.PartOfSpeechId : null,
                    OrderedForm = new(word.OtherForm.ToLower(System.Globalization.CultureInfo.CurrentCulture).OrderByDescending(a => a).ToArray()),
                });
            }

            _context.Words.AddRangeAsync(list);

            bool isSaved = _context.SaveChanges() == words.Count;

            return isSaved;
        }

        public bool Update(FullWordDto parameters)
        {
            var wordToUpdate = _context.Words
                .Where(w => !w.IsDeleted)
                .FirstOrDefault(w => w.Id == parameters.Id);

            if (wordToUpdate != null)
            {
                wordToUpdate.OtherForm = parameters.OtherForm;
                wordToUpdate.MainForm = parameters.MainForm;
                wordToUpdate.OrderedForm = new(parameters.OtherForm.ToLower(System.Globalization.CultureInfo.CurrentCulture).OrderByDescending(a => a).ToArray());
            }

            bool result = _context.SaveChanges() > 0;

            return result;
        }

        public bool Delete(int wordId)
        {
            var wordTodelete = _context.Words
                .Where(w => !w.IsDeleted)
                .FirstOrDefault(w => w.Id == wordId);

            if (wordTodelete != null)
                wordTodelete.IsDeleted = true;

            return _context.SaveChanges() > 0;

        }

        public int IsWordExists(string inputWord)
        {
            var word = _context.Words.FirstOrDefault(w => w.OtherForm.Equals(inputWord));

            return word != null ? word.Id : 0;
        }

        public IEnumerable<string> GetAnagrams(string word)
        {
            string orderedWord = new(word.Replace(" ", "").OrderByDescending(w => w).ToArray());

            IEnumerable<string> anagrams = _context.Words
                                                    .Where(w => w.OrderedForm == orderedWord && w.OtherForm != word)
                                                    .Select(x => x.OtherForm)
                                                    .Distinct()
                                                    .OrderBy(w => w)
                                                    .ToList();
            return anagrams;
        }
    }
}
