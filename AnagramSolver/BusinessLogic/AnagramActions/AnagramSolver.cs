using BusinessLogic.DictionaryActions;
using Contracts.Interfaces;
using Contracts.Models;
using System.Collections.Immutable;
using System.ComponentModel.Design;
using System.Diagnostics;

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
            var inputSequence = inputWord.ToLower().OrderByDescending(c => c);

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
            var tempList = GetValidWordsFromWordList(inputWords);

            var inputSequence = new string(inputWords.Replace(" ", "").OrderBy(c => c).ToArray()).ToLower();

            Dictionary<string, int> counters = new Dictionary<string, int>();
            counters.Add("ilgieji", 0);
            counters.Add("lygus", 0);
            counters.Add("trumpieji", 0);


            List<List<string>> anagrams = new();
            Stack<int> indexes = new();
            List<Stack<int>> anagramIndexes = new();
            int index = 0;
            char[] residualChars = inputSequence.ToArray();

            var timer = new Stopwatch();
            timer.Start();

            // temp lista issiskaidyti pagal tai, kokio ilgio jame zodziai
            // key - ilgis, value - indeksas
            Dictionary<int, int> wordLengthIndexes = new();

            int counter = 1;
            var firstIndex = 0;

            for (int i = 0; i < tempList.Count - 2; i++)
            {

                if (tempList[i].OrderedForm.Length == tempList[i + 1].OrderedForm.Length)
                {
                    counter++;
                }
                else
                {
                    wordLengthIndexes.Add(tempList[i].OrderedForm.Length, firstIndex);
                    counter = 1;
                    firstIndex = i + 1;
                }
            }
            if (firstIndex < tempList.Count - 1)
            {
                wordLengthIndexes.Add(tempList[firstIndex].OrderedForm.Length, firstIndex);
            }


            while (index < tempList.Count)
            {
                // 1 ismesti per ilgus zodzius arba sekas ar tai, kas turi netinkamu (nelikusiu) raidziu
                if (tempList[index].MainForm.Length > residualChars.Length)
                {
                    counters["ilgieji"] = counters["ilgieji"] + 1;

                    // 1.1
                    if (index < tempList.Count - 1)
                    {
                        // rasti artimiausia tinkamo ilgio arba trumpesni indeksa
                        var tempIndex = FindNextAppropriateLengthIndex(wordLengthIndexes, residualChars.Length);

                        if (tempIndex == -1) break;
                    }

                    // 1.2
                    else
                    {
                        // 1.2.1
                        if (indexes.Count > 2)
                        {
                            residualChars = AppendResidualChars(residualChars, tempList[indexes.Peek()].OrderedForm);
                            indexes.Pop();
                            index = indexes.Peek() + 1;
                            residualChars = AppendResidualChars(residualChars, tempList[indexes.Peek()].OrderedForm);
                        }

                        // 1.2.2
                        else if (indexes.Count == 2)
                        {
                            indexes.Pop();
                            index = indexes.Peek() + 1;
                            residualChars = inputSequence.ToArray();
                        }

                        // 1.2.3
                        else if (indexes.Count == 1)
                        {
                            index = indexes.Peek() + 1;
                            residualChars = inputSequence.ToArray();
                        }
                    }
                }

                if (!WordContainsOnlyCharsFromList(residualChars, tempList[index].OrderedForm))
                {
                    counters["lygus"] = counters["lygus"] + 1;


                    // 1.1
                    if (index < tempList.Count - 1)
                    {
                        index++;
                        continue;
                    }

                    // 1.2
                    else
                    {
                        // 1.2.1
                        if (indexes.Count > 2)
                        {
                            residualChars = AppendResidualChars(residualChars, tempList[indexes.Peek()].OrderedForm);
                            indexes.Pop();
                            index = indexes.Peek() + 1;
                            residualChars = AppendResidualChars(residualChars, tempList[indexes.Peek()].OrderedForm);
                        }

                        // 1.2.2
                        else if (indexes.Count == 2)
                        {
                            indexes.Pop();
                            index = indexes.Peek() + 1;
                            residualChars = inputSequence.ToArray();
                        }

                        // 1.2.3
                        else if (indexes.Count == 1)
                        {
                            index = indexes.Peek() + 1;
                            residualChars = inputSequence.ToArray();
                        }
                    }
                }

                // 2 darbas su vienodo ilgio zodziais arba kai seka pasiekia reikiama ilgi
                else if (tempList[index].MainForm.Length == residualChars.Length)
                {
                    indexes.Push(index);
                    Stack<int> tempIndexes = new(indexes.AsEnumerable());
                    anagramIndexes.Add(tempIndexes);

                    // 2.1
                    if (index < tempList.Count - 1)
                    {
                        index = indexes.Peek() + 1;
                    }

                    // 2.2
                    else
                    {
                        indexes.Pop();
                        index = indexes.Peek() + 1;
                        residualChars = AppendResidualChars(residualChars, tempList[indexes.Peek()].OrderedForm);
                    }
                }

                // 3 trumpieji zodziai
                else
                {
                    counters["trumpieji"] = counters["trumpieji"] + 1;

                    // 3.1
                    if (index < tempList.Count - 1)
                    {
                        indexes.Push(index);
                        residualChars = TrimWordCharsFromList(residualChars, tempList[indexes.Peek()].OrderedForm);
                        index = indexes.Peek() + 1; // arba index++
                        continue;
                    }

                    // 3.2
                    else if (indexes.Count > 0)
                    {
                        index = indexes.Peek() + 1;
                        residualChars = AppendResidualChars(residualChars, tempList[indexes.Peek()].OrderedForm);
                    }
                }

                switch (indexes.Count)
                {
                    case 1: indexes.Clear(); break;
                    case 0: index = tempList.Count; break;
                    default: indexes.Pop(); break;

                }
            }



            timer.Stop();

            Console.WriteLine("Time taken: " + timer.Elapsed.ToString(@"m\:ss\.fff"));
            Console.WriteLine("ilgieji: " + counters["ilgieji"]);
            Console.WriteLine("lygus: " + counters["lygus"]);
            Console.WriteLine("trumpieji: " + counters["trumpieji"]);


            foreach (var indexSequence in anagramIndexes)
            {
                List<string> wordSequence = new();

                foreach (var i in indexSequence)
                {
                    wordSequence.Add(tempList[i].MainForm);
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


