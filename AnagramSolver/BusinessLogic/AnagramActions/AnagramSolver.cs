using BusinessLogic.DictionaryActions;
using BusinessLogic.InputWordActions;
using Contracts.Interfaces;
using Contracts.Models;

namespace BusinessLogic.AnagramActions
{
    public class AnagramSolver : IAnagramSolver
    {
        private HashSet<string> wordList { get; set; }

        public AnagramSolver()
        {
            wordList = new WordRepository().GetWords().Select(word => word.MainForm).ToHashSet();
        }

        public List<string> GetAnagrams(InputWord inputWord)
        {
            Permutator permutator = new Permutator();
            permutator.GeneratePermutations(inputWord);

            // unnecessary null check (input word is not supposed to be null) + it should be at the beginning of the method
            if (inputWord == null)
                return new List<string>();

            return wordList.Intersect(inputWord.Permutations).ToList();
        }
    }
}

