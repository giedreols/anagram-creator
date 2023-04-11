using Cli;
using Contracts.Interfaces;
using Contracts.Models;

namespace BusinessLogic.DictionaryActions
{
    internal class WordRepository : IWordRepository
    {
        private static string path { get; set; }
        private Renderer renderer = new Renderer();


        public WordRepository()
        {
            var dir = Directory.GetCurrentDirectory();
            if (dir == null)
            {
                renderer.ShowError("Can't find dictionary.");
            }

            try
            {
                path = Path.Combine(Directory.GetParent(dir).FullName, "net7.0\\Files\\zodynas.txt");
            }
            catch (Exception ex)
            {
                renderer.ShowError(ex.Message);
                throw;
            }
        }

        public IList<DictWord> GetWords()
        {
            List<DictWord> words = new(200000);

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
            catch (Exception ex)
            {
                renderer.ShowError(ex.Message);
                throw;
            }

            return words;
        }
    }
}
