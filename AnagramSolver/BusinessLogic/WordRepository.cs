using Cli;
using System.Security.Cryptography;

namespace BusinessLogic
{
    interface IWordRepository
    {
        IList<Word> GetWords();
    }

    internal class WordRepository : IWordRepository
    {
        public IList<Word> GetWords()
        {
            var words = new List<Word>(200000);

            string path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "net7.0\\Files\\zodynas.txt");

            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] fields = line.Split('\t');

                        var word = new Word()
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



            //try
            //{
            //    using (StreamReader sr = new StreamReader(path))
            //    {
            //        string line;
            //        while ((line = sr.ReadLine()) != null)
            //        {
            //            string[] fields = line.Split('\t');

            //            var word = new Word()
            //            {
            //                MainForm = fields[0],
            //                Type = fields[1],
            //                Forms = new List<string>()
            //                {
            //                    fields[3]
            //                }
            //            };
            //            words.Add(word);
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("The file could not be read:");
            //    Console.WriteLine(e.Message);
            //}

            //return words;
        }
    }
}
