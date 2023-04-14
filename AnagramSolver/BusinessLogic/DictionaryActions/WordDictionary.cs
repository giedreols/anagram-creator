using Contracts.Interfaces;
using Contracts.Models;
using System.Collections.ObjectModel;

namespace BusinessLogic.DictionaryActions
{
    internal class WordDictionary : IWordRepository
    {
        public ReadOnlyCollection<DictWord> Words { get; private set; }
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

        public IList<DictWord> GetWords()
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

            return Words;
        }
    }
}
