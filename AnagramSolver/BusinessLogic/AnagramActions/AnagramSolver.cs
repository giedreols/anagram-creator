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
            IList<DictWord> words = wordRepository.GetWords();
            List<AnagramWord> tempList = new();

            foreach (var word in words)
            {
                tempList.Add(new AnagramWord(word.MainForm));
                tempList.Add(new AnagramWord(word.AnotherForm));
            }

            tempList = tempList.DistinctBy(word => word.LowerCaseForm).ToList();
            tempList = tempList.OrderByDescending(word => word.MainForm.Length).ToList();
            WordList = tempList.ToImmutableList();
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


