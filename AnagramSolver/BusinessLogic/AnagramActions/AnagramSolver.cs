using BusinessLogic.DictionaryActions;
using BusinessLogic.InputWordActions;
using Contracts.Interfaces;
using Contracts.Models;

namespace BusinessLogic.AnagramActions
{
    public class AnagramSolver : IAnagramSolver
    {
        private HashSet<string> WordList { get; set; }

        public AnagramSolver()
        {
            IWordRepository wordRepository = new WordDictionary();
            WordList = wordRepository.GetWords().Select(word => word.MainForm).ToHashSet();
            WordList.Intersect(wordRepository.GetWords().Select(word => word.AnotherForm).ToHashSet());
        }

        public List<string> GetAnagrams(InputWord inputWord)
        {
            IPermutator permutator = new Permutator();
            permutator.GeneratePermutations(inputWord);

            return WordList.Intersect(inputWord.Permutations).ToList();
        }
    }
}

