using BusinessLogic.DictionaryActions;
using Contracts.Interfaces;
using System.Collections.ObjectModel;

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
            List<string> tempList = new();
            var tempInputWord = inputWord.OrderBy(c => c);

            foreach (var word in WordList)
            {
                if (tempInputWord.SequenceEqual(word.OrderBy(c => c)))
                {
                    tempList.Add(word);
                }
            }

            tempList.Remove(inputWord);

            return tempList;
        }

    }
}


