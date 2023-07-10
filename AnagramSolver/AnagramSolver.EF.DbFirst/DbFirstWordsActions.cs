using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;

namespace AnagramSolver.EF.DbFirst
{
    public class DbFirstWordsActions : IWordsActions
    {
        public IEnumerable<FullWordDto> GetMatchingWords(string inputWord)
        {
            using var context = new AnagramSolverDataContext();

            IQueryable<string> matchingItems = context.Words
                        .Where(item => item.OtherForm.Contains(inputWord))
                        .Select(w => w.OtherForm);

            List<FullWordDto> convertedWords = new();

            foreach (string word in matchingItems)
            {
                convertedWords.Add(new FullWordDto(word));
            }

            return convertedWords;
        }

        public IEnumerable<FullWordDto> GetWords()
        {
            var FullWordList = new List<FullWordDto>();

            using var context = new AnagramSolverDataContext();

            var words = context.Words.Select(w => w.OtherForm).ToList();

            FullWordList.AddRange(words.Select(w => new FullWordDto(w)));

            return FullWordList.AsEnumerable();
        }

        public bool InsertWord(FullWordDto parameters)
        {
            using var context = new AnagramSolverDataContext();

            var isPartOfSpeechExists = context.PartsOfSpeech
                                    .FirstOrDefault(x => x.Abbreviation.Equals(parameters.PartOfSpeechAbbreviation));

            if (isPartOfSpeechExists == null)
            {
                context.PartsOfSpeech.Add(new()
                {
                    Abbreviation = parameters.PartOfSpeechAbbreviation
                });
                context.SaveChanges();
            }

            var partOfSpeechId = context.PartsOfSpeech
                                .First(x => x.Abbreviation.Equals(parameters.PartOfSpeechAbbreviation))
                                .Id;

            var newWord = new Word
            {
                MainForm = parameters.MainForm,
                OtherForm = parameters.OtherForm,
                OrderedForm = new(parameters.OtherForm.ToLower(System.Globalization.CultureInfo.CurrentCulture).OrderByDescending(a => a).ToArray()),
                PartOfSpeechId = partOfSpeechId
            };

            context.Words.Add(newWord);

            bool result = context.SaveChanges() > 0;

            return result;
        }

        public bool IsWordExists(string inputWord)
        {
            using var context = new AnagramSolverDataContext();

            return context.Words.Any(w => w.OtherForm.Equals(inputWord));
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
