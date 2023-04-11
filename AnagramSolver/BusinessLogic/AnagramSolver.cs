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
        private Permutator permutator { get; set; }
        private HashSet<string> wordList { get; set; }

        public AnagramSolver()
        {
            permutator = new Permutator();
            wordList = (new WordRepository().GetWords()).Select(word => word.MainForm).ToHashSet<string>();
        }

        public IList<string> GetAnagrams(string myWord)
        {
            var allPermutations = permutator.GetPermutations(myWord);       
            return (wordList.Intersect(allPermutations)).ToList<string>();
        }
    }
}

