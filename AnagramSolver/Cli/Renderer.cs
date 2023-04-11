using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cli
{
    interface IRenderer
    {
        void ShowAnagrams(IList<string> anagrams);

        string GetWord();
    }

    public class Renderer : IRenderer
    {
        public string GetWord()
        {
            var config = new Configuration();
            var minLength = config.MinLength;

            Console.WriteLine("**************");
            Console.WriteLine("Enter word: ");
            var word = Console.ReadLine();

            if (!IsValidChars(word))
            {
                Console.WriteLine("Word contains invalid characters. Try again.");
                GetWord();
            }

            if (!IsValidLength(word, minLength))
            {
                Console.WriteLine("Word is too short. Min. length = " + minLength);
                GetWord();
            }

            return word;
        }

        public bool Repeat()
        {
            Console.WriteLine("Repeat? Y/N");
            var repeat = Console.ReadLine().ToLower();

            switch (repeat)
            {
                case "y": return true;
                case "n": return false;
                default:
                    Repeat();
                    return false;
            }
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

        private static bool IsValidChars(string word)
        {
            bool isValid = false;

            foreach (char letter in word)
            {
                if (char.IsDigit(letter) || char.IsLetter(letter) || letter.Equals('-'))
                {
                    isValid = true;
                }

                else
                {
                    isValid = false;
                    break;
                }
            }
            return isValid;
        }

        private static bool IsValidLength(string word, int minLength)
        {
            return word.Length >= minLength;
        }
    }
}
