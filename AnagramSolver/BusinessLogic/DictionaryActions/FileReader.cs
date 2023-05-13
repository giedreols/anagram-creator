using Contracts.Interfaces;

namespace BusinessLogic.DictionaryActions
{
    public class FileReader : IFileReader
    {
        private static string path;

        // turbut reiketu failo pavadinima iskelti kur nors i konfiga, o pati patha paduoti i "read file"? ir tada galeciau testuoti sita klase labiau?
        public FileReader()
        {
            try
            {
                path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "BusinessLogic\\bin\\debug\\net7.0\\Files\\zodynas.txt");
            }
            catch
            {
                throw new ArgumentException("Could not find file path");
            }
        }

        public IList<string> ReadFile()
        {
            List<string> list = new();

            try
            {
                using StreamReader sr = new(path);
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
            catch
            {
                throw new IOException("Could not get words from the dictionary.");
            }

            return list;
        }
    }
}
