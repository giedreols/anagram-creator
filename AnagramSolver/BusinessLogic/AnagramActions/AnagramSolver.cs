using BusinessLogic.DictionaryActions;
using Contracts.Interfaces;
using Contracts.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace BusinessLogic.AnagramActions
{
    public class AnagramSolver : IAnagramSolver
    {
        private IReadOnlyDictionary<string, string> WordList { get; set; }

        public AnagramSolver()
        {
            IWordRepository wordRepository = new WordDictionary();

            List<string> tempList = new(wordRepository.GetWords().Select(word => word.MainForm));
            tempList.AddRange(wordRepository.GetWords().Select(word => word.AnotherForm));
            tempList = tempList.Distinct().ToList();

            var tempDictionary = tempList.ToDictionary(word => word, word => new string(word.OrderBy(c => c).ToArray()));
            WordList = tempDictionary;
        }

        public List<string> GetAnagrams(string inputWord)
        {
            List<string> anagrams = new();
            var inputSequence = inputWord.ToLower().OrderBy(c => c);

            foreach (var word in WordList)
            {
                if (inputSequence.SequenceEqual(word.Value.ToLower()) && word.Key.ToLower() != inputWord.ToLower())
                {
                    anagrams.Add(word.Key);
                }
            }

            return anagrams;
        }
    }
}


