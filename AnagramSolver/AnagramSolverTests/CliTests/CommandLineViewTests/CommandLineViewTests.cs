using Cli;
using Moq;
using System;
using Microsoft.Extensions.Logging.Abstractions;
using McMaster.Extensions.CommandLineUtils;
using Contracts.Interfaces;

namespace AnagramSolverTests.BusinessLogicTests.InputWordActionsTests
{
    public class CommandLineViewTests
    {
        [SetUp]
        public void Setup()
        {

        }

        // truksta testu kitiems comman line view metodams


        //[Test]
        public void GetWord_ReadsWordFromCommandLine_IfWordIsEntered()
        {
            // nezinau kaipo sita parasyt, nerandu kaip user input uzsimokint

            string input = "ragai" + Environment.NewLine;

            using var sw = new StringWriter();
            Console.SetOut(sw);

            Console.SetIn(new StringReader(input));

            // neveikia....
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