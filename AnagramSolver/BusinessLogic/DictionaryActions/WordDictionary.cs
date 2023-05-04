using Contracts.Interfaces;
using Contracts.Models;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace BusinessLogic.DictionaryActions
{
    public class WordDictionary : IWordRepository
    {
        private ReadOnlyCollection<DictWord> Words { get; set; }
        private static string path;

        public WordDictionary()
        {
            try
            {
                path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "net7.0\\Files\\zodynas.txt");
            }
            catch
            {
                throw new ArgumentException("Could not find file path");
            }
        }

        // kaip geriau ir logiskiau - ar metode saugot kintamuosius, ar klaseje? ir privacius metodus geriau void ar return tipo daryt?
        public ImmutableList<AnagramWord> GetWords()
        {
            ReadWords();
            return ConvertDictionaryWordsToAnagramWords();
        }

        private ImmutableList<AnagramWord> ConvertDictionaryWordsToAnagramWords()
        {
            List<AnagramWord> tempList = new();

            foreach (var word in Words)
            {
                tempList.Add(new AnagramWord(word.MainForm));
                tempList.Add(new AnagramWord(word.AnotherForm));

            }

            tempList = tempList.DistinctBy(word => word.LowerCaseForm).ToList();
            tempList = tempList.OrderByDescending(word => word.MainForm.Length).ToList();
            return tempList.ToImmutableList();
        }

        private void ReadWords()
        {
            List<DictWord> tempList = new();

            try
            {
                using StreamReader sr = new(path);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    DictWord word = new(fields[0], fields[1], fields[2]);
                    tempList.Add(word);
                }
            }
            catch
            {
                throw new IOException("Could not get words from the dictionary.");
            }

            Words = new ReadOnlyCollection<DictWord>(tempList.Distinct().ToList());
        }
    }
}
