using BusinessLogic.AnagramActions;
using Contracts.Interfaces;
using Contracts.Models;
using Moq;
using System.Collections.Immutable;

namespace AnagramSolverTests.BusinessLogicTests.AnagramActionsTests
{
    public class Tests
    {
        private Mock<IWordRepository> mockWordRepository;
        private AnagramSolver anagramSolver;

        [SetUp]
        public void Setup()
        {
            ImmutableList<AnagramWord> list = (new List<AnagramWord>() {
                new AnagramWord("siela"),
                new AnagramWord("alus"),
                new AnagramWord("upė"),
                new AnagramWord("liesa"),
                new AnagramWord("liepa"),
                new AnagramWord("sula") }).ToImmutableList();

            mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
            mockWordRepository.Setup(p => p.GetWords()).Returns(list);

            anagramSolver = new(mockWordRepository.Object);
        }


        [Test]
        public void GetAnagrams_ReturnsAnagram_IfInputWordHasIt()
        {
            List<string> anagrams = anagramSolver.GetAnagrams("palei");

            List<string> expResult = new() { "liepa" };

            Assert.That(anagrams, Is.EqualTo(expResult));
        }

        [Test]
        public void GetAnagrams_ReturnsEmtpyList_IfInputWordDoesNotHaveIt()
        {
            List<string> anagrams = anagramSolver.GetAnagrams("rytas");

            Assert.That(anagrams, Is.Empty);
        }

        [Test]
        public void GetAnagrams_ReturnsAnagram_IfInputWordInCapitals()
        {
            List<string> anagrams = anagramSolver.GetAnagrams("LIESA");

            List<string> expResult = new() { "siela" };

            Assert.That(anagrams, Is.EqualTo(expResult));
        }

        [Test]
        public void GetAnagrams_ReturnsEmptyList_IfInputWordExistsInList()
        {
            List<string> anagrams = anagramSolver.GetAnagrams("upė");

            Assert.That(anagrams, Is.Empty);
        }

    }
}