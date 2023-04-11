using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DictionaryActions;
using BusinessLogic.InputWordActions;

namespace BusinessLogic
{
    interface IAnagramSolver
    {
        IList<string> GetAnagrams(string myWords);
    }

    public class AnagramSolver : IAnagramSolver
    {
        private static HashSet<string> WordList = (new WordRepository().GetWords()).Select(word => word.MainForm).ToHashSet<string>();
        private IList<string> recurrentWords { get; set; }

        private Permutator permutator { get; set; }

        public AnagramSolver()
        {
            permutator = new Permutator();
        }

        public IList<string> GetAnagrams(string myWord)
        {
            var allPermutations = permutator.GetPermutations(myWord);            

            var wordList = (new WordRepository().GetWords()).Select(word => word.MainForm).ToHashSet<string>();

            return (wordList.Intersect(allPermutations)).ToList<string>();
        }
    }
}

