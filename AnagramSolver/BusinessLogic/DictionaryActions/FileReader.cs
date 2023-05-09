using Contracts.Interfaces;

namespace BusinessLogic.DictionaryActions
{
    public class FileReader : IFileReader
    {
        private static string _path;

        public FileReader()
        {
            try
            {
                _path = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "net7.0\\Files\\zodynas.txt");
            }
            catch
            {
                throw new ArgumentException("Could not find file path");
            }
        }

        public string ReadFile()
        {
            string text;

            try
            {
                text = File.ReadAllText(_path);
            }
            catch
            {
                throw new IOException("Could not get words from the dictionary.");
            }

            return text;
        }
    }
}
