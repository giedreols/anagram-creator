using BusinessLogic.DictionaryActions;
using BusinessLogic.InputWordActions;
using Contracts.Interfaces;
using Contracts.Models;
using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;

namespace BusinessLogic.AnagramActions
{
    public class AnagramSolver : IAnagramSolver
    {
        private ReadOnlyCollection<string> WordList { get; set; }

        public AnagramSolver()
        {
            IWordRepository wordRepository = new WordDictionary();

            List<string> tempList = new(wordRepository.GetWords().Select(word => word.MainForm));
            tempList.AddRange(wordRepository.GetWords().Select(word => word.AnotherForm));
            WordList = new ReadOnlyCollection<string>(tempList.Distinct().ToList());
        }

        public List<string> GetAnagrams(string inputWord)
        {
            var tempList = new List<string>();
            string tempInputWord = new(inputWord.OrderBy(c => c).ToArray());

            foreach (var word in WordList)
            {
                string tempDictWord = new(word.OrderBy(c => c).ToArray());

                if (tempDictWord.SequenceEqual(tempInputWord))
                {
                    tempList.Add(word);
                }
            }

            tempList.Remove(inputWord);

            return tempList;
        }
    }
}


