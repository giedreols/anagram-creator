using Cli;

namespace BusinessLogic.DictionaryActions
{
    interface IWordRepository
    {
        IList<DictWord> GetWords();
    }

    internal class WordRepository : IWordRepository
    {
        private string path;

        public WordRepository()
        {
            path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "net7.0\\Files\\zodynas.txt");

        }

        public IList<DictWord> GetWords()
        {
            var words = new List<DictWord>(200000);

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] fields = line.Split('\t');

                        var word = new DictWord()
                        {
                            MainForm = fields[0],
                            Type = fields[1],
                            AnotherForm = fields[2]
                        };
                        words.Add(word);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return words;
        }
    }
}
