using Contracts.Interfaces;

namespace Cli
{
    public class Renderer : IRenderer
    {
        public Renderer()
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

        public void ShowAnagrams(IList<string> anagrams)
        {
            Console.WriteLine("**************");

            if (anagrams.Count != 0)
            {
                Console.WriteLine("Anagrams:");

                foreach (string anagram in anagrams)
                {
                    Console.WriteLine(anagram);
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
    }
}
