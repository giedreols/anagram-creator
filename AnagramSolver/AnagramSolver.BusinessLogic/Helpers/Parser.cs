using AnagramSolver.Contracts.Dtos;
using AnagramSolver.Contracts.Dtos.Obsolete;

namespace AnagramSolver.BusinessLogic.Helpers
{
    public static class Parser
    {
        public static List<string> ParseLinesWithTabs(string text)
        {
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);

            return lines.ToList();
        }


        public static List<string> ParseSingleLines(string text)
        {
            string[] lines = text.Split('\n');

            return lines.ToList();
        }

        public static List<FullWordDto> ParseDictionaryWordsFromLines(List<string> linesList)
        {
            List<FullWordDto> wordList = new();

            try
            {
                foreach (string line in linesList)
                {
                    if (line.Contains('\t'))
                    {
                        string[] fields = line.Split('\t');
                        wordList.Add(new FullWordDto(fields[0], fields[2], fields[1]));
                    }
                }
                if (wordList.Count == 0) { throw new Exception("Dictionary is empty"); }
            }
            catch
            {
                throw new IndexOutOfRangeException("Dictionary is empty of file structure is incorrect; Each line should contain at least 3 fields, separated by tabs");
            }
            return wordList;
        }

        public static List<string> ParseWordsFromDictionaryFile(List<string> linesList)
        {
            List<string> wordList = new();

            try
            {
                foreach (string line in linesList)
                {
                    if (line.Contains('\t'))
                    {
                        string[] fields = line.Split('\t');
                        wordList.Add(fields[0]);
                        wordList.Add(fields[2]);
                    }
                }
                if (wordList.Count == 0) { throw new Exception("Dictionary is empty"); }
            }

            catch
            {
                throw new IndexOutOfRangeException("Dictionary is empty of file structure is incorrect; Each line should contain at least 3 fields, separated by tabs");
            }

            return wordList;
        }
    }
}
