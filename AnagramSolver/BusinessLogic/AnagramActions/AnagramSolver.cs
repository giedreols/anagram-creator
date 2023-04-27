using BusinessLogic.DictionaryActions;
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

                anagrams.Add(wordSequence);
            }

            return anagrams;

        }

        private int FindNextAppropriateLengthIndex(Dictionary<int, int> indexesList, int length)
        {
            while (length > 0)
            {
                if (indexesList.ContainsKey(length))
                {
                    return indexesList[length];
                }
                else
                {
                    length--;
                }

            }
            return -1;
        }

        private static char[] AppendResidualChars(char[] residualChars, string word)
        {
            return residualChars.Concat(word.ToCharArray()).ToArray();
        }

        private static char[] TrimWordCharsFromList(char[] list, string stringToRemove)
        {

            foreach (char letter in stringToRemove.ToArray())
            {

                int index = Array.IndexOf(list, letter);
                if (index != -1)
                {
                    char[] newArr = new char[list.Length - 1];
                    Array.Copy(list, 0, newArr, 0, index);
                    Array.Copy(list, index + 1, newArr, index, list.Length - index - 1);
                    list = newArr;
                }
            }

            return list;
        }

        private static bool WordContainsOnlyCharsFromList(char[] list, string word)
        {
            bool contains = true;

            foreach (char letter in word.ToArray())
            {
                int index = Array.IndexOf(list, letter);
                if (index != -1)
                {
                    char[] newArr = new char[list.Length - 1];
                    Array.Copy(list, 0, newArr, 0, index);
                    Array.Copy(list, index + 1, newArr, index, list.Length - index - 1);
                    list = newArr;
                    contains = true;
                }
                else
                {
                    contains = false;
                    break;
                }
            }

            return contains;

        }

        private List<AnagramWord> GetValidWordsFromWordList(string inputWords)
        {
            List<AnagramWord> list = new();
            string[] inputWordList = inputWords.ToLower().Split(' ');
            string inputWordString = inputWords.ToLower();


            foreach (var word in WordList)
            {
                bool isAppropriate = false;

                if (word.LowerCaseForm.Equals("sula"))
                {

                }

                if (inputWordString.Length < word.OrderedForm.Length || inputWordList.Contains(word.LowerCaseForm))
                {
                    continue;
                }

                foreach (var letter in word.OrderedForm)
                {
                    if (inputWordString.Contains(letter))
                    {
                        isAppropriate = true;
                    }
                    else
                    {
                        isAppropriate = false;
                        break;
                    }
                }

                if (isAppropriate)
                {
                    list.Add(word);
                }
            }

            return list;
        }
    }
}


