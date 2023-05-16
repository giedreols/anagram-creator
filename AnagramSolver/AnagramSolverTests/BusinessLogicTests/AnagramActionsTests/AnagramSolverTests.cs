using Contracts.Interfaces;
using Contracts.Models;
using Moq;

namespace AnagramSolverTests.BusinessLogicTests.AnagramActionsTests
{
	public class AnagramSolverTests
    {
        private Mock<IWordRepository> _mockWordRepository;
        private IAnagramSolver _anagramSolver;

        [SetUp]
        public void Setup()
        {
            List<AnagramWord> list = new () {
                new AnagramWord("siela"),
                new AnagramWord("alus"),
                new AnagramWord("upė"),
                new AnagramWord("liesa"),
                new AnagramWord("liepa"),
                new AnagramWord("sula") };

            _mockWordRepository = new Mock<IWordRepository>(MockBehavior.Strict);
            _mockWordRepository.Setup(p => p.GetWords()).Returns(list);

			_anagramSolver = new BusinessLogic.AnagramActions.AnagramSolver(_mockWordRepository.Object);
        }


        [Test]
        public void GetAnagrams_ReturnsAnagram_IfInputWordHasIt()
        {
            List<string> expResult = new() { "liepa" };

            List<string> anagrams = _anagramSolver.GetAnagrams("palei");

            Assert.That(anagrams, Is.EqualTo(expResult));
        }

        [Test]
        public void GetAnagrams_ReturnsEmtpyList_IfInputWordDoesNotHaveIt()
        {
            List<string> anagrams = _anagramSolver.GetAnagrams("rytas");

            Assert.That(anagrams, Is.Empty);
        }

        [Test]
        public void GetAnagrams_ReturnsAnagram_IfInputWordInCapitals()
        {
            List<string> expResult = new() { "siela" };
            
            List<string> anagrams = _anagramSolver.GetAnagrams("LIESA");

            Assert.That(anagrams, Is.EqualTo(expResult));
        }

        [Test]
        public void GetAnagrams_ReturnsEmptyList_IfInputWordExistsInList()
        {
            List<string> anagrams = _anagramSolver.GetAnagrams("upė");

            Assert.That(anagrams, Is.Empty);
        }

    }
}