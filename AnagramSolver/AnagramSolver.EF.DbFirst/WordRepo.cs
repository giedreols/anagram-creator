using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.DbFirst
{
    public class WordRepo : IWordRepository
    {
        private readonly AnagramSolverDataContext _context;

        public WordRepo(AnagramSolverDataContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<int, string>> GetMatchingWordsAsync(string inputWord)
        {
            Dictionary<int, string> matchingItems = await _context.Words
                        .Where(item => item.OtherForm.Contains(inputWord))
                        .Where(item => !item.IsDeleted)
                        .ToDictionaryAsync(w => w.Id, w => w.OtherForm);

            return matchingItems;
        }

        public async Task<Dictionary<int, string>> GetWordsAsync()
        {
            var FullWordList = new List<FullWordDto>();

            Dictionary<int, string> words = await _context.Words
                .Where(item => !item.IsDeleted)
                .ToDictionaryAsync(w => w.Id, w => w.OtherForm);

            return words;
        }

        public async Task<int> AddAsync(FullWordDto parameters)
        {
            var newWord = new Word
            {
                MainForm = parameters.MainForm,
                OtherForm = parameters.OtherForm,
                PartOfSpeechId = parameters.PartOfSpeechId > 0 ? parameters.PartOfSpeechId : null,
                OrderedForm = new(parameters.OtherForm.ToLower(System.Globalization.CultureInfo.CurrentCulture).OrderByDescending(a => a).ToArray()),
            };

            await _context.Words.AddAsync(newWord);

            bool isSaved = await _context.SaveChangesAsync() > 0;

            if (isSaved)
                return newWord.Id;

            return 0;
        }

        public async Task<bool> AddListAsync(IList<FullWordDto> words)
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

            await _context.Words.AddRangeAsync(list);

            bool isSaved = await _context.SaveChangesAsync() == words.Count;

            return isSaved;
        }

        public async Task<bool> UpdateAsync(FullWordDto parameters)
        {
            var wordToUpdate = await _context.Words
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(w => w.Id == parameters.Id);

            if (wordToUpdate != null)
            {
                wordToUpdate.OtherForm = parameters.OtherForm;
                wordToUpdate.MainForm = parameters.MainForm;
                wordToUpdate.OrderedForm = new(parameters.OtherForm.ToLower(System.Globalization.CultureInfo.CurrentCulture).OrderByDescending(a => a).ToArray());
            }

            bool result = await _context.SaveChangesAsync() > 0;

            return result;
        }

        public async Task<bool> DeleteAsync(int wordId)
        {
            var wordTodelete = await _context.Words
                .Where(w => !w.IsDeleted)
                .FirstOrDefaultAsync(w => w.Id == wordId);

            if (wordTodelete != null)
                wordTodelete.IsDeleted = true;

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<int> IsWordExistsAsync(string inputWord)
        {
            var word = await _context.Words.Where(w => !w.IsDeleted).FirstOrDefaultAsync(w => w.OtherForm.Equals(inputWord));

            return word != null ? word.Id : 0;
        }

        public async Task<IEnumerable<string>> GetAnagramsAsync(string word)
        {
            string orderedWord = new(word.Replace(" ", "").OrderByDescending(w => w).ToArray());

            var anagrams = await _context.Words
                                                    .Where(w => w.OrderedForm == orderedWord && w.OtherForm != word)
                                                    .Select(x => x.OtherForm)
                                                    .Distinct()
                                                    .OrderBy(w => w)
                                                    .ToListAsync();

            return anagrams;
        }
    }
}
