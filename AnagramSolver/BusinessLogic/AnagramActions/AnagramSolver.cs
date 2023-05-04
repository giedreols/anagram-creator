using Contracts.Interfaces;
using Contracts.Models;
using System.Collections.Immutable;

namespace BusinessLogic.AnagramActions
{
    public class AnagramSolver : IAnagramSolver
    {
        private ImmutableList<AnagramWord> WordList { get; set; }

        public AnagramSolver(IWordRepository wordRepository)
        {
            WordList = wordRepository.GetWords();
        }

        public List<string> GetAnagrams(string inputWord)
        {
            List<string> anagrams = new();
            var loweredInputWord = inputWord.ToLower().Replace(" ", "");
            var inputSequence = loweredInputWord.OrderByDescending(c => c);

            foreach (var word in WordList)
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


