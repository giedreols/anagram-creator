namespace Cli
{
    interface IRenderer
    {
        void ShowInitialText();

        string GetWord();

        void RejectWord(string reason);

        void ShowAnagrams(IList<string> anagrams);

        bool DoRepeat();

    }

    public class Renderer : IRenderer
    {
        void IRenderer.ShowInitialText()
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

                foreach (var anagram in anagrams)
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
            var repeatText = Console.ReadLine().ToLower();
            bool repeatValue = false;

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
