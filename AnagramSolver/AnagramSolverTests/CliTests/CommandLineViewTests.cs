using Cli;
using Contracts.Interfaces;

namespace AnagramSolverTests.CliTests
{
    public class CommandLineViewTests
    {
        private IRenderer _commandLine;
        private List<string> _anagrams;

        [SetUp]
        public void Setup()
        {
            _commandLine = new CommandLineView();
            _anagrams = new List<string>();
        }

        [Test]
        public void ShowAnagrams_ShowsAnagrams_IfTheyExist()
        {
            _anagrams.Add("alus");
            _anagrams.Add("vasara");
            _anagrams.Add("lobis");

            using var sw = new StringWriter();
            Console.SetOut(sw);
            _commandLine.ShowAnagrams(_anagrams);

            string result = sw.ToString().Trim();

            _anagrams.ForEach(anagram => StringAssert.Contains(anagram, result));
        }

        [Test]
        public void ShowAnagrams_ShowsAnagrams_IfTheyHaveLtChars()
        {
            _anagrams.Add("ąčęėįšųūž");

            using var sw = new StringWriter();
            Console.SetOut(sw);

            _commandLine.ShowAnagrams(_anagrams);

            string result = sw.ToString().Trim();

            StringAssert.Contains(_anagrams[0], result);
        }

        [Test]
        public void ShowAnagrams_ShowsInfoMessage_IfNoAnagrams()
        {
            using var sw = new StringWriter();
            Console.SetOut(sw);

            _commandLine.ShowAnagrams(_anagrams);

            string result = sw.ToString().Trim();

            StringAssert.Contains("No anagram", result);
        }

        [TearDown]
        public void TearDown()
        {
            _anagrams.Clear();
        }
    }
}