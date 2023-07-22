using System.Text;

namespace AnagramSolver.BusinessLogic.Helpers
{
    public static class Converter
    {
        public static byte[] ConvertListToByteArr(this IList<string> list)
        {
            IList<string> stringList = new List<string>();

            foreach (var word in list)
            {
                stringList.Add(word);
            }

            string concatenatedString = string.Join("\n", stringList);

            byte[] fileBytes = Encoding.UTF8.GetBytes(concatenatedString);

            return fileBytes;
        }
    }
}
