using Contracts.Interfaces;
using Contracts.Models;

namespace BusinessLogic.DictionaryActions
{
    internal class WordRepository : IWordRepository
    {
        private static string path { get; set; }

        public WordRepository()
        {
            var dir = Directory.GetCurrentDirectory();
            try
            {
                path = Path.Combine(Directory.GetParent(dir).FullName, "net7.0\\Files\\zodynas.txt");
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        public IList<DictWord> GetWords()
        {
            List<DictWord> words = new();

            try
            {
                using StreamReader sr = new StreamReader(path);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split('\t');
                    DictWord word = new DictWord(fields[0], fields[1], fields[2]);
                    words.Add(word);
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            return words;
        }
    }
}
