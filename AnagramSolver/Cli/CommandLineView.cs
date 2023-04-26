using Contracts.Interfaces;
using System.Text;

namespace Cli
{
    public class CommandLineView : IRenderer
    {
        public CommandLineView()
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.UTF8;
        }

        public void ShowHeader()
        {
            Console.WriteLine("**************");
            Console.WriteLine("ANAGRAM GENERATOR");
            Console.WriteLine("**************");
        }

        public string GetWord()
        {
            Console.WriteLine("Enter word: ");
            return Console.ReadLine();
        }

        public void RejectWord(string reason)
        {
            Console.WriteLine("Can't create anagram. Reason: " + reason);
        }

        public void ShowAnagrams(IList<List<string>> anagrams)
        {
            Console.WriteLine("**************");

            if (anagrams.Count != 0)
            {
                Console.WriteLine("Anagrams:");

                foreach (var anagram in anagrams)
                {
                    Console.WriteLine(string.Join(" ", anagram));
                }
            }
            else
            {
                Console.WriteLine("No anagram was found.");
            }

            Console.WriteLine("**************");
        }

        public bool DoRepeat()
        {
            Console.WriteLine("Repeat? Y/N");
            string repeatText = Console.ReadLine().ToLower();
            bool repeatValue;

            switch (repeatText)
            {
                case "y": repeatValue = true; break;
                case "n": repeatValue = false; break;
                default:
                    repeatValue = DoRepeat();
                    break;
            }
            return repeatValue;
        }

        public void ShowError(string message)
        {
            Console.WriteLine("Something is wrong. Message: " + message);
        }
    }
}
