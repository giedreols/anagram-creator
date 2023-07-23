﻿using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Dtos.Obsolete;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.EF.DbFirst.Entities;

namespace AnagramSolver.EF.DbFirst
{
    // WordRepository

    public class WordRepo : IWordRepository
    {
        public IEnumerable<string> GetMatchingWords(string inputWord)
        {
            using var context = new AnagramSolverDataContext();

            IQueryable<string> matchingItems = context.Words
                        .Where(item => item.OtherForm.Contains(inputWord))
                        .Select(w => w.OtherForm)
                        .Distinct();

            return matchingItems.ToList();
        }

        public IEnumerable<string> GetWords()
        {
            var FullWordList = new List<FullWordDto>();

            using var context = new AnagramSolverDataContext();

            var words = context.Words.Select(w => w.OtherForm).Distinct().ToList();

            return words;
        }

        // bukesni repo metodai privalumas
        // galima insert part of speech iskelti i business logic ir butu gal ir geriau
        // insert part of speech if does not exist

        public bool InsertWord(FullWordDto parameters)
        {
            using var context = new AnagramSolverDataContext();

            int partOfSpeechId = 0;

            if (parameters.PartOfSpeechAbbreviation != null)
            {

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

                partOfSpeechId = context.PartsOfSpeech
                                    .First(x => x.Abbreviation.Equals(parameters.PartOfSpeechAbbreviation))
                                    .Id;
            }

            var newWord = new Word
            {
                MainForm = parameters.MainForm,
                OtherForm = parameters.OtherForm,
                OrderedForm = new(parameters.OtherForm.ToLower(System.Globalization.CultureInfo.CurrentCulture).OrderByDescending(a => a).ToArray()),
            };

            if (partOfSpeechId != 0)
            {
                newWord.PartOfSpeechId = partOfSpeechId;
            }

            context.Words.Add(newWord);

            bool result = context.SaveChanges() > 0;

            return result;
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

        public int InsertAnagrams(WordWithAnagramsDto anagrams)
        {
            throw new NotImplementedException();
        }
    }
}
