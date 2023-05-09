using Contracts.Interfaces;
using Contracts.Models;
using System.Collections.Immutable;

namespace BusinessLogic.AnagramActions
{
    public class AnagramSolver : IAnagramSolver
    {
        private ImmutableList<AnagramWord> _wordList { get; set; }

        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordList = wordRepository.GetWords();
        }

        public List<string> GetAnagrams(string inputWord)
        {
            List<string> anagrams = new();
            var loweredInputWord = inputWord.ToLower().Replace(" ", "");
            var inputSequence = loweredInputWord.OrderByDescending(c => c);

            foreach (var word in _wordList)
            {
                if (inputSequence.SequenceEqual(word.OrderedForm) && word.LowerCaseForm != loweredInputWord)
                {
                    anagrams.Add(word.MainForm);
                }
            }

            return anagrams;
        }
    }
}


