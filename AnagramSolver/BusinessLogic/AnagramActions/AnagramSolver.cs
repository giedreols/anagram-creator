using BusinessLogic.DictionaryActions;
using Contracts.Interfaces;
using Contracts.Models;
using System.Collections.Immutable;

namespace BusinessLogic.AnagramActions
{
    public class AnagramSolver : IAnagramSolver
    {
        private ImmutableList<AnagramWord> WordList { get; set; }

        public AnagramSolver()
        {
            IWordRepository wordRepository = new WordDictionary();
            IList<DictWord> words = wordRepository.GetWords();
            List<AnagramWord> tempList = new();

            foreach (var word in words)
            {
                tempList.Add(new AnagramWord(word.MainForm, word.Type));
                tempList.Add(new AnagramWord(word.AnotherForm, word.Type));
            }

            tempList = tempList.DistinctBy(word => word.LowerCaseForm).ToList();
            tempList = tempList.OrderByDescending(word => word.MainForm.Length).ToList();
            WordList = tempList.ToImmutableList();
        }

        public List<string> GetAnagrams(string inputWord)
        {
            List<string> anagrams = new();
            var inputSequence = inputWord.ToLower().OrderBy(c => c);

            foreach (var word in WordList)
            {
                // koks skirtumas tarp ToLower ir ToLoweInvariant ?
                if (inputSequence.SequenceEqual(word.OrderedForm) && word.LowerCaseForm != inputWord.ToLower())
                {
                    anagrams.Add(word.MainForm);
                }
            }

            return anagrams;
        }


        public List<List<string>> GetAnagramsMultiWord(string inputWords)
        {
            List<List<string>> anagrams = new();

            var inputSequence = new string(inputWords.Replace(" ", "").OrderBy(c => c).ToArray()).ToLower();

            // kaip butu tvarkinga ir logiska - ar visus kintamuosius isirasyti i klases lygmeni ir nepaduoti metodui, ar visus paduoti, ar kazkaip dali taip, dali taip?
            anagrams = GetSequencesWithValidLength(inputSequence);

            return anagrams;
        }

        private List<List<string>> GetSequencesWithValidLength(string inputSequence)
        {
            Stack<int> indexes = new();
            List<Stack<int>> anagramIndexes = new();
            List<List<string>> anagrams = new();

            int index = 0;
            char[] residualChars = inputSequence.ToArray();


            while (index < WordList.Count)
            {
                // 1 ismesti per ilgus zodzius arba sekas ar tai, kas turi netinkamu (nelikusiu) raidziu
                if (WordList[index].MainForm.Length > residualChars.Length || !CheckIfWordContainsOnlyResidualChars(residualChars, WordList[index].OrderedForm))
                {
                    // 1.1
                    if (index < WordList.Count - 1)
                    {
                        index++;
                        continue;
                    }

                    // 1.2
                    if (index == WordList.Count - 1)
                    {

                        // 1.2.1
                        if (indexes.Count > 2)
                        {
                            residualChars = AppendResidualChars(residualChars, WordList[indexes.Peek()].OrderedForm);
                            indexes.Pop();
                            index = indexes.Peek() + 1;
                            residualChars = AppendResidualChars(residualChars, WordList[indexes.Peek()].OrderedForm);
                            indexes.Pop();
                            continue;
                        }

                        // 1.2.2
                        if (indexes.Count == 2)
                        {
                            indexes.Pop();
                            index = indexes.Peek() + 1;
                            indexes.Clear();
                            residualChars = inputSequence.ToArray();
                            continue;
                        }

                        // 1.2.3
                        if (indexes.Count == 1)
                        {
                            index = indexes.Peek() + 1;
                            indexes.Clear();
                            residualChars = inputSequence.ToArray();
                            continue;
                        }

                        // 1.2.4
                        if (indexes.Count == 0)
                        {
                            break;
                        }
                    }
                }

                // 2 darbas su vienodo ilgio zodziais arba kai seka pasiekia reikiama ilgi
                if (WordList[index].MainForm.Length == residualChars.Length)
                {
                    indexes.Push(index);
                    Stack<int> tempIndexes = new(indexes.AsEnumerable());
                    anagramIndexes.Add(tempIndexes);

                    // 2.1
                    if (index < WordList.Count - 1)
                    {
                        index = indexes.Peek() + 1;

                        // 2.1.1
                        if (indexes.Count > 0)
                        {
                            index = indexes.Peek() + 1;
                            indexes.Pop();
                            continue;
                        }

                        // 2.1.2
                        if (indexes.Count == 0)
                        {
                            index = indexes.Peek() + 1;
                            indexes.Clear();
                            continue;
                        }
                    }

                    // 2.2
                    if (index == WordList.Count - 1)
                    {

                        // 2.2.1
                        if (indexes.Count > 1)
                        {
                            indexes.Pop();
                            index = indexes.Peek() + 1;
                            residualChars = AppendResidualChars(residualChars, WordList[indexes.Peek()].OrderedForm);
                            indexes.Pop();
                            continue;
                        }

                        // 2.2.2
                        if (indexes.Count == 1)
                        {
                            indexes.Pop();
                            index = indexes.Peek() + 1;
                            residualChars = inputSequence.ToArray();
                            indexes.Clear();
                            continue;
                        }

                        // 2.2.3
                        if (indexes.Count == 0)
                        {
                            break;
                        }
                    }
                }

                // 3 trumpieji zodziai
                if (WordList[index].MainForm.Length < residualChars.Length)
                {
                    // 3.1
                    if (index < WordList.Count - 1)
                    {
                        indexes.Push(index);
                        residualChars = TrimResidualChars(residualChars, WordList[indexes.Peek()].OrderedForm);
                        index = indexes.Peek() + 1; // arba index++
                        continue;
                    }

                    // 3.2
                    if (index == WordList.Count - 1)
                    {
                        // 3.2.1
                        if (indexes.Count > 1)
                        {
                            index = indexes.Peek() + 1;
                            residualChars = AppendResidualChars(residualChars, WordList[indexes.Peek()].OrderedForm);
                            indexes.Pop();
                            continue;
                        }

                        // 3.2.2
                        if (indexes.Count == 1)
                        {
                            index = indexes.Peek() + 1;
                            indexes.Clear();
                            residualChars = inputSequence.ToArray();
                            continue;
                        }

                        // 3.2.3
                        if (indexes.Count == 0)
                        {
                            break;
                        }
                    }
                }
            }

            foreach (var indexSequence in anagramIndexes)
            {
                List<string> wordSequence = new();

                foreach (var i in indexSequence)
                {
                    wordSequence.Add(WordList[i].MainForm);
                }

                anagrams.Add(wordSequence);
            }

            return anagrams;

        }

        private static char[] AppendResidualChars(char[] residualChars, string word)
        {
            return residualChars.Concat(word.ToCharArray()).ToArray();
        }

        private static char[] TrimResidualChars(char[] residualChars, string stringToRemove)
        {

            foreach (char letter in stringToRemove.ToArray())
            {

                int index = Array.IndexOf(residualChars, letter);
                if (index != -1)
                {
                    char[] newArr = new char[residualChars.Length - 1];
                    Array.Copy(residualChars, 0, newArr, 0, index);
                    Array.Copy(residualChars, index + 1, newArr, index, residualChars.Length - index - 1);
                    residualChars = newArr;
                }
            }

            return residualChars;
        }

        private static bool CheckIfWordContainsOnlyResidualChars(char[] residualChars, string word)
        {
            bool contains = true;

            foreach (char letter in word.ToArray())
            {
                int index = Array.IndexOf(residualChars, letter);
                if (index != -1)
                {
                    char[] newArr = new char[residualChars.Length - 1];
                    Array.Copy(residualChars, 0, newArr, 0, index);
                    Array.Copy(residualChars, index + 1, newArr, index, residualChars.Length - index - 1);
                    residualChars = newArr;
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

        private List<string> GetValidWords(string inputSequence)
        {
            List<string> list = new();

            foreach (var word in WordList)
            {
                bool isAppropriate = false;

                if (inputSequence.Length < word.OrderedForm.Length)
                {
                    continue;
                }

                foreach (var letter in word.OrderedForm)
                {
                    if (inputSequence.Contains(letter))
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
                    list.Add(word.MainForm);
                }
            }

            return list;
        }
    }
}


