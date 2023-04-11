using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DictionaryActions;
using BusinessLogic.InputWordActions;
using Cli;
using BusinessLogic.InputWordActions;

namespace BusinessLogic
{
    interface IAnagramSolver
    {
        List<string> GetAnagrams(InputWord myWords);
    }

    public class AnagramSolver : IAnagramSolver
    {
        private HashSet<string> wordList { get; set; }

        public AnagramSolver()
        {
            wordList = (new WordRepository().GetWords()).Select(word => word.MainForm).ToHashSet<string>();
        }

        public List<string> GetAnagrams(InputWord myWord)
        {
            var permutator = new Permutator();
            myWord = permutator.GeneratePermutations(myWord);
            return (wordList.Intersect(myWord.Permutations)).ToList();
        }
    }
}

