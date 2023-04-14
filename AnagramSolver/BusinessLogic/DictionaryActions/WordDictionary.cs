using Contracts.Interfaces;
using Contracts.Models;

namespace BusinessLogic.DictionaryActions
{
    internal class WordDictionary : IWordRepository
    {
        public List<DictWord> Words { get; private set; }
        private static string path;

        public WordDictionary()
        {
            Words = new List<DictWord>();

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

            try
            {
                using StreamReader sr = new (path);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    DictWord word = new(fields[0], fields[1], fields[2]);
                    Words.Add(word);
                }
            }
            catch
            {
                throw new IOException("Could not get words from the dictionary.");
            }

            return Words;
        }
    }
}
