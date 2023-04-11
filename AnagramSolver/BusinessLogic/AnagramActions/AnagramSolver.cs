using BusinessLogic.DictionaryActions;
using BusinessLogic.InputWordActions;
using Contracts.Models;

namespace BusinessLogic.AnagramActions
{
    internal interface IAnagramSolver
    {
        List<string> GetAnagrams(InputWord myWords);
    }

    public class AnagramSolver : IAnagramSolver
    {
        private HashSet<string> wordList { get; set; }

        public AnagramSolver()
        {
            wordList = new WordRepository().GetWords().Select(word => word.MainForm).ToHashSet();
        }

        public List<string> GetAnagrams(InputWord myWord)
        {
            Permutator permutator = new Permutator();
            myWord = permutator.GeneratePermutations(myWord);
            return wordList.Intersect(myWord.Permutations).ToList();
        }
    }
}

