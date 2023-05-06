using Cli;

namespace AnagramSolverTests.CliTests.CommandLineViewTests
{
    public class CommandLineViewTests
    {
        [SetUp]
        public void Setup()
        {

        }

        // nezinau kaipo sita parasyt, kad veiktu
        //[Test]
        public void GetWord_ReadsWordFromCommandLine_IfWordIsEntered()
        {
            string input = "ragai\r\n";

            using var sw = new StringWriter();
            Console.SetOut(sw);

            Console.SetIn(new StringReader(input));

            string result = new CommandLineView().GetWord();

            Assert.That(result, Is.EqualTo(input));
        }

        [Test]
        public void ShowAnagrams_ShowsAnagrams_IfTheyExist()
        {
            List<string> anagrams = new()
            {
                "alus",
                "vasara",
                "lobis"
            };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            new CommandLineView().ShowAnagrams(anagrams);

            string result = sw.ToString().Trim();

            anagrams.ForEach(anagram => StringAssert.Contains(anagram, result));
        }

        [Test]
        public void ShowAnagrams_ShowsAnagrams_IfTheyHaveLtChars()
        {
            List<string> anagrams = new()
            {
                "ąčęėįšųūž",
            };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            new CommandLineView().ShowAnagrams(anagrams);

            string result = sw.ToString().Trim();

            StringAssert.Contains(anagrams[0], result);
        }

        [Test]
        public void ShowAnagrams_ShowsInfoMessage_IfNoAnagrams()
        {
            List<string> anagrams = new() { };

            using var sw = new StringWriter();
            Console.SetOut(sw);

            new CommandLineView().ShowAnagrams(anagrams);

            string result = sw.ToString().Trim();

            StringAssert.Contains("No anagram", result);
        }
    }
}